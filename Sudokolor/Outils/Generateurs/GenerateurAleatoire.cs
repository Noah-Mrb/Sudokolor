using Modele;
using System.Security.Cryptography;
using System.Text;

namespace Outils.Generateurs
{
    /// <summary>
    /// Generateur de grille de sudoku
    /// aleatoire résolvable
    /// </summary>
    /// <author>Valentin Colindre, Noah Mirbel</author>
    public class GenerateurAleatoire : IGenerateurGrille
    {
        private const int TAILLE_GRILLE = 9;
        private const int TAILLE_SOUS_GRILLE = 3;
        private Random? aleatoire;
        private Grille? grilleResolue;

        /// <inheritdoc />
        public Grille GenererGrille(string graine = "", int quantiteCaseVide = 40)
        {
            aleatoire = GenererUnRandom(graine);
            int[,] grille = new int[TAILLE_GRILLE, TAILLE_GRILLE];
            RemplirSousGrillesDiagonales(grille);
            RemplirReste(grille, 0, TAILLE_SOUS_GRILLE);

            // on conserve la grille complétée pour les solutions
            grilleResolue = ConvertirEnGrille(grille, false, graine);

            RetirerNombres(grille, quantiteCaseVide);
            return ConvertirEnGrille(grille, false, graine);
        }

        /// <summary>
        /// Génère un nombre aléatoire à partir d'une graine
        /// </summary>
        /// <param name="graine">graine de génération</param>
        /// <returns>un Random</returns>
        private Random GenererUnRandom(string graine)
        {
            // On transforme la graine (une chaîne de caractères) en un tableau de bytes avec un encodage UTF-8
            byte[] bytesGraine = Encoding.UTF8.GetBytes(graine);

            // On applique le hachage SHA-256 à ces bytes pour obtenir un tableau de bytes de 32 octets
            byte[] hash = SHA256.Create().ComputeHash(bytesGraine);

            // On prend les 4 premiers octets du tableau de hash et on les convertit en un entier (int)
            int graineInt = BitConverter.ToInt32(hash, 0);

            // On utilise cet entier comme graine pour initialiser un générateur de nombres aléatoires
            return new Random(graineInt);
        }

        /// <inheritdoc />
        public int ObtenirSolutionCase(int ligne, int colonne, string graine = "")
        {
            int valeur = 0;

            if (grilleResolue != null)
            {
                valeur = grilleResolue[ligne, colonne].Valeur;
            }
            else
            {
                valeur = GenererGrille(graine, 0)[ligne, colonne].Valeur;
            }

            return valeur;
        }

        /// <inheritdoc />
        public bool VerifierGrille(Partie partie)
        {
            bool resultat = true;
            int i = 0;
            while (i < TAILLE_GRILLE && resultat)
            {
                int j = 0;
                while (j < TAILLE_GRILLE && resultat)
                {
                    bool estValide = EstValide(ConvertirPartieEnTableau(partie), i, j, partie.RecupererCaseGrille(i, j).Valeur);
                    if (partie.RecupererCaseGrille(i, j).Valeur == 0 || !estValide)
                        resultat = false;
                    j++;
                }
                i++;
            }

            return resultat;
        }

        /// <inheritdoc />
        public List<(int, int)> RecupererErreurs(Partie partie)
        {
            List<(int, int)> erreurs = new List<(int, int)> ();

            for (int i = 0; i < TAILLE_GRILLE; i++)
            {
                for (int j = 0; j < TAILLE_GRILLE; j++)
                {
                    if (
                            partie.RecupererCaseGrille(i, j).Valeur != 0
                            && !EstValide(ConvertirPartieEnTableau(partie), i, j, partie.RecupererCaseGrille(i, j).Valeur)
                            && partie.RecupererCaseGrille(i, j).EstModifiable
                        )
                    {
                        erreurs.Add((i, j));
                    }
                }
            }

            return erreurs;
        }

        /// <summary>
        /// Remplit les sous-grilles en diagonales avec des valeurs
        /// </summary>
        /// <param name="grille">grille actuelle</param>
        private void RemplirSousGrillesDiagonales(int[,] grille)
        {
            for (int i = 0; i < TAILLE_GRILLE; i += TAILLE_SOUS_GRILLE)
                RemplirSousGrille(grille, i, i);
        }

