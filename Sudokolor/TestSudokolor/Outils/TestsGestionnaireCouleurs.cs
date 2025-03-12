using Modele;
using Outils;
using Outils.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSudokolor.Outils
{
    /// <summary>
    /// Classe de test du gestionnaire de
    /// couleurs
    /// </summary>
    public class TestsGestionnaireCouleurs
    {
        /// <summary>
        /// Test la méthode GetCouleur 
        /// du gestionnaire de couleurs
        /// </summary>
        /// <author>Valentin Colindre</author>
        [Fact]
        public void TestObtenirCouleur()
        {
            for (int i = 0; i < 10; i++)
                Assert.Equal(Enum.GetName(typeof(DEFAUT), i).Replace("_", ""), GestionnaireCouleurs.ObtenirCouleur(i, THEME_COULEUR.DEFAUT));

            Assert.NotEqual(Enum.GetName(typeof(DEFAUT), 0), GestionnaireCouleurs.ObtenirCouleur(0, THEME_COULEUR.DEFAUT));
        }
    }
}
