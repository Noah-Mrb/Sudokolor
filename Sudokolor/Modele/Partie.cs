using Newtonsoft.Json;


namespace Modele
{
    /// <summary>
    /// Classe d'une partie
    /// Contient les informations d'une 
    /// partie en cours.
    /// </summary>
    /// <author>Valentin Colindre</author>
    public class Partie
    {

        [JsonProperty]
        private string graine;


        [JsonProperty]
        private Grille grille;


        /// <summary>
        /// Historique des changements de la partie
        /// en cours
        /// </summary>
        [JsonProperty]
        private List<int[]> historique;

        /// <summary>
        /// Graine de la partie
        /// </summary>
        public string Graine => graine;

        /// <summary>
        /// Instantie une partie à partir
        /// d'une grille et d'un historique 
        /// </summary>
        /// <param name="grille">grille</param>
        /// <param name="historique">historique des actions de la partie</param>
        /// <param name="graine">graine</param>
        public Partie(Grille grille, List<int[]> historique,string graine)
        {
            this.graine = graine;
            this.grille = grille;
            this.historique = historique;
        }

        /// <summary>
        /// Ajoute une action à l'historique
        /// </summary>
        /// <param name="action">
        /// Un tableau d'entier de deux valeurs : 
        /// index de la case changée
        /// couleur changée
        /// </param>
        public void AjouterAction(int[] action)
        {
            historique.Add(action);
        }

        /// <summary>
        /// Renvoi l'action à 
        /// l'index précisé de l'historique
        /// </summary>
        /// <param name="index">index de l'action</param>
        /// <returns>action de l'index précisé</returns>
        public int[] ObtenirAction(int index)
        {
            return historique[index];
        }
        /// <summary>
        /// Renvoi la taille de l'historique
        /// </summary>
        /// <returns>Taille de l'historique</returns>
        public int ObtenirTailleHistorique()
        {
            return historique.Count;
        }

        /// <summary>
        /// Retire une action de l'historique
        /// </summary>
        /// <param name="index">index de l'action</param>
        public void RetirerAction(int index) { 
            historique.RemoveAt(index);
        }

        /// <summary>
        /// Récupère la taille de la dimension précisée de la grille
        /// de la partie
        /// </summary>
        /// <param name="dimension">dimension</param>
        /// <returns>taille de la grille dans la dimension précisée</returns>
        public int RecupererTailleGrille(int dimension)
        {
            return this.grille.RecupererTaille(dimension);
        }

        /// <summary>
        /// renvoi la case correspondant 
        /// aux coordonnées spécifiées en paramètre
        /// </summary>
        /// <param name="hauteur">hauteur</param>
        /// <param name="largeur">largeur</param>
        /// <returns>case liée</returns>
        public Case RecupererCaseGrille(int hauteur, int largeur)
        {
            return this.grille[hauteur, largeur];
        }

    }
}
