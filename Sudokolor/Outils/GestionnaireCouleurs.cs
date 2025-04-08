using Modele;
using Outils.Themes;

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

        /// <summary>
        /// Renvoi la couleur de bordure d'erreur
        /// selon le theme
        /// </summary>
        /// <param name="theme">theme</param>
        /// <returns>couleur de bordure correspondante</returns>
        public static string ObtenirCouleurBordureErreur(THEME_COULEUR theme)
        {
            const string COULEUR_DEFAUT = "d50610";
            const string COULEUR_DALTONIEN = "FF61FA";
            string couleurHexa = COULEUR_DEFAUT;
            switch (theme)
            {
                case THEME_COULEUR.DEFAUT:
                    couleurHexa = COULEUR_DEFAUT;
                    break;
                case THEME_COULEUR.DALTONIEN:
                    couleurHexa = COULEUR_DALTONIEN;
                    break;
                default:
                    couleurHexa = COULEUR_DEFAUT;
                    break;
            }

            return couleurHexa;
        }
    }
}
