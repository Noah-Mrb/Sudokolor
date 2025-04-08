using Sudokolor.Resources.Strings;
using System.Globalization;

namespace Sudokolor.Convertisseurs
{
    /// <summary>
    /// Convertisseur de booleen en texte
    /// de mode de partie
    /// </summary>
    /// <author>Valentin Colindre</author>
    public class ConvertisseurBooleenMode : IValueConverter
    {
        /// <summary>
        /// Convertis un booleen en texte représentant le mode de partie
        /// Soit contre la montre
        /// soit classique
        /// </summary>
        /// <param name="value">booleen</param>
        /// <param name="targetType">Non-utilisé</param>
        /// <param name="parameter">Non utilisé</param>
        /// <param name="culture">Non-utilisé</param>
        /// <returns>texte représentant le mode</returns>
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            string texte = AppResources.app_page_principale_btn_classique;
            if (value is bool contreLaMontre)
            {
                if (contreLaMontre)
                    texte = AppResources.app_page_principale_btn_contre_la_montre;
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