        /// <summary>
        /// remplit une sous-grille
        /// </summary>
        /// <param name="grille">grille actuelle</param>
        /// <param name="ligne">début de la ligne de la sous-grille</param>
        /// <param name="colonne">début de la colonne dans la sous-grille</param>
        private void RemplirSousGrille(int[,] grille, int ligne, int colonne)
        {
            var nombres = Enumerable.Range(1, TAILLE_GRILLE).ToList();
            for (int i = 0; i < TAILLE_SOUS_GRILLE; i++)
                for (int j = 0; j < TAILLE_SOUS_GRILLE; j++)
                {
                    int index = aleatoire.Next(nombres.Count);
                    grille[ligne + i, colonne + j] = nombres[index];
                    nombres.RemoveAt(index);
                }
        }

        /// <summary>
        /// Remplit ce qui n'a pas été remplit de la grille.
        /// Récursif.
        /// </summary>
        /// <param name="grille">grille actuelle</param>
        /// <param name="ligne">début de la ligne</param>
        /// <param name="colonne">début de la colonne</param>
        /// <returns>vrai si réussite, faux sinon</returns>
        private bool RemplirReste(int[,] grille, int ligne, int colonne)
        {
            bool resultat = false;
            bool continuer = true;

            while (continuer)
            {
                if (colonne == TAILLE_GRILLE)
                {
                    ligne++;
                    colonne = 0;
                }

                if (ligne == TAILLE_GRILLE)
                {
                    resultat = true;
                    continuer = false;
                }
                else if (grille[ligne, colonne] != 0)
                {
                    colonne++;
                }
                else
                {
                    bool nombreTrouve = false;
                    for (int num = 1; num <= TAILLE_GRILLE && !nombreTrouve; num++)
                    {
                        if (EstValide(grille, ligne, colonne, num))
                        {
                            grille[ligne, colonne] = num;
                            if (RemplirReste(grille, ligne, colonne + 1))
                            {
                                resultat = true;
                                nombreTrouve = true;
                            }
                            else
                            {
                                grille[ligne, colonne] = 0;
                            }
                        }
                    }
                    if (!nombreTrouve)
                    {
                        continuer = false;
                    }
                }
            }

            return resultat;
        }

        /// <summary>
        /// Vérifie si une valeur est valide pour une case par rapport à celles autour.
        /// </summary>
        /// <param name="grille">Grille Sudoku.</param>
        /// <param name="ligne">Indice de la ligne de la case.</param>
        /// <param name="colonne">Indice de la colonne de la case.</param>
        /// <param name="num">Valeur à tester pour la case.</param>
        /// <returns>Vrai si la valeur est valide, faux sinon.</returns>
        private bool EstValide(int[,] grille, int ligne, int colonne, int num)
        {
            bool estValide = true;

            estValide &= EstValideDansLigne(grille, ligne, colonne, num);
            estValide &= EstValideDansColonne(grille, ligne, colonne, num);
            estValide &= EstValideDansSousGrille(grille, ligne, colonne, num);

            return estValide;
        }

        /// <summary>
        /// Vérifie si une valeur est valide dans la ligne donnée.
        /// </summary>
        /// <param name="grille">Grille Sudoku.</param>
        /// <param name="ligne">Indice de la ligne.</param>
        /// <param name="colonne">Indice de la colonne de la case.</param>
        /// <param name="num">Valeur à tester.</param>
        /// <returns>Vrai si la valeur n'existe pas dans la ligne sauf dans la case spécifiée, faux sinon.</returns>
        /// <author>Nordine Hida, Noah Mirbel</author>
        private bool EstValideDansLigne(int[,] grille, int ligne, int colonne, int num)
        {
            bool estValide = true;
            int x = 0;

            while (x < grille.GetLength(1) && estValide)
            {
                if (x != colonne && grille[ligne, x] == num)
                {
                    estValide = false;
                }
                x++;
            }

            return estValide;
        }

        /// <summary>
        /// Vérifie si une valeur est valide dans la colonne donnée.
        /// </summary>
        /// <param name="grille">Grille Sudoku.</param>
        /// <param name="ligne">Indice de la ligne de la case.</param>
        /// <param name="colonne">Indice de la colonne.</param>
        /// <param name="num">Valeur à tester.</param>
        /// <returns>Vrai si la valeur n'existe pas dans la colonne sauf dans la case spécifiée, faux sinon.</returns>
        /// <author>Nordine Hida, Noah Mirbel</author>
        private bool EstValideDansColonne(int[,] grille, int ligne, int colonne, int num)
        {
            bool estValide = true;
            int y = 0;

            while (y < grille.GetLength(0) && estValide)
            {
                if (y != ligne && grille[y, colonne] == num)
                {
                    estValide = false;
                }
                y++;
            }

            return estValide;
        }

