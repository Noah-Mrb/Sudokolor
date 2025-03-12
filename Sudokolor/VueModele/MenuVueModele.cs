using CommunityToolkit.Mvvm.ComponentModel;
using Outils;
using System.ComponentModel;

namespace VueModele
{
    /// <summary>
    /// Vue Modèle du menu principal
    /// Regroupe l'ensemble des paramètres commun aux autres Vues
    /// </summary>
    /// <author>Nordine HIDA, Valentin Colindre</author>
    public partial class MenuVueModele : ObservableObject, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        private ISauvegardePartie sauvegarde;


        /// <summary>
        /// graine saisie sur le menu et utilisé pour créer la partie
        /// </summary>
        /// <author>Nordine HIDA</author>
        [ObservableProperty]
        private string graine = string.Empty;

        /// <summary>
        /// Si une partie est en cours
        /// </summary>
        /// <author>Valentin Colindre</author>
        public bool PartieEnCours => this.sauvegarde.PartieEnCours();

        /// <summary>
        /// Créer le vuemodele 
        /// avec le système de sauvegarde
        /// </summary>
        /// <param name="sauvegarde">système de sauvegarde</param>
        /// <author>Valentin Colindre</author>
        public MenuVueModele(ISauvegardePartie sauvegarde)
        {
            this.sauvegarde = sauvegarde;
        }

        /// <summary>
        /// Efface la partie en cours
        /// </summary>
        /// <author>Valentin Colindre</author>
        public void EffacerPartie()
        {
            this.sauvegarde.EffacerPartie();
            MettreAJour();
        }

        /// <summary>
        /// Signale une mise à jour du vuemodele
        /// </summary>
        /// <author>Valentin Colindre</author>
        public void MettreAJour()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }


    }
}
