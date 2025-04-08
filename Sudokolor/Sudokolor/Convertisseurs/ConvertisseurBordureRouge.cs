using Modele;
using Outils;
using System.Globalization;


namespace Sudokolor.Convertisseurs
{
    /// <summary>
    /// Convertisseur détermiant la couleur de la bordure d'une case modifiable de la grille
    /// </summary>
    /// <author>Noah Mirbel</author>
    public class ConvertisseurBordureRouge: IValueConverter
    {
        /// <summary>
        /// Convertit un booléen en couleur selon son état
        /// vrai = transparent
        /// faux = couleur d'erreur
        /// defaut = transparent
        /// </summary>
        /// <param name="value">booléen</param>
        /// <param name="targetType">non utilisé</param>
        /// <param name="parameter">non utilisé</param>
        /// <param name="culture">non utilisées</param>
        /// <returns>Couleur d'erreur si le booléen est faux, sinon/par défaut transparent.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            Color couleur = Colors.Transparent;
            if (value is bool estValide && !estValide)
            {
                string theme = Preferences.Get("theme", "DEFAUT");
                THEME_COULEUR themeCouleur = (THEME_COULEUR)Enum.Parse(typeof(THEME_COULEUR), theme);
                couleur = Color.FromRgba("#" + GestionnaireCouleurs.ObtenirCouleurBordureErreur(themeCouleur));
            }
            return couleur;
        }

        // non implémentée car non nécessaire
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
