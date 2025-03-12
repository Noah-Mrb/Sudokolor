using VueModele;
﻿using System.Globalization;
using Outils;
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
        }

        /// <summary>
        /// Reprend une partie en cours
        /// </summary>
        /// <author>Romain CARD, Valentin Colindre</author>
        private async void ReprendrePartie(object sender, EventArgs e)
        {
            await this.LancerPartie();
        }

        /// <summary>
        /// Lance une nouvelle partie
        /// </summary>
        /// <author>Valentin Colindre</author>
        private async void NouvellePartie(object sender, EventArgs e)
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
                this.LancerPartie();
            }
        }

        /// <summary>
        /// Lance la partie
        /// </summary>
        /// <author>Valentin Colindre</author>
        private async Task LancerPartie()
        {
            chargementBoutons(true);

            ShellNavigationQueryParameters parametres = RecupererParametres();

            await Shell.Current.GoToAsync("Partie", parametres);

            chargementBoutons(false);
        }

        /// <summary>
        /// Récupère les parametres du shell transmis pendant la navigation
        /// </summary>
        /// <author>Nordine HIDA</author>
        private ShellNavigationQueryParameters RecupererParametres()
        {
            ShellNavigationQueryParameters parametres = new();

            parametres["graine"] = vm.Graine;

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
            RondDeChargement.IsRunning = etat;
        }
    }
}