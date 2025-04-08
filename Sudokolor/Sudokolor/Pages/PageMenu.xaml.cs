using VueModele;
﻿using System.Globalization;
using CommunityToolkit.Maui.Views;
using Sudokolor.Popups;
using Sudokolor.Resources.Strings;

namespace Sudokolor.Pages
{
    public partial class PageMenu : ContentPage
    {
        private MenuVueModele vm;

        public PageMenu(MenuVueModele vm)
        {
            //initialisation du VueModele
            this.vm = vm;
            InitialiserPreferencesUtilisateur();
            InitializeComponent();
            this.BindingContext = vm;
        }

        private void InitialiserPreferencesUtilisateur()
        {
            // Initialise la langue de l'application
            string langue = Preferences.Get("langue", "");
            if ("" != langue && langue != CultureInfo.CurrentCulture.Name)
            {
                var culture = new CultureInfo(langue);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
                CultureInfo.CurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.vm.MettreAJour();
            ChangerBoutonDifficulte(Preferences.Get("difficulte", 0));
        }

        /// <summary>
        /// Reprend une partie en cours
        /// </summary>
        /// <author>Romain CARD, Valentin Colindre</author>
        private async void ReprendrePartie(object sender, EventArgs e)
        {
            await this.LancerPartie();
        }

        private async void ContreLaMontre(object sender, EventArgs e)
        {
            if (this.vm.PartieEnCours)
            {
                this.LancerNouvellePartie(true);
            }
            else
            {
                await this.LancerPartie(true);
            }
        }

        /// <summary>
        /// Lance une nouvelle partie
        /// </summary>
        /// <author>Valentin Colindre</author>
        private async void NouvellePartie(object sender, EventArgs e)
        {
            this.LancerNouvellePartie();
        }

        private async void LancerNouvellePartie(bool contreLaMontre = false)
        {
            PopupCustomBooleen popup = new PopupCustomBooleen(
                AppResources.app_page_principale_abandon_popup,
                AppResources.app_page_principale_abandon_cancel,
                AppResources.app_page_principale_abandon_confirm
            );
            bool? garder = (bool?)(await this.ShowPopupAsync(popup));
            if (garder == false)
            {
                this.vm.EffacerPartie();
                await this.LancerPartie(contreLaMontre);
            }
        }

        //Change la difficulté dans les préférences de l'application
        private async void ChangerDifficulte(object sender, EventArgs e)
        {
            int valeur = Convert.ToInt32(((Button)sender).Text);

            Preferences.Set("difficulte", valeur);
            ChangerBoutonDifficulte(valeur);
        }

        //change le bouton selectionné dans les difficultés
        private void ChangerBoutonDifficulte(int difficulte)
        {
            foreach (Button bouton in GrilleDifficulte.Children)
            {
                if (bouton.Text == difficulte.ToString())
                {
                    bouton.Style = null;
                }
                else
                {
                    bouton.Style = (Style)Application.Current.Resources["Inactif"];
                }
            }
            
        }

        /// <summary>
        /// Lance la partie
        /// </summary>
        /// <author>Valentin Colindre</author>
        private async Task LancerPartie(bool contreLaMontre=false)
        {
            chargementBoutons(true);

            ShellNavigationQueryParameters parametres = RecupererParametres(contreLaMontre);

            await Shell.Current.GoToAsync("Partie", parametres);

            chargementBoutons(false);
        }

        /// <summary>
        /// Récupère les parametres du shell transmis pendant la navigation
        /// </summary>
        /// <author>Nordine HIDA</author>
        private ShellNavigationQueryParameters RecupererParametres(bool contreLaMontre=false)
        {
            ShellNavigationQueryParameters parametres = new();

            parametres["graine"] = vm.Graine;
            parametres["contreLaMontre"] = contreLaMontre;

            return parametres;
        }

        /// <summary>
        /// Ouvre la page des options
        /// </summary>
        /// <author>Romain CARD</author>
        private async void Options(object sender, EventArgs e)
        {
            chargementBoutons(true);

            await Shell.Current.GoToAsync("Options");

            chargementBoutons(false);
        }

        /// <summary>
        /// Ouvre la page de l'historique
        /// </summary>
        /// <author>Noah Mirbel</author>
        private async void OuvrirHistorique(object sender, EventArgs e)
        {
            chargementBoutons(true);

            await Shell.Current.GoToAsync("Historique");

            chargementBoutons(false);
        }


        /// <summary>
        /// Change l'état de chargement des boutons de 
        /// lancement
        /// </summary>
        /// <param name="etat">
        /// etat (vrai) = chargement
        /// etat (faux) = pas de chargement
        /// </param>
        private void chargementBoutons(bool etat)
        {
            BoutonLancerPartie.IsEnabled = !etat;
            BoutonReprendrePartie.IsEnabled = !etat;
            BoutonNouvellePartie.IsEnabled = !etat;
            BoutonOptions.IsEnabled = !etat;
            BoutonHistorique.IsEnabled = !etat;
            RondDeChargement.IsRunning = etat;
        }
    }
}