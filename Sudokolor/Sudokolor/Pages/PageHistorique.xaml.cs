using CommunityToolkit.Maui.Views;
using Sudokolor.Popups;
using Sudokolor.Resources.Strings;
using VueModele;

namespace Sudokolor.Pages
{
    public partial class PageHistorique : ContentPage
    {

        private HistoriqueVueModele? vm;

        private bool popupActive;

        /// <summary>
        /// Constructeur de la page de l'historique
        /// </summary>
        /// <param name="vm">vuemodele passé par injection de dépendance</param>
        /// <author>Noah Mirbel</author>
        public PageHistorique(HistoriqueVueModele vm)
        {
            this.vm = vm;
            this.popupActive = false;
            InitializeComponent();
            BindingContext = this.vm;
        }



        // copie la graine dans le presse-papier et affiche une popup si la copie a reussi
        private async void CopierGrainePressePapier(object sender, EventArgs e)
        {
            string texte = this.vm.PartieSelectionnee.Graine;
            if (!string.IsNullOrEmpty(texte))
            {
                CopierDansPressePapier(texte);
                if (!popupActive)
                    AfficherPopUpSuccesCopie();
            }
        }


        // copie dans le presse papier le texte passe en parametre
        private async void CopierDansPressePapier(string texte)
        {
            await Clipboard.Default.SetTextAsync(texte);
        }

        // Affiche une popup pour indiquer que la copie a ete effectuee
        private async void AfficherPopUpSuccesCopie()
        {
            PopupCustomTexteEtOk popup = new(AppResources.app_page_jeu_confirmation);
            popupActive = true;
            await this.ShowPopupAsync(popup);
            popupActive = false;
        }

        /// <summary>
        /// Renvoi vers le menu principal
        /// </summary>
        /// <author>Noah Mirbel</author>
        private async void RetourAuMenu(object sender, EventArgs e)
        {
            BoutonRetourMenu.IsEnabled = false;
            RondDeChargement.IsRunning = true;
            await Shell.Current.GoToAsync("..");
            BoutonRetourMenu.IsEnabled = true;
            RondDeChargement.IsRunning = false;
        }
    }
}