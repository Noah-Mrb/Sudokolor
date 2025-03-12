using CommunityToolkit.Mvvm.ComponentModel;
using Modele.Exceptions;
using Newtonsoft.Json;


namespace Modele
{
    /// <summary>
    /// Une case d'une grille
    /// </summary>
    /// <author>Valentin Colindre</author>
    public partial class Case : ObservableObject
    {
        [ObservableProperty]
        [JsonProperty]
        private int valeur;

        
        [ObservableProperty]
        [JsonProperty]
        private bool estModifiable;


        /// <summary>
        /// Créer une case
        /// </summary>
        /// <param name="valeur">valeur de la case</param>
        /// <param name="modifiable">modifiable</param>
        /// <exception cref="HorsLimiteException">Valeur en dehors des limites 0 et 9</exception>
        public Case(int valeur,bool modifiable) 
        {
            if(valeur<0 || valeur>9)
                throw new HorsLimiteException("La valeur doit être supérieur ou égal à 0 ou inférieur ou égal à 9.");
            this.valeur = valeur;
            this.estModifiable = modifiable;
        }

    }
}
