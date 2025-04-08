using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Modele;
using Modele.Exceptions;
using Outils;
using System.Collections.ObjectModel;

namespace VueModele
{
    /// <summary>
    /// VueModele d'une partie
    /// </summary>
    /// <author>Valentin Colindre</author>
    public partial class PartieVueModele : ObservableObject
    {
        //Taille limite de l'historique de modification
        private const int LIMITE_HISTORIQUE = 10000000;

        /// <summary>
        /// Nombre de cases maximales pour chaque couleur dans la grille
        /// (on peut dépasser cette valeur mais cela signifie que la grille est incorrecte)
        /// </summary>
        private const int NOMBRE_CASES_POUR_CHAQUE_COULEUR = 9;

        /// <summary>
        /// Cases de la partie
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<Case> contenu;

        /// <summary>
        /// Couleurs de la palette ainsi que le nombre de cases restantes
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<CouleurPalette> couleurs;

        public bool ContreLaMontre => partieModele.ContreLaMontre;

        /// <summary>
        /// Couleur actuellement sélectionnée par l'utilisateur pour jouer
        /// </summary>
        [ObservableProperty]
        private int couleurActive;

        private DateTime tempsDebut;


        private THEME_COULEUR themeCouleur;

        /// <summary>
        /// Thème de couleur sélectionné par l'utilisateur
        /// </summary>
        public THEME_COULEUR ThemeCouleur => themeCouleur;

        /// <summary>
        /// Difficulté de la partie
        /// </summary>
        public DIFFICULTE Difficulte => partieModele.Difficulte;

        private Partie partieModele;

        [ObservableProperty]
        private string graine;

        private IGenerateurGrille generateur;

        private ISauvegardePartie sauvegarde;

        private ISauvegardeHistorique sauvegardeHistorique;

        private ICalculateurScore calculateurScore;

        // indique si l'utilisateur est en mode indice ou non
        [ObservableProperty]
        private bool modeIndice;

        [ObservableProperty]
        private bool toutesLesCasesSontRemplies;


        /// <summary>
        /// Créer le VueModele d'une partie
        /// </summary>
        /// <param name="generateur">générateur à utiliser</param>
        /// <param name="sauvegarde">gestionnaire de sauvegarde de partie</param>
        /// <param name="sauvegardeHistorique">gestionnaire de sauvegarde de partie dans l'historique</param>
        public PartieVueModele(
            IGenerateurGrille generateur, 
            ISauvegardePartie sauvegarde, 
            ICalculateurScore calculateurScore,
            ISauvegardeHistorique sauvegardeHistorique
            ) 
        {
            this.calculateurScore = calculateurScore;
            this.sauvegarde = sauvegarde;
            this.generateur = generateur;
            this.sauvegardeHistorique = sauvegardeHistorique;
            Contenu = new ObservableCollection<Case>();
            Couleurs = new ObservableCollection<CouleurPalette>();
            CouleurActive = 0;
            themeCouleur = THEME_COULEUR.DEFAUT;
            tempsDebut = DateTime.Now;
            ModeIndice = false;
        }

        /// <summary>
        /// Initialise la partie
        /// </summary>
        /// <param name="graine">graine utilisée pour la partie</param>
        public void InitialiserPartie(string graine, DIFFICULTE difficulte, bool contreLaMontre)
        {
            //On vérifie si une sauvegarde existe
            if (this.sauvegarde.PartieEnCours())
            {
                //On la charge si c'est le cas
                partieModele = this.sauvegarde.ChargerPartie();
            }
            else
            {
                //Sinon : nouvelle partie

                // Génére une graine si elle est null/vide
                if (String.IsNullOrEmpty(graine))
                    graine = GenererUneGraine();
                //Création de la nouvelle partie
                partieModele = ConfigurateurPartie.ConfigurerPartie(
                    difficulte,
                    graine,
                    this.generateur,
                    contreLaMontre
                    );
            }
            //Initialisation du contenu de la grille, graine et des couleurs
            this.Graine = partieModele.Graine;
            DefinirCouleurs();

            ActualiserContenu();
            this.Sauvegarde();
            this.VerifierCasesAvecErreur();
        }

        /// <summary>
        /// Initialise le theme du vueModele
        /// 
        /// </summary>
        /// <param name="themeCouleur"></param>
        public void InitialiserTheme(THEME_COULEUR themeCouleur)
        {
            this.themeCouleur = themeCouleur;
            this.DefinirCouleurs();
        }


