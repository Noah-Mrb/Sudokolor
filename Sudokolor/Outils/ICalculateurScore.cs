
namespace Outils
{
    /// <summary>
    /// Calculateur de score à partir de points de données
    /// </summary>
    /// <author>Valentin Colindre</author>
    public interface ICalculateurScore
    {

        /// <summary>
        /// Renvoi le score calculé à partir des paramètres
        /// </summary>
        /// <param name="temps">temps écoulé depuis le début de la partie</param>
        /// <param name="nombreActions">nombres d'actions effectuées depuis le début de la partie</param>
        /// <param name="nombreAides">nombre d'aides utilisés depuis le début de la partie</param>
        /// <returns>score en entier. Plus haut = meilleur.</returns>
        public int CalculerScoreNormal(TimeSpan temps, int nombreActions, int nombreAides);

        /// <summary>
        /// Renvoi le score calculé à partir des paramètres
        /// pour le mode contre la montre
        /// </summary>
        /// <param name="temps">temps restant depuis le début de la partie</param>
        /// <param name="nombreActions">nombre d'actions utilisés depuis le début de la partie</param>
        /// <param name="nombreAides">nombre d'aides utilisés depuis le début de la partie</param>
        /// <returns>score en entier. plus haut = meilleur</returns>
        public int CalculerScoreContreLaMontre(TimeSpan temps, int nombreActions, int nombreAides);
    }
}
