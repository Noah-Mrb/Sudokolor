using Modele;
using System.Globalization;

namespace Sudokolor.Convertisseurs
{
    /// <summary>
    /// Convertisseur de difficulte en etoile texte
    /// </summary>
    /// <author>Valentin Colindre</author>
    public class ConvertisseurEtoile : IValueConverter
    {
        /// <summary>
        /// Convertis une difficulte en une ou plusieurs etoiles (string)
        /// </summary>
        /// <param name="value">difficulte</param>
        /// <param name="targetType">Non-utilisé</param>
        /// <param name="parameter">Non utilisé</param>
        /// <param name="culture">Non-utilisé</param>
        /// <returns>texte d'etoile</returns>
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            string texte = "";
            if (value is DIFFICULTE nombreEtoile)
            {
                for (int i = 0; i <= System.Convert.ToInt32(nombreEtoile); i++)
                    texte += "★";
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
