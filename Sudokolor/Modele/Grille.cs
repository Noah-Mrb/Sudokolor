using Modele.Exceptions;
using Newtonsoft.Json;

namespace Modele
{
    /// <summary>
    /// Une grille du jeu,
    /// elle contient les données de la grille
    /// </summary>
    /// <author>Valentin Colindre</author>
    public class Grille
    {
        private const int TAILLEGRILLE = 9;

        [JsonProperty]
        private Case[,] contenu;

        /// <summary>
        /// Créer une grille
        /// </summary>
        /// <param name="contenu">contenu de la grille</param>
        public Grille(Case[,] contenu) 
        {
            this.contenu = contenu;
        }

        /// <summary>
        /// Accès et modificateur des cases de la grille
        /// sans dévoiler l'implémentation
        /// </summary>
        /// <param name="hauteur">position en hauteur de la case</param>
        /// <param name="largeur">position en largeur de la case</param>
        /// <returns>Case ciblée</returns>
        public Case this[int hauteur, int largeur]
        {
            get
            {
                return this.contenu[hauteur, largeur];
            }
            set
            {
                ModifierCase(hauteur, largeur,value.Valeur);
            }
        }



        /// <summary>
        /// Récupère la taille d'une des dimensions
        /// du contenu
        /// </summary>
        /// <param name="dimension">dimension</param>
        /// <returns>taille en int</returns>
        public int RecupererTaille(int dimension)
        {
            return contenu.GetLength(dimension);
        }

        /// <summary>
        /// Modifie la valeur d'une case
        /// </summary>
        /// <param name="hauteur">position en hauteur de la case</param>
        /// <param name="largeur">position en largeur de la case</param>
        /// <param name="valeur">nouvelle valeur</param>
        /// <exception cref="CaseNonModifiableException">La modification de la case est désactivée</exception>
        public void ModifierCase(int hauteur, int largeur,int valeur)
        {
            if (hauteur < 0 ||hauteur >= contenu.GetLength(0) || largeur < 0 || largeur >= contenu.GetLength(1) || valeur<0 || valeur>TAILLEGRILLE)
                throw new HorsLimiteException("la hauteur ou la largeur est incorrect.");
            if (!contenu[hauteur, largeur].EstModifiable)
                throw new CaseNonModifiableException("La case n'est pas modifiable.");
            contenu[hauteur, largeur].Valeur=valeur;
        }

    }
}
