using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public Grille GenererGrille(string graine = "");

        /// <summary>
        /// Vérifie si une grille est complétée
        /// </summary>
        /// <param name="grille">grille à vérifier</param>
        /// <returns>vrai si complète, faux sinon</returns>
        /// <author>Valentin Colindre</author>
        public bool VerifierGrille(Partie partie);

        /// <summary>
        /// Renvoi la solution de la grille en paramètre
        /// </summary>
        /// <param name="grille">grille à résoudre</param>
        /// <returns>la grille résolue</returns>
        /// <author>Valentin Colindre</author>
        public Grille ObtenirSolution(Partie partie);
    }
}