        // Génère une graine aléatoire 
        // Chaine de caractère en majuscule de 10 à 15 caractère 
        private string GenererUneGraine()
        {
            // Définir les caractères utilisables (uniquement les lettres majuscules de l'alphabet).
            const string lettres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            Random random = new Random();
            // Déterminer une longueur aléatoire pour le code, entre 10 et 15.
            int longueur = random.Next(10, 16);

            // Construire le code en sélectionnant des caractères aléatoires.
            char[] code = new char[longueur];
            for (int i = 0; i < longueur; i++)
            {
                code[i] = lettres[random.Next(lettres.Length)];
            }

            // Convertir le tableau de caractères en chaîne et la retourner.
            return new string(code);
        }



        // Change la couleur d'une case
        // suivant la difficulté, vérifie si elle est juste
        // Et vérifie si toutes les cases sont pleines pour l'affichage de la popup
        [RelayCommand]
        private void ChangerCouleur(int indexCase)
        {
            if (indexCase >= Contenu.Count() || indexCase < 0)
                throw new HorsLimiteException("L'index de la case est invalide.");

            if (Contenu[indexCase].EstModifiable)
            {
                Case caseEnCours = Contenu[indexCase];

                if (ModeIndice && Difficulte != DIFFICULTE.DIFFICILE)
                {
                    RevelerIndice(indexCase);
                }
                else
                {
                    // On ajoute l'action à l'historique
                    partieModele.AjouterAction([indexCase, caseEnCours.Valeur]);
                    if (partieModele.ObtenirTailleHistorique() > LIMITE_HISTORIQUE)
                    {
                        partieModele.RetirerAction(0);
                    }

                    // Gestion des couleurs et du nombre de cases restantes
                    MettreAJourCompteurCouleurs(caseEnCours.Valeur, CouleurActive);
                    MettreAJourValeurCase(caseEnCours);
                    ActualiserFaussesCases(caseEnCours);
                }
                this.VerifierCasesAvecErreur();
                this.partieModele.AjouterNombreActions(1);

                this.Sauvegarde();
            }
            else
            {
                throw new CaseNonModifiableException("Case non modifiable.");
            }

            ToutesLesCasesSontRemplies = this.ToutesLesCasesRemplies();
        }

        private void ActualiserFaussesCases(Case caseEnCours)
        {
            if (partieModele.Difficulte == DIFFICULTE.NORMAL || partieModele.Difficulte == DIFFICULTE.FACILE)
            {
                caseEnCours.EstValide = true;
            }
        }

        // Gère la logique de changement de couleur pour une case donnée
        private void MettreAJourValeurCase(Case caseEnCours)
        {
            if (caseEnCours.Valeur == CouleurActive)
            {
                // Si la case a déjà la valeur de la couleur active, on efface la couleur (en mettant 0)
                caseEnCours.Valeur = 0;
            }
            else if (caseEnCours.Valeur != 0)
            {
                // Si on remplace une autre couleur
                caseEnCours.Valeur = CouleurActive;
            }
            else
            {
                // Sinon on applique la couleur active
                caseEnCours.Valeur = CouleurActive;
            }
        }

        // Gère la logique de changement de couleur pour  la mise à jour des compteurs de couleurs
        private void MettreAJourCompteurCouleurs(int ancienneValeur, int nouvelleValeur)
        {
            if (ancienneValeur == nouvelleValeur)
            {
                // Si la case a déjà la valeur de la couleur active, on incrémente le compteur
                Couleurs[ancienneValeur].NombreRestant++;
            }
            else if (ancienneValeur != 0)
            {
                // Si on remplace une autre couleur, on ajuste les compteurs
                Couleurs[ancienneValeur].NombreRestant++;
                Couleurs[nouvelleValeur].NombreRestant--;
            }
            else
            {
                // Sinon on décrémente le compteur
                Couleurs[nouvelleValeur].NombreRestant--;
            }
        }


        // Révèle la valeur d'une case et met à jour les compteurs de couleurs
        private void RevelerIndice(int indexCase)
        {
            int ligne = indexCase / Grille.TAILLE_GRILLE;
            int colonne = indexCase % Grille.TAILLE_GRILLE;

            Case caseEnCours = Contenu[indexCase];

            int ancienneCouleur = caseEnCours.Valeur;

            caseEnCours.Valeur = generateur.ObtenirSolutionCase(ligne, colonne, graine);
            caseEnCours.EstModifiable = false;

            if (ancienneCouleur != caseEnCours.Valeur)
                MettreAJourCompteurCouleurs(ancienneCouleur, caseEnCours.Valeur);

            this.partieModele.AjouterNombreAides(1);
        }


