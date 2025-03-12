using Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSudokolor.Modele
{
    /// <summary>
    /// Test de la classe modele de donnée
    /// Partie
    /// </summary>
    public class TestPartie
    {
        /// <summary>
        /// Test les méthodes liées
        /// à l'historique de Partie
        /// </summary>
        [Fact]
        public void TestHistorique()
        {
            Partie partie = new Partie(ObtenirGrille(), new(), "test");
            partie.AjouterAction(new int[]{ 0, 1 });
            Assert.Equal(1, partie.ObtenirTailleHistorique());
            Assert.Equal(new int[] { 0, 1 }, partie.ObtenirAction(0));
            partie.RetirerAction(0);
            Assert.Equal(0, partie.ObtenirTailleHistorique());
        }


        /// <summary>
        /// Renvoiune grille de test
        /// </summary>
        /// <returns>grille de test</returns>
        private Grille ObtenirGrille() { 
            int[,] solution = new int[9, 9] {
                {5, 3, 4, 6, 7, 8, 9, 1, 2},
                {6, 7, 2, 1, 9, 5, 3, 4, 8},
                {1, 9, 8, 3, 4, 2, 5, 6, 7},
                {8, 5, 9, 7, 6, 1, 4, 2, 3},
                {4, 2, 6, 8, 5, 3, 7, 9, 1},
                {7, 1, 3, 9, 2, 4, 8, 5, 6},
                {9, 6, 1, 5, 3, 7, 2, 8, 4},
                {2, 8, 7, 4, 1, 9, 6, 3, 5},
                {3, 4, 5, 2, 8, 6, 1, 7, 9}
            };
            Case[,] contenu = new Case[9, 9];

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    contenu[i, j] = new Case(solution[i, j], true);
            return new Grille(contenu);
        }
    }
}
