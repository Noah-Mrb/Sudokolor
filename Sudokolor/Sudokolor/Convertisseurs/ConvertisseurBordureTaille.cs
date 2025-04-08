using System.Globalization;

namespace Sudokolor.Convertisseurs
{
    /// <summary>
    /// Convertisseur déterminant la taille de la bordure de la couleur active
    /// </summary>
    /// <author>Valentin Colindre</author>
    public class ConvertisseurBordureTaille: IValueConverter
    {
        /// <summary>
        /// Convertit un entier (valeur de couleur)
        /// en entier correspondant à une taille (taille de bordure), 
        /// s'il est égal à la couleur active de la page
        /// alors l'entier sera 3
        /// sinon 0
        /// </summary>
        /// <param name="value">booléen</param>
        /// <param name="targetType">non utilisé</param>
        /// <param name="parameter">page de la partie</param>
        /// <param name="culture">non utilisées</param>
        /// <returns>3 si couleur active, sinon 0.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            const int TAILLE_SELECTION = 3;
            const int TAILLE_NORMALE = 0;
            int taille = TAILLE_NORMALE;
            if (value is int valeurActive && parameter is Button boutonPalette && boutonPalette.Text!="")
            {
                int valeurPalette = System.Convert.ToInt32(boutonPalette.Text);
                taille = valeurActive == valeurPalette ? TAILLE_SELECTION : TAILLE_NORMALE;
            }
            return taille;
        }

        // non implémentée car non nécessaire
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
