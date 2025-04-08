using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Modele
{
    /// <summary>
    /// Classe d'une partie 
    /// pour son enregistrement dans l'historique
    /// </summary>
    /// <author>Noah Mirbel</author>
    public partial class PartieHistorique: ObservableObject
    {
        /// <summary>
        /// code de la grille afin de la regénerer
        /// </summary>
        [ObservableProperty]
        [JsonProperty]
        private string graine;

        /// <summary>
        /// Difficulté de la partie
        /// </summary>
        [ObservableProperty]
        [JsonProperty]
        private DIFFICULTE difficulte;

        /// <summary>
        /// Date de sauvegarde de la partie
        /// </summary>
        [ObservableProperty]
        [JsonProperty]
        private DateTime date;

        /// <summary>
        /// score de la partie qui determine si elle est enregistré
        /// </summary>
        [ObservableProperty]
        [JsonProperty]
        private int score;

        /// <summary>
        /// définie si la partie était une partie normal ou contre la montre
        /// </summary>
        [JsonProperty]
        [ObservableProperty]
        private bool contreLaMontre;

        /// <summary>
        /// Créer le modele partie historique
        /// </summary>
        /// <param name="difficulte">difficulte de la partie</param>
        /// <param name="graine">graine de la partie</param>
        /// <param name="score">score de la partie</param>
        /// <param name="contreLaMontre">si la partie était contre la montre</param>
        public PartieHistorique(DIFFICULTE difficulte, string graine, int score, bool contreLaMontre)
        {
            this.difficulte = difficulte;
            this.graine = graine;
            this.date = DateTime.Now;
            this.score = score;
            this.contreLaMontre = contreLaMontre;
        }
    }
}
