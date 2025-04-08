using System.Globalization;


namespace Sudokolor.Convertisseurs
{
    /// <summary>
    /// Convertisseur détermiant la couleur de la bordure de la couleur active
    /// </summary>
    /// <author>Valentin Colindre</author>
    public class ConvertisseurBordureCouleur: IValueConverter
    {
        /// <summary>
        /// Convertit un entier en couleur, s'il
        /// est égal à la couleur active de la page
        /// alors la couleur sera blanche
        /// sinon transparent
        /// </summary>
        /// <param name="value">entier de la couleur active</param>
        /// <param name="targetType">non utilisé</param>
        /// <param name="parameter">entier à comparer</param>
        /// <param name="culture">non utilisées</param>
        /// <returns>Couleur blanche si couleur active, sinon transparent.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            Color couleur = Colors.Transparent;
            if (value is int valeurActive && parameter is Button boutonPalette && boutonPalette.Text!="")
            {
                int valeurPalette = System.Convert.ToInt32(boutonPalette.Text);
                couleur = valeurActive == valeurPalette ? Colors.White : Colors.Transparent;
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
