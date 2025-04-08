using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Modele;
using Outils;
using System.Collections.ObjectModel;

namespace VueModele
{
    /// <summary>
    /// VueModele d'un historique
    /// </summary>
    public partial class HistoriqueVueModele: ObservableObject
    { 

        /// <summary>
        /// parties dans l'historique
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<PartieHistorique> partiesHistorique;

        /// <summary>
        /// Si l'historique n'est pas vide
        /// </summary>
        [ObservableProperty]
        private bool historiqueNonVide;

        /// <summary>
        /// la partie selectionnee dont on peut voir les details
        /// </summary>
        [ObservableProperty]
        private PartieHistorique? partieSelectionnee;

        /// <summary>
        /// determine si le boutons des details de la partie est visible
        /// </summary>
        [ObservableProperty]
        private bool detailsVisible;

        /// <summary>
        /// Créer le VueModele de l'historique
        /// </summary>
        public HistoriqueVueModele(ISauvegardeHistorique sauvegardeHistorique)
        {
            DetailsVisible = false;
            PartieSelectionnee = null;
            PartiesHistorique = new ObservableCollection<PartieHistorique>(
                sauvegardeHistorique.ChargerHistorique()
                .OrderByDescending(x => x.Score)
                .ToList()
            );
            HistoriqueNonVide = PartiesHistorique.Any();
        }

        /// <summary>
        /// determine si l'on affiche le bouton des details de la partie
        /// et donne les informations a y mettre
        /// </summary>
        /// <param name="graine">graine de la partie sélectionnée</param>
        [RelayCommand]
        private void SelectionnerPartie(string graine)
        {
            if (PartieSelectionnee != null && graine == PartieSelectionnee.Graine)
                DetailsVisible = !DetailsVisible;
            else
            {
                PartieSelectionnee = PartiesHistorique.First(x => x.Graine == graine);
                DetailsVisible = true;
            }
        }
    }
}
