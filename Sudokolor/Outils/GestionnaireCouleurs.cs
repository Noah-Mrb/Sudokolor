using Modele;
using Outils.Themes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outils
{
    /// <summary>
    /// Gestionnaire de couleurs
    /// </summary>
    /// <author>Valentin Colindre</author>
    public static class GestionnaireCouleurs
    {

        /// <summary>
        /// Renvoi le code hex de la couleur
        /// associée à la valeur et au thème donné
        /// </summary>
        /// <param name="valeur">valeur</param>
        /// <param name="theme">thème</param>
        /// <returns>le code hex de la couleur</returns>
        public static string ObtenirCouleur(int valeur, THEME_COULEUR theme)
        {
            //Utilisé pour le mode chiffre
            const string GRIS = "404040";
            const string BLEU_FONCE = "445174";

            //Couleur utilisé en cas de thème inconnu
            const string COULEUR_DEFAUT = "4F391F";

            string res;
            switch (theme)
            {
                case THEME_COULEUR.DEFAUT:
                    res = Enum.GetName(typeof(DEFAUT), valeur).Replace("_", "");
                    break;

                case THEME_COULEUR.DALTONIEN:
                    res = Enum.GetName(typeof(DALTONIEN), valeur).Replace("_", "");
                    break;

                case THEME_COULEUR.CHIFFRES:
                    //0 = vide
                    if (valeur > 0)
                    {
                        res = BLEU_FONCE;
                    }
                    else
                    {
                        res = GRIS;
                    }
                    break;

                default:
                    //Valeur par défaut
                    res = COULEUR_DEFAUT;
                    break;
            }

            return res;
        }
    }
}
