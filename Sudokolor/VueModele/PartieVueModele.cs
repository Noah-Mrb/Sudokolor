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
        /// Cases de la partie
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<Case> contenu;

        /// <summary>
        /// Couleurs de la palette
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<int> couleurs;

        /// <summary>
        /// Couleur actuellement sélectionnée par l'utilisateur pour jouer
        /// </summary>
        [ObservableProperty]
        private int couleurActive;


        private THEME_COULEUR themeCouleur;

        /// <summary>
        /// Thème de couleur sélectionné par l'utilisateur
        /// </summary>
        public THEME_COULEUR ThemeCouleur => themeCouleur;

        private Partie partieModele;

        [ObservableProperty]
        private string graine;

        private IGenerateurGrille generateur;

        private ISauvegardePartie sauvegarde;

        [ObservableProperty]
        private bool toutesLesCasesSontRemplies;
       
        /// <summary>
        /// Créer le VueModele d'une partie
        /// </summary>
        /// <param name="generateur">générateur à utiliser</param>
        /// <param name="sauvegarde">gestionnaire de sauvegarde de partie</param>
        public PartieVueModele(IGenerateurGrille generateur, ISauvegardePartie sauvegarde) 
        {
            this.sauvegarde = sauvegarde;
            this.generateur = generateur;
            Contenu = new ObservableCollection<Case>();
            Couleurs = new ObservableCollection<int>();
            CouleurActive = 0;
            themeCouleur = THEME_COULEUR.DEFAUT;
        }

        /// <summary>
        /// Initialise la partie
        /// </summary>
        /// <param name="graine">graine utilisée pour la partie</param>
        public void InitialiserPartie(string graine)
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
                partieModele = new Partie(this.generateur.GenererGrille(graine), new(), graine);
            }
            //Initialisation du contenu de la grille, graine et des couleurs
            this.Graine = partieModele.Graine;
            DefinirCouleurs();
            ActualiserContenu();
            this.Sauvegarde();
        }

        /// <summary>
        /// Initialise le theme du vueModele
        /// </summary>
        /// <param name="themeCouleur"></param>
        public void InitialiserTheme(THEME_COULEUR themeCouleur)
        {
            this.themeCouleur = themeCouleur;
            this.DefinirCouleurs();
        }

        /// <summary>
        /// Génère une graine aléatoire 
        /// Chaine de caractère en majuscule de 10 à 15 caractère 
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Change la couleur d'une case
        /// Et vérifie si toutes les cases sont pleines pour l'affichage de la popup
        /// </summary>
        /// <param name="indexCase">Indice de la case (entre 0 et 80)</param>
        /// <exception cref="HorsLimiteException">Si l'index est invalide</exception>
        /// <exception cref="CaseNonModifiableException">Si la case n'est pas modifiable</exception>
        [RelayCommand]
        private void ChangerCouleur(int indexCase) 
        {
            if (indexCase >= Contenu.Count() || indexCase < 0)
                throw new HorsLimiteException("L'index de la case est invalide.");
            if (Contenu[indexCase].EstModifiable)
            {
                partieModele.AjouterAction(new int[2] { indexCase, Contenu[indexCase].Valeur });
                if(partieModele.ObtenirTailleHistorique() > LIMITE_HISTORIQUE)
                {
                    partieModele.RetirerAction(0);
                }
                Contenu[indexCase].Valeur = (0 != CouleurActive && CouleurActive == Contenu[indexCase].Valeur) ? 0 : CouleurActive;
                this.Sauvegarde();
            }
            else
            {
                throw new CaseNonModifiableException("Case non modifiable.");
            }

            ToutesLesCasesSontRemplies = this.ToutesLesCasesRemplies();
        }

        /// <summary>
        /// Annule la dernière modification effectuée sur la grille
        /// </summary>
        [RelayCommand]
        private void Retour()
        {
            if (partieModele.ObtenirTailleHistorique() > 0)
            {
                int[] modification = partieModele.ObtenirAction(partieModele.ObtenirTailleHistorique() - 1);
                Contenu[modification[0]].Valeur = modification[1];
                partieModele.RetirerAction(partieModele.ObtenirTailleHistorique() - 1);
                this.Sauvegarde();
                ToutesLesCasesSontRemplies = this.ToutesLesCasesRemplies();
            }
        }

        /// <summary>
        /// Change la couleur active
        /// </summary>
        /// <param name="valeur">nouvelle couleur</param>
        [RelayCommand]
        private void ChangerCouleurActive(int valeur){ CouleurActive = valeur; }

        /// <summary>
        /// Actualise le contenu de la collection observable
        /// </summary>
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
        /// Met à jour la liste des couleurs du thème
        /// </summary>
        /// <author>Romain Card</author>
        private void DefinirCouleurs()
        {
            Couleurs.Clear();
            for (int i = 0; i <= 9; i++)
            {
                Couleurs.Add(i);
            }
        }

        /// <summary>
        /// Vérifie si toutes les cases on été remplie
        /// </summary>
        /// <author>Noah Mirbel</author>
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
            return generateur.VerifierGrille(partieModele);
        }


        /// <summary>
        /// Sauvegarde la partie en cours
        /// </summary>
        private async Task Sauvegarde()
        {
            if (this.partieModele != null && this.sauvegarde != null)
            {
                this.sauvegarde.SauvegarderPartie(this.partieModele);
            }
        }
    }
}
