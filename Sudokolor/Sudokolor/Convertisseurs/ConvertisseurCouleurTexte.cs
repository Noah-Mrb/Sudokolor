using Modele;
using Sudokolor.Pages;
using System.Globalization;

namespace Sudokolor.Convertisseurs
{
    /// <summary>
    /// Convertisseur de valeur à couleur
    /// </summary>
    /// <author>Valentin Colindre</author>
    public class ConvertisseurCouleurTexte : IValueConverter
    {
        /// <summary>
        /// Convertis une valeur en couleur : 
        /// transparent si le thème n'est pas chiffre
        /// blanc sinon
        /// </summary>
        /// <param name="value">valeur</param>
        /// <param name="targetType">Non-utilisé</param>
        /// <param name="parameter">page de partie</param>
        /// <param name="culture">Non-utilisé</param>
        /// <returns>la couleur</returns>
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            Color color = Colors.Transparent;
            if (value is int intValue && parameter is PagePartie page)
            {
                if (page.Theme == THEME_COULEUR.CHIFFRES)
                {
                    if (intValue != 0)
                        color = Colors.White;
                }
            }
            return color;
        }

        //Inutilisé
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
