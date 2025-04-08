
namespace Outils.CalculateursScore
{
    /// <summary>
    /// Calculateur de score additionnel.
    /// Renvoi un score entre 0 et 10000
    /// </summary>
    /// <author>Valentin Colindre</author>
    public class CalculateurScoreAdditionnel : ICalculateurScore
    {
        const int VALEUR_REFERENCE = 10000;
        const double TEMPS_MODIFICATEUR = 0.01;
        const double ACTIONS_MODIFICATEUR = 0.02;
        const double AIDES_MODIFICATEUR = 0.5;

        /// <inheritdoc/>
        public int CalculerScoreContreLaMontre(TimeSpan temps, int nombreActions, int nombreAides)
        {
            //Ajout de 1 pour éviter d'avoir 0
            double scoreTemps = (1 + Math.Log(1 + temps.TotalSeconds * TEMPS_MODIFICATEUR));
            double scoreActions = 1 + Math.Log(1 + nombreActions * ACTIONS_MODIFICATEUR);
            double scoreAides = 1 + Math.Log(1 + nombreAides * AIDES_MODIFICATEUR);

            double score = Math.Round(VALEUR_REFERENCE * scoreTemps / (scoreActions + scoreAides));
            return Convert.ToInt32(score);
        }

        /// <inheritdoc/>
        public int CalculerScoreNormal(TimeSpan temps, int nombreActions, int nombreAides)
        {
            //Ajout de 1 pour éviter d'avoir 0
            double scoreTemps = (1 + Math.Log(1 + temps.TotalSeconds * TEMPS_MODIFICATEUR));
            double scoreActions = 1 + Math.Log(1 + nombreActions * ACTIONS_MODIFICATEUR);
            double scoreAides = 1+ Math.Log(1 + nombreAides * AIDES_MODIFICATEUR);

            double score = Math.Round(VALEUR_REFERENCE / (scoreTemps + scoreActions + scoreAides));
            return Convert.ToInt32(score);
        }
    }
}
