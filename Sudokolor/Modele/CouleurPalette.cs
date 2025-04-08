using CommunityToolkit.Mvvm.ComponentModel;

namespace Modele
{
    /// <summary>
    /// Une couleur de la palette
    /// Associe la valeur à son nombre de cases restantes possible avec cette valeur
    /// </summary>
    /// <author>Valentin / Nordine</author>
    public partial class CouleurPalette : ObservableObject
    {
        /// <summary>
        /// Constructeur de la couleur de la palette
        /// </summary>
        /// <param name="valeur">Valeur de la couleur</param>
        /// <param name="nombreRestant">son nombre de cases restantes</param>
        public CouleurPalette(int valeur, int nombreRestant) 
        { 
            this.valeur = valeur;
            this.nombreRestant = nombreRestant;
        }

        /// <summary>
        /// valeur de la couleur
        /// </summary>
        [ObservableProperty]
        private int valeur;

        /// <summary>
        /// Nombre de cases restantes possible avec cette valeur
        /// Nombre max de cases possible - nombre sur la grille.
        /// </summary>
        [ObservableProperty]
        private int nombreRestant;
    }
}
