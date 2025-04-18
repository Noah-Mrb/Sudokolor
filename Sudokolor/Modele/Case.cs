﻿using CommunityToolkit.Mvvm.ComponentModel;
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
        /// <summary>
        /// la valeur de la case
        /// </summary>
        [ObservableProperty]
        [JsonProperty]
        private int valeur;

        /// <summary>
        /// Si une case est modifiable
        /// </summary>
        [ObservableProperty]
        [JsonProperty]
        private bool estModifiable;

        /// <summary>
        /// Si une case est valide
        /// </summary>
        [ObservableProperty]
        private bool estValide;


        /// <summary>
        /// Créer une case
        /// </summary>
        /// <param name="valeur">valeur de la case</param>
        /// <param name="modifiable">modifiable</param>
        /// <exception cref="HorsLimiteException">Valeur en dehors des limites 0 et 9</exception>
        public Case(int valeur,bool modifiable) 
        {
            if(valeur < 0 || valeur > Grille.TAILLE_GRILLE)
                throw new HorsLimiteException($"La valeur doit être supérieur ou égal à 0 ou inférieur ou égal à {Grille.TAILLE_GRILLE}.");
            this.valeur = valeur;
            this.estModifiable = modifiable;
            this.estValide = true;
        }
    }
}