        /// <summary>
        /// Vérifie si une valeur est valide dans la sous-grille correspondante.
        /// </summary>
        /// <param name="grille">Grille Sudoku.</param>
        /// <param name="ligne">Indice de la ligne de la case.</param>
        /// <param name="colonne">Indice de la colonne de la case.</param>
        /// <param name="num">Valeur à tester.</param>
        /// <returns>Vrai si la valeur n'existe pas dans la sous-grille sauf dans la case spécifiée, faux sinon.</returns>
        /// <author>Nordine Hida, Noah Mirbel</author>
        private bool EstValideDansSousGrille(int[,] grille, int ligne, int colonne, int num)
        {
            bool estValide = true;
            int debutLigne = ligne - ligne % TAILLE_SOUS_GRILLE;
            int debutColonne = colonne - colonne % TAILLE_SOUS_GRILLE;
            int i = 0;
            

            while (i < TAILLE_SOUS_GRILLE && estValide)
            {
                int j = 0;
                while (j < TAILLE_SOUS_GRILLE && estValide)
                {
                    int currentLigne = debutLigne + i;
                    int currentColonne = debutColonne + j;

                    if ((currentLigne != ligne || currentColonne != colonne) && grille[currentLigne, currentColonne] == num)
                    {
                        estValide = false;
                    }
                    j++;
                }
                i++;
            }

            return estValide;
        }

        /// <summary>
        /// Met une certaine quantité de case à la valeur 0 tout
        /// en vérifiant qu'il n'y a bien qu'une solution
        /// </summary>
        /// <param name="grille">grille</param>
        /// <param name="nombresARetirer">nombre de case à mettre à 0</param>
        private void RetirerNombres(int[,] grille, int nombresARetirer)
        {
            while (nombresARetirer > 0)
            {
                int ligne = aleatoire.Next(TAILLE_GRILLE);
                int colonne = aleatoire.Next(TAILLE_GRILLE);
                if (grille[ligne, colonne] != 0)
                {
                    int temp = grille[ligne, colonne];
                    grille[ligne, colonne] = 0;
                    //Clonage de la grille nécessaire pour éviter de modifier
                    //celle utilisé par la fonction actuellement
                    int[,] grilleTemporaire = (int[,])grille.Clone();
                    nombresARetirer--;
                }
            }
        }

        /// <summary>
        /// Compte le nombre de solution de la grille
        /// Récursif.
        /// </summary>
        /// <param name="grille">grille</param>
        /// <returns>nombre de solution</returns>
        private int CompterSolutions(int[,] grille)
        {
            int res = 1;
            for (int i = 0; i < TAILLE_GRILLE; i++)
                for (int j = 0; j < TAILLE_GRILLE; j++)
                    if (grille[i, j] == 0)
                    {
                        int count = 0;
                        for (int num = 1; num <= TAILLE_GRILLE && count < 2; num++)
                        {
                            if (EstValide(grille, i, j, num))
                            {
                                grille[i, j] = num;
                                count += CompterSolutions(grille);
                            }
                        }
                        res = count;
                    }
            return res;
        }

        /// <summary>
        /// Convertis un tableau à deux dimensions int[,]
        /// en grille
        /// </summary>
        /// <param name="tableau">tableau int[,] à deux dimensions</param>
        /// <param name="modifiable">Si les cases non-vides sont modifiables ou non</param>
        /// <param name="graine">graine de génération de la grille</param>
        /// <returns>grille</returns>
        private Grille ConvertirEnGrille(int[,] tableau, bool modifiable, string graine = "")
        {
            Case[,] contenu = new Case[TAILLE_GRILLE, TAILLE_GRILLE];
            for (int i = 0; i < TAILLE_GRILLE; i++)
                for (int j = 0; j < TAILLE_GRILLE; j++)
                    contenu[i, j] = new Case(tableau[i, j], tableau[i, j] != 0 ? modifiable : true);
            return new Grille(contenu);
        }
        
        /// <summary>
        /// Convertit une partie en tableau à partir
        ///  de la grille contenu dans partie
        /// </summary>
        /// <param name="partie">partie</param>
        /// <returns>
        /// tableau double des valeurs des cases 
        /// de la grille de partie
        /// </returns>
        private int[,] ConvertirPartieEnTableau(Partie partie)
        {
            int[,] tableau = new int[TAILLE_GRILLE, TAILLE_GRILLE];
            for (int i = 0; i < TAILLE_GRILLE; i++)
                for (int j = 0; j < TAILLE_GRILLE; j++)
                    tableau[i, j] = partie.RecupererCaseGrille(i, j).Valeur;
            return tableau;
        }
    }
}
