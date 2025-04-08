using Modele;

namespace Outils
{
    /// <summary>
    /// Configurateur de partie, fournis les paramètres qui dépendent de la difficulté
    /// </summary>
    /// <author>Valentin Colindre</author>
    public static class ConfigurateurPartie
    {
        //dictionnaire des cases vides par difficulté
        //Dictionnaire = constante impossible
        private static Dictionary<DIFFICULTE, int> casesVides = new()
        {
            { DIFFICULTE.FACILE, 40 },
            { DIFFICULTE.NORMAL, 50 },
            { DIFFICULTE.DIFFICILE, 60 },
        };

        //dictionnaire des temps pour le mode contre la montre en secondes
        //Dictionnaire = constante impossible
        private static Dictionary<DIFFICULTE, int> tempsContreLaMontre = new()
        {
            { DIFFICULTE.FACILE, 2100 },
            { DIFFICULTE.NORMAL, 1800 },
            { DIFFICULTE.DIFFICILE, 900 },
        };


        /// <summary>
        /// Configure une partie à partir des paramètres
        /// et fournis les bonnes valeurs selon la difficulté
        /// </summary>
        /// <param name="difficulte">difficulté</param>
        /// <param name="graine">graine</param>
        /// <param name="generateur">générateur de grille</param>
        /// <param name="contreLaMontre">si la partie est contre la montre</param>
        /// <returns>la partie configurée</returns>
        public static Partie ConfigurerPartie(DIFFICULTE difficulte, string graine, IGenerateurGrille generateur, bool contreLaMontre)
        {
            return new Partie(
                generateur.GenererGrille(graine, casesVides[difficulte]),
                new(),
                graine,
                difficulte,
                contreLaMontre,
                contreLaMontre ? new TimeSpan(0, 0, tempsContreLaMontre[difficulte]) : new TimeSpan()
            );
        }
    }
}
