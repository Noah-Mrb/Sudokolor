using CommunityToolkit.Mvvm.ComponentModel;
using Modele;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VueModele
{
    /// <summary>
    /// VueModele de la page des options de l'application
    /// </summary>
    /// <author>Romain Card</author>
    public partial class OptionsVueModele : ObservableObject
    {
        /// <summary>
        /// Liste des langues disponibles
        /// </summary>
        [ObservableProperty]
        private Dictionary<string, string> langues;

        /// <summary>
        /// Langue actuellement choisie par l'utilisateur
        /// </summary>
        [ObservableProperty]
        private string langueChoisie;

        /// <summary>
        /// Type d'affichage choisi par l'utilisateur pour le remplissage de la grille
        /// </summary>
        [ObservableProperty]
        private THEME_COULEUR themeChoisi;

        public OptionsVueModele()
        {
            langues = new Dictionary<string, string> {
                { "Français", "fr-FR" },
                { "English", "en-EN" },
            };
            themeChoisi = THEME_COULEUR.DEFAUT;
        }
    }
}
