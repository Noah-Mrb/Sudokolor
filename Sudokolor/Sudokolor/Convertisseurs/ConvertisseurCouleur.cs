using Sudokolor.Pages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudokolor.Convertisseurs
{
    /// <summary>
    /// Convertisseur de valeur à couleur
    /// </summary>
    /// <author>Valentin Colindre</author>
    public class ConvertisseurCouleur : IValueConverter
    {
        /// <summary>
        /// Convertis une valeur en couleur
        /// via la méthode de la page.
        /// La valeur 0 rend la couleur partiellement transparente.
        /// </summary>
        /// <param name="value">valeur</param>
        /// <param name="targetType">Non-utilisé</param>
        /// <param name="parameter">page de partie</param>
        /// <param name="culture">Non-utilisé</param>
        /// <returns>la couleur</returns>
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int intValue && parameter is PagePartie page)
            {
                Color color = page.ObtenirCouleur(intValue);
                if (intValue == 0)
                    color = color.MultiplyAlpha(0.38f);
                return color;
            }
            return Colors.Transparent;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
