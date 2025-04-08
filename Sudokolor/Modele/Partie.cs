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

        [JsonProperty]
        private TimeSpan temps;

        [JsonProperty]
        private int nombreActions;

        [JsonProperty]
        private int nombreAides;

        [JsonProperty]
        private bool contreLaMontre;


        /// <summary>
        /// Historique des changements de la partie
        /// en cours
        /// </summary>
        [JsonProperty]
        private List<int[]> historique;

        /// <summary>
        /// Difficulté de la partie
        /// </summary>
        [JsonProperty]
        private DIFFICULTE difficulte;

        public DIFFICULTE Difficulte => difficulte;

        /// <summary>
        /// Graine de la partie
        /// </summary>
        public string Graine => graine;

        /// <summary>
        /// Renvoi le nombre d'actions faites dans la partie
        /// </summary>
        public int NombreActions => nombreActions;

        /// <summary>
        /// Renvoi le nombre d'aides utilisés dans la partie
        /// </summary>
        public int NombreAides => nombreAides;

        /// <summary>
        /// Si la partie est en mode contre la montre
        /// </summary>
        public bool ContreLaMontre => contreLaMontre;

        /// <summary>
        /// Renvoi le temps de la partie
        /// </summary>
        public TimeSpan Temps => temps;

        /// <summary>
        /// Instantie une partie à partir
        /// d'une grille et d'un historique 
        /// </summary>
        /// <param name="grille">grille</param>
        /// <param name="historique">historique des actions de la partie</param>
        /// <param name="graine">graine</param>
        /// <param name="difficulte">Difficulté de la partie</param>
        /// <param name="contreLaMontre">Si la partie est en mode contre la montre</param>
        /// <param name="tempsDebut">Temps de la partie à l'initialisation</param>
        public Partie(
            Grille grille,
            List<int[]> historique,
            string graine,
            DIFFICULTE difficulte,
            bool contreLaMontre,
            TimeSpan tempsDebut
            )
        {
            this.graine = graine;
            this.grille = grille;
            this.historique = historique;
            this.difficulte = difficulte;
            this.contreLaMontre = contreLaMontre;
            this.temps= tempsDebut;
            this.nombreActions = 0;
            this.nombreAides = 0;
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

        /// <summary>
        /// Ajoute la valeur en paramètre au nombre d'actions
        /// </summary>
        /// <param name="valeurAAjouter">valeur à ajouter</param>
        public void AjouterNombreActions(int valeurAAjouter)
        {
            this.nombreActions += valeurAAjouter;
        }

        /// <summary>
        /// Ajoute la valeur en paramètre au nombre d'aides
        /// </summary>
        /// <param name="valeurAAjouter">valeur à ajouter</param>
        public void AjouterNombreAides(int valeurAAjouter)
        {
            this.nombreAides += valeurAAjouter;
        }

        /// <summary>
        /// Ajoute du temps au temps de la partie
        /// </summary>
        /// <param name="tempsSupplementaire">temps à ajouter</param>
        public void AjouterTempsPartie(TimeSpan tempsSupplementaire)
        {
            this.temps = this.temps.Add(tempsSupplementaire);
        }

        /// <summary>
        /// Retire du temps au temps de la partie
        /// </summary>
        /// <param name="tempsEnMoins">temps à retirer</param>
        public void SoustraireTempsPartie(TimeSpan tempsEnMoins)
        {
            this.temps = this.temps.Subtract(tempsEnMoins);
        }

    }
}
