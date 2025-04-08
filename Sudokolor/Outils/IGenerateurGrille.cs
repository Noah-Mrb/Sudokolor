using Modele;

namespace Outils
{
    /// <summary>
    /// Interface pour la génération d'une grille
    /// </summary>
    /// <author>Noah Mirbel</author>
    public interface IGenerateurGrille
    {
        /// <summary>
        /// Génère une grille, si une valeur est donnée en paramètre une grille spécifique peut être généré via cette valeur
        /// </summary>
        /// <param name="graine">Valeur permetant de générer une grille spécifique si elle est renseigné</param>
        /// <returns>Une grille initialisé</returns>
        public Grille GenererGrille(string graine = "", int quantiteCaseVide=40);

        /// <summary>
        /// Vérifie si une grille est complétée
        /// </summary>
        /// <param name="grille">grille à vérifier</param>
        /// <returns>vrai si complète, faux sinon</returns>
        /// <author>Valentin Colindre</author>
        public bool VerifierGrille(Partie partie);

        /// <summary>
        /// Récupère toutes les erreurs dans une grilles
        /// </summary>
        /// <param name="partie">grille de la partie en cours</param>
        /// <returns>liste des erreurs</returns>
        /// <author>Noah Mirbel</author>
        public List<(int, int)> RecupererErreurs(Partie partie);

        /// <summary>
        /// Récupère la solution d'une case
        /// </summary>
        /// <param name="ligne">ligne de la case à vérifier</param>
        /// <param name="colonne">colonne de la case à vérifier</param>
        /// <param name="graine">graine de la grille à vérifier si besoin</param>
        /// <returns>valeur de la case</returns>
        public int ObtenirSolutionCase(int ligne, int colonne, string graine = "");
    }
}