        // Annule la dernière modification effectuée sur la grille
        [RelayCommand]
        private void Retour()
        {
            if (partieModele.ObtenirTailleHistorique() > 0)
            {
                int tailleHistorique = partieModele.ObtenirTailleHistorique();
                int[] modification = partieModele.ObtenirAction(tailleHistorique - 1);

                // tant que l'action précédente concerne une case verrouillée, on passe à la suivante
                bool trouve = false;
                while (tailleHistorique > 0 && !trouve)
                {
                    tailleHistorique--;
                    modification = partieModele.ObtenirAction(tailleHistorique);
                    partieModele.RetirerAction(tailleHistorique);
                    trouve = Contenu[modification[0]].EstModifiable;
                }

                if (Contenu[modification[0]].EstModifiable)
                {
                    this.partieModele.AjouterNombreActions(1);
                    
                    ActualiserFaussesCases(Contenu[modification[0]]);

                    //Mise a jour du nombre de cases restantes avec chaque couleur
                    MettreAJourCompteurCouleurs(Contenu[modification[0]].Valeur, modification[1]);

                    Contenu[modification[0]].Valeur = modification[1];

                    this.Sauvegarde();
                    ToutesLesCasesSontRemplies = this.ToutesLesCasesRemplies();
                }
                VerifierCasesAvecErreur();
            }
        }


        // Change la couleur active
        [RelayCommand]
        private void ChangerCouleurActive(int valeur) { CouleurActive = valeur; }


        // Actualise le contenu de la collection observable
        private void ActualiserContenu()
        {
            Contenu.Clear();
            for (int i = 0; i < partieModele.RecupererTailleGrille(0); i++)
            {
                for (int j = 0; j < partieModele.RecupererTailleGrille(1); j++)
                {
                    Contenu.Add(partieModele.RecupererCaseGrille(i, j));
                }
            }
            ToutesLesCasesSontRemplies = this.ToutesLesCasesRemplies();
        }

        // Active ou désactive le mode indice
        [RelayCommand]
        private void IndiceMode()
        {
            ModeIndice = !ModeIndice;
        }

        /// <summary>
        /// Renvoie la couleur liée à la valeur fournie
        /// </summary>
        /// <param name="valeur">valeur</param>
        /// <returns>couleur en code hex</returns>
        public string ObtenirCouleur(int valeur)
        {
            return GestionnaireCouleurs.ObtenirCouleur(valeur, themeCouleur);
        }

        /// <summary>
        /// Renvoi le temps de la partie sous forme de texte
        /// </summary>
        /// <returns>temps formatté heures:minutes:jours</returns>
        public TimeSpan ObtenirTemps()
        {
            TimeSpan tempsEcoule;
            if (this.partieModele.ContreLaMontre)
            {
                tempsEcoule = this.partieModele.Temps - (DateTime.Now - tempsDebut);
            }
            else
            {
                tempsEcoule = (DateTime.Now - tempsDebut) + this.partieModele.Temps;
                
            }
            return tempsEcoule;
        }

        /// <summary>
        /// Renvoi le score de la partie actuelle
        /// </summary>
        /// <returns>score entier</returns>
        public int ObtenirScore()
        {
            int score = 0;
            if (this.partieModele.ContreLaMontre)
            {
                score = this.calculateurScore.CalculerScoreContreLaMontre(
                    this.partieModele.Temps - (DateTime.Now - tempsDebut),
                    this.partieModele.NombreActions,
                    this.partieModele.NombreAides
                    );
            }
            else
            {
                score = this.calculateurScore.CalculerScoreNormal(
                (DateTime.Now - tempsDebut) + this.partieModele.Temps,
                this.partieModele.NombreActions,
                this.partieModele.NombreAides
                );
            }
            return score;
        }


        // Met à jour la liste des couleurs du thème
        private void DefinirCouleurs()
        {
            Couleurs.Clear();
            MettreAJourNombreCasesRestantes();
        }

        // Vérifie si toutes les cases on été remplie
        private bool ToutesLesCasesRemplies()
        {
            return Contenu.All(c => c.Valeur != 0);
        }

        /// <summary>
        /// Vérifie si la grille soumise est correct
        /// </summary>
        /// <returns>renvoie la valeur de la vérification</returns>
        /// <author>Noah Mirbel</author>
        public bool VerifierGrille()
        {
            this.VerifierCasesAvecErreur(this.partieModele.Difficulte == DIFFICULTE.NORMAL);
            this.partieModele.AjouterNombreAides(1);
            return generateur.VerifierGrille(partieModele);
        }


        /// <summary>
        /// Sauvegarde la partie en cours
        /// </summary>
        /// <author>Valentin</author>
        public void Sauvegarde()
        {
            if (this.partieModele != null && this.sauvegarde != null)
            {
                if(this.partieModele.ContreLaMontre)
                    partieModele.SoustraireTempsPartie(DateTime.Now - tempsDebut);
                else
                    partieModele.AjouterTempsPartie(DateTime.Now - tempsDebut);
                tempsDebut = DateTime.Now;
                this.sauvegarde.SauvegarderPartie(this.partieModele);
            }
        }

