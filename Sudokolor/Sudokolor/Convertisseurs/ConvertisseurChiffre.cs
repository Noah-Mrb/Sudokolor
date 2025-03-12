using Modele;
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
    /// Convertisseur de valeur à chiffre texte
    /// </summary>
    /// <author>Valentin Colindre</author>
    public class ConvertisseurChiffre : IValueConverter
    {
        /// <summary>
        /// Convertis une valeur en texte selon le thème
        /// </summary>
        /// <param name="value">valeur</param>
        /// <param name="targetType">Non-utilisé</param>
        /// <param name="parameter">page de partie</param>
        /// <param name="culture">Non-utilisé</param>
        /// <returns>la couleur</returns>
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            string texte = "";
            if (value is int intValue && parameter is PagePartie page)
            {
                if (page.Theme == THEME_COULEUR.CHIFFRES)
                {
                    if (intValue != 0)
                        texte = intValue.ToString();
                }
            }
            return texte;
        }

        //Inutilisé
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
