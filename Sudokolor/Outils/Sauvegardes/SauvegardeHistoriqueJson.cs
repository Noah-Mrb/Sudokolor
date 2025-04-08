using Modele;
using Newtonsoft.Json;

namespace Outils.Sauvegardes
{
    public class SauvegardeHistoriqueJson: ISauvegardeHistorique
    {

        //Chemin vers le fichier de sauvegarde
        private readonly string cheminFichier;

        /// <summary>
        /// Créer la classe de gestion
        /// de sauvegarde de partie json
        /// </summary>
        /// <param name="chemin">Chemin vers le lieu de stockage</param>
        /// <param name="fichier">nom du fichier de stockage</param>
        public SauvegardeHistoriqueJson(string chemin, string fichier)
        {
            this.cheminFichier = Path.Combine(chemin, fichier);
        }


        /// <inheritdoc />
        public void SauvegarderPartie(PartieHistorique partieHistorique)
        {
            List<PartieHistorique> partiesHistorique = this.ChargerHistorique();
            partiesHistorique = this.ajouterPartie(partiesHistorique, partieHistorique);
            // Serialize the Partie object to JSON
            string jsonData = JsonConvert.SerializeObject(partiesHistorique);

            // Write the JSON data to the file asynchronously
            File.WriteAllText(cheminFichier, jsonData);
        }


        /// <inheritdoc />
        public List<PartieHistorique> ChargerHistorique()
        {
            List<PartieHistorique>? resultat = null;

            if (File.Exists(cheminFichier))
            {
                string json = File.ReadAllText(cheminFichier);
                resultat = JsonConvert.DeserializeObject<List<PartieHistorique>>(json);
            }

            if (resultat == null)
            {
                resultat = new List<PartieHistorique>();
            }

            return resultat;
        }

        /// <summary>
        /// Si une partie avec la graine de la partie actuelle existe avec un moins bon score
        /// sinon si il y a au moins 10 parties, on supprime celle avec le moins bon score pour ajouter la nouvelle
        /// sinon on ajoute la nouvelle
        /// </summary>
        /// <param name="partiesHistorique">liste des parties dans l'historique</param>
        /// <param name="partieHistorique">partie que l'on veut enregistrer dans l'historique</param>
        /// <returns>renvoie la nouvelle liste à enregistrer</returns>
        private List<PartieHistorique> ajouterPartie(List<PartieHistorique> partiesHistorique, PartieHistorique partieHistorique)
        {
            PartieHistorique partieExistante = partiesHistorique.FirstOrDefault(p => p.Graine == partieHistorique.Graine);

            if (partieExistante != null && partieExistante.Score < partieHistorique.Score)
            {
                partiesHistorique.Remove(partieExistante);
                partiesHistorique.Add(partieHistorique);
            }
            else if(partiesHistorique.Count() >= 10)
            {
                PartieHistorique partieMinScore = partiesHistorique.OrderBy(p => p.Score).First();
                if (partieHistorique.Score > partieMinScore.Score)
                {
                    partiesHistorique.Remove(partieMinScore);
                    partiesHistorique.Add(partieHistorique);
                }
            }
            else
            {
                partiesHistorique.Add(partieHistorique);
            }

            return partiesHistorique;
        }

        /// <inheritdoc />
        public void EffacerHistorique()
        {
            if (File.Exists(cheminFichier))
            {
                File.Delete(cheminFichier);
            }
        }
    }
}