        public void SauvegardeHistorique()
        {
            if (this.partieModele != null)
            {
                PartieHistorique partieHistorique = new PartieHistorique(Difficulte, graine, ObtenirScore(), this.partieModele.ContreLaMontre);
                this.sauvegardeHistorique.SauvegarderPartie(partieHistorique);
            }
        }

        /// <summary>
        /// Termine la partie en cours en la supprimant
        /// de la mémoire
        /// </summary>
        public void TerminerPartie()
        {
            if (
                !this.partieModele.ContreLaMontre ||
                (this.partieModele.ContreLaMontre && ObtenirTemps().TotalSeconds > 0)
            )
            {
                this.SauvegardeHistorique();
            }
            if (this.sauvegarde.PartieEnCours())
                this.sauvegarde.EffacerPartie();
        }

        #region Gestion du nombre de cases restantes par couleur

        /// <summary>
        /// Met à jour la liste des couleurs de la palette avec 
        /// le nouveau nombre de cases restante pour chaque valeur.
        /// </summary>
        /// <author>Nordine</author>
        private void MettreAJourNombreCasesRestantes()
        {
            Couleurs.Clear();
            int[] tableauNombreRestant = RecupererNombreCasesRestantes();
            for (int i = 0; i <= NOMBRE_CASES_POUR_CHAQUE_COULEUR; i++)
            {
                int nombreRestant = i > 0 ? tableauNombreRestant[i - 1] : 0;
                Couleurs.Add(new CouleurPalette(i, nombreRestant));
            }
        }

        /// <summary>
        /// Récupère le tableau contenant le nombre de cases restantes pour chaque couleur.
        /// </summary>
        /// <returns>Tableau où chaque élément représente les cases restantes pour une couleur donnée.</returns>
        /// <author>Nordine</author>
        private int[] RecupererNombreCasesRestantes()
        {
            int[] occurrences = new int[NOMBRE_CASES_POUR_CHAQUE_COULEUR];

            // Calculer les occurrences actuelles des couleurs dans la grille.
            CalculerOccurrencesActuelles(occurrences);

            // Calculer les cases restantes en soustrayant les occurrences actuelles.
            return CalculerCasesRestantes(occurrences);
        }

        /// <summary>
        /// Parcourt la grille et met à jour les occurrences des couleurs dans le tableau.
        /// </summary>
        /// <param name="occurrences">Tableau des occurrences à mettre à jour.</param>
        /// <author>Nordine</author>
        private void CalculerOccurrencesActuelles(int[] occurrences)
        {
            foreach (Case caseItem in Contenu)
            {
                // Ignorer les cases vides (valeur = 0).
                if (caseItem.Valeur != 0)
                {
                    occurrences[caseItem.Valeur - 1]++; // Décaler pour correspondre à l'index du tableau.
                }
            }
        }

        /// <summary>
        /// Calcule le tableau des cases restantes pour chaque couleur en comparant
        /// les occurrences actuelles avec le maximum attendu.
        /// </summary>
        /// <param name="occurrences">Tableau contenant les occurrences actuelles.</param>
        /// <returns>Tableau des cases restantes pour chaque couleur.</returns>
        /// <author>Nordine</author>
        private int[] CalculerCasesRestantes(int[] occurrences)
        {
            int[] casesRestantes = new int[NOMBRE_CASES_POUR_CHAQUE_COULEUR];

            for (int i = 0; i < occurrences.Length; i++)
            {
                // Calculer les cases restantes pour chaque couleur.
                casesRestantes[i] = NOMBRE_CASES_POUR_CHAQUE_COULEUR - occurrences[i];
            }

            return casesRestantes;
        }

        #endregion


        //Vérifie les cases de la grille pour d'éventuelles erreurs
        //et change leur état EstValide en conséquence
        private void VerifierCasesAvecErreur(bool forcer = false)
        {
            if (this.partieModele.Difficulte == DIFFICULTE.FACILE || forcer)
            {
                List<(int, int)> listDesErreurs = generateur.RecupererErreurs(partieModele);

                List<int> indexCases = new List<int>();
                foreach (var (ligne, colonne) in listDesErreurs)
                {
                    indexCases.Add(ligne * 9 + colonne);

                }
                for (int i = 0; i < Contenu.Count; i++)
                {
                    Contenu[i].EstValide = !indexCases.Contains(i);
                }
            }
        }
    }
}
