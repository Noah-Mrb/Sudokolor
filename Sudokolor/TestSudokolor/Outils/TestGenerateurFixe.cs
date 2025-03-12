using Modele;
using Outils.Generateurs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSudokolor.Outils
{
    /// <summary>
    /// Classe de test du generateur fixe
    /// </summary>
    /// <author>Valentin Colindre</author>
    public class TestGenerateurFixe
    {
        /// <summary>
        /// Test la méthode GenererGrille
        /// du générateur fixe.
        /// Vérifie si la grille renvoyée est conforme
        /// à ce qui est attendu du générateur
        /// </summary>
        [Fact]
        public void TestGenererGrille()
        {
            GenerateurFixe generateur = new GenerateurFixe();
            Grille grille1 = generateur.GenererGrille("test1");
            Grille grille2 = generateur.GenererGrille("test2");
            Grille grilleAleatoire = generateur.GenererGrille((new Random()).Next().ToString());

            for (int i = 0; i < 2; i++)
            {
                Assert.True(grille1.RecupererTaille(i) > 0);
                Assert.True(grille2.RecupererTaille(i) > 0);
                Assert.True(grilleAleatoire.RecupererTaille(i) > 0);
            }
            Assert.Equal(grille1[0, 0].Valeur, grille2[0, 0].Valeur);
            Assert.Equal(grille1[0, 0].Valeur, grilleAleatoire[0, 0].Valeur);
            Assert.Equal(
                grille1[grille1.RecupererTaille(0) - 1, grille1.RecupererTaille(0) - 1].Valeur,
                grille2[grille2.RecupererTaille(0) - 1, grille2.RecupererTaille(0) - 1].Valeur
            );

            Assert.False(TestNonModifiable(grille1));
            Assert.False(TestNonModifiable(grille2));
            Assert.False(TestNonModifiable(grilleAleatoire));
        }

        /// <summary>
        /// Test la méthode VerifierGrille
        /// du générateur fixe
        /// </summary>
        [Fact]
        public void TestVerifierGrille()
        {
            GenerateurFixe generateurAleatoire = new GenerateurFixe();
            Grille grilleNonFini = generateurAleatoire.GenererGrille();
            Grille grilleSolution = ObtenirSolution();

            Assert.True(generateurAleatoire.VerifierGrille(new (grilleSolution,new(),"")));
            Assert.False(generateurAleatoire.VerifierGrille(new (grilleNonFini,new(),"")));
        }

        /// <summary>
        /// Vérifie si toutes 
        /// les cases de la grille spécifiée
        /// sont non modifiables
        /// </summary>
        /// <param name="grille">grille spécifiée</param>
        /// <returns>vrai si non modifiable, faux sinon</returns>
        private bool TestNonModifiable(Grille grille)
        {
            bool nonModifiable = true;
            int hauteur = 0;
            int largeur = 0;
            while (nonModifiable && hauteur < grille.RecupererTaille(0) - 1 && largeur < grille.RecupererTaille(0) - 1)
            {
                nonModifiable = nonModifiable && !grille[hauteur, largeur].EstModifiable;
                hauteur++;
                largeur++;
            }
            return nonModifiable;
        }

        /// <summary>
        /// Renvoi la grille solution du générateur fixe.
        /// A mettre à jour si changement de la solution
        /// du générateur fixe
        /// </summary>
        /// <returns>grille solution</returns>
        private Grille ObtenirSolution()
        {
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
