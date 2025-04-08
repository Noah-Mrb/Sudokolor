using Modele;
using Newtonsoft.Json;
using Outils.Exceptions;


namespace Outils.Sauvegardes
{
    /// <summary>
    /// Gestion de sauvegarde de partie
    /// via Json
    /// </summary>
    /// <author>Valentin Colindre</author>
    public class SauvegardePartieJson : ISauvegardePartie
    {

        //Chemin vers le fichier de sauvegarde
        private readonly string cheminFichier;

        /// <summary>
        /// Créer la classe de gestion
        /// de sauvegarde de partie json
        /// </summary>
        /// <param name="chemin">Chemin vers le lieu de stockage</param>
        /// <param name="fichier">nom du fichier de stockage</param>
        public SauvegardePartieJson(string chemin, string fichier)
        {
            this.cheminFichier = Path.Combine(chemin, fichier);
        }

        /// <inheritdoc />
        public Partie ChargerPartie()
        {
            Partie? resultat = null;

            if (File.Exists(cheminFichier))
            {
                string json = File.ReadAllText(cheminFichier);
                resultat = JsonConvert.DeserializeObject<Partie>(json);
            }

            if (resultat == null)
            {
                throw new PartieIntrouvableException();
            }

            return resultat;
        }

        /// <inheritdoc />
        public void EffacerPartie()
        {
            if (File.Exists(cheminFichier))
            {
                File.Delete(cheminFichier);
            }
            else
            {
                throw new PartieIntrouvableException();
            }
        }

        /// <inheritdoc />
        public bool PartieEnCours()
        {
            return File.Exists(cheminFichier);
        }

        /// <inheritdoc />
        public void SauvegarderPartie(Partie partie)
        {
            // Serialize the Partie object to JSON
            string jsonData = JsonConvert.SerializeObject(partie);

            // Write the JSON data to the file asynchronously
            File.WriteAllText(cheminFichier, jsonData);
        }
    }
}
