using Modele;

namespace Outils
{
    /// <summary>
    /// Interface de service de sauvegarde
    /// de l'historique
    /// </summary>
    /// <author>Noah Mirbel</author>
    public interface ISauvegardeHistorique
    {
        /// <summary>
        /// Sauvegarde une partie dans l'historique
        /// si l'historique est plein, renvoi une exception
        /// </summary>
        /// <param name="partie">partie à sauvergarder</param>
        /// <exception cref="HistoriquePleinException">Si l'historique est plein</exception>
        public void SauvegarderPartie(PartieHistorique partieHistorique);

        /// <summary>
        /// Charge l'historique des parties
        /// </summary>
        /// <returns>renvoie la liste de l'historique des partie</returns>
        public List<PartieHistorique> ChargerHistorique();

        /// <summary>
        /// Supprime l'historique entièrement
        /// </summary>
        public void EffacerHistorique();
    }
}
