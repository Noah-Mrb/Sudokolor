using Sudokolor.Pages;
using System.Globalization;
using Modele;

namespace Sudokolor.Convertisseurs
{
    /// <summary>
    /// Convertisseur de valeur à booléen
    /// </summary>
    /// <author>Valentin Colindre</author>
    public class ConvertisseurCompteurVisible : IValueConverter
    {
        /// <summary>
        /// Convertis une valeur en booléen de visibilité
        /// Vrai si la valeur est supérieure à 0
        /// </summary>
        /// <param name="value">valeur de la case actuelle</param>
        /// <param name="targetType">Non-utilisé</param>
        /// <param name="parameter">page de partie</param>
        /// <param name="culture">Non-utilisé</param>
        /// <returns>vrai pour toutes les valeurs sauf 0</returns>
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool visibilite = false;
            if (value is int intValue && parameter is PagePartie page)
            {
                //on affiche pas en mode difficile
                if (page.Difficulte != DIFFICULTE.DIFFICILE)
                    visibilite = intValue > 0;
            }
            return visibilite;
        }

        //Inutilisé
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
