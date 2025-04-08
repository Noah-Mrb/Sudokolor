using Modele;

namespace Outils
{
    /// <summary>
    /// Interface de service de sauvegarde
    /// d'une partie
    /// </summary>
    /// <author>Valentin Colindre</author>
    public interface ISauvegardePartie
    {
        /// <summary>
        /// Sauvegarde une partie,
        /// écrasant l'ancienne.
        /// </summary>
        /// <param name="partie">partie à sauvegarder</param>
        public void SauvegarderPartie(Partie partie);

        /// <summary>
        /// Charge une partie si une est présente
        /// </summary>
        /// <returns>Partie en mémoire</returns>
        /// <exception cref="PartieIntrouvableException">Si aucune partie n'est trouvée</exception>
        public Partie ChargerPartie();

        /// <summary>
        /// Vérifie si une partie est en mémoire
        /// </summary>
        /// <returns>vrai si partie en mémoire, faux sinon</returns>
        public bool PartieEnCours();

        /// <summary>
        /// Efface la partie en cours
        /// </summary>
        /// <exception cref="PartieIntrouvableException">Si aucune partie n'est trouvée</exception>
        public void EffacerPartie();
    }
}
