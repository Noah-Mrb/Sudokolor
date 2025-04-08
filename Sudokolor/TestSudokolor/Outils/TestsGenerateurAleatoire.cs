using Modele;
using Outils.Generateurs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TestSudokolor.Outils
{
    /// <summary>
    /// Classe de test du générateur aléatoire
    /// </summary>
    /// <author>Valentin Colindre</author>
    public class TestsGenerateurAleatoire
    {
        /// <summary>
        /// Test la méthode GenererGrille
        /// du générateur aléatoire.
        /// Vérifie si la grille renvoyée est conforme
        /// à ce qui est attendu du générateur
        /// </summary>
        [Fact]
        public void TestGenererGrille()
        {
            GenerateurAleatoire generateur = new GenerateurAleatoire();
            Grille grille1 = generateur.GenererGrille("test1");
            Grille grille2 = generateur.GenererGrille("test2");
            Grille grilleAleatoire = generateur.GenererGrille((new Random()).Next().ToString());

            for (int i = 0; i < 2; i++)
            {
                Assert.True(grille1.RecupererTaille(i) > 0);
                Assert.True(grille2.RecupererTaille(i) > 0);
                Assert.True(grilleAleatoire.RecupererTaille(i) > 0);
            }
            Assert.False(TestIdentique(
                grille1,
                grille2,
                grille1.RecupererTaille(0),
                grille2.RecupererTaille(1)
            ));
            Assert.False(TestIdentique(
                grille1,
                grilleAleatoire,
                grille1.RecupererTaille(0),
                grille2.RecupererTaille(1)
            ));

            Assert.False(TestSimilitude(grille1));
            Assert.False(TestSimilitude(grille2));
            Assert.False(TestSimilitude(grilleAleatoire));
            Assert.False(TestNonModifiable(grille1));
            Assert.False(TestNonModifiable(grille2));
            Assert.False(TestNonModifiable(grilleAleatoire));
        }

        /// <summary>
        /// Test la méthode VerifierGrille
        /// du générateur aléatoire
        /// </summary>
        [Fact]
        public void TestVerifierGrille()
        {
            GenerateurAleatoire generateurAleatoire = new GenerateurAleatoire();
            Partie partieValide = new(ObtenirGrilleValide(), new(), "", DIFFICULTE.FACILE, false, new());
            Partie partieInvalide = new(ObtenirGrilleInvalide(), new(), "", DIFFICULTE.FACILE, false, new());
            Assert.True(generateurAleatoire.VerifierGrille(partieValide));
            Assert.False(generateurAleatoire.VerifierGrille(partieInvalide));
        }

        /// <summary>
        /// Test la méthode RecupererErreurs
        /// du générateur aléatoire
        /// </summary>
        [Fact]
        public void TestVerifierRecuperationErreursGrille()
        {
            GenerateurAleatoire generateurAleatoire = new GenerateurAleatoire();
            Grille grilleAvecErreurs = ObtenirGrilleAvecErreurs();
            Grille grilleValide = ObtenirGrilleValide();
            Partie partieValide = new Partie(grilleValide, new(), "", DIFFICULTE.FACILE, false, new());
            Partie partieAvecErreurs = new Partie(grilleAvecErreurs, new(), "", DIFFICULTE.FACILE, false, new());


            Assert.Empty(generateurAleatoire.RecupererErreurs(partieValide));
            Assert.Equal(3,generateurAleatoire.RecupererErreurs(partieAvecErreurs).Count);
        }

        /// <summary>
        /// Teste que la méthode ObtenirCaseSolution renvoie bien les bonnes valeurs qui permettent de résoudre la grille
        /// </summary>
        /// <author>Romain Card</author>
        [Fact]
        public void TestObtenirCaseSolution()
        {
            GenerateurAleatoire generateurAleatoire = new GenerateurAleatoire();
            Grille grille = generateurAleatoire.GenererGrille("test");
            for (int i = 0; i < grille.RecupererTaille(0); i++)
            {
                for (int j = 0; j < grille.RecupererTaille(1); j++)
                {
                    if (grille[i, j].EstModifiable)
                    {
                        Assert.Equal(grille[i, j].Valeur, 0);
                    }
                    else
                    {
                        Assert.Equal(grille[i, j].Valeur, generateurAleatoire.ObtenirSolutionCase(i, j, "test"));
                    }
                }
            }
        }

        /// <summary>
        /// Vérifie si toutes les valeurs de la grille 
        /// spécifiée sont égales
        /// </summary>
        /// <param name="grille">grille spécifiée</param>
        /// <returns>vrai si similaires, faux sinon</returns>
        private bool TestSimilitude(Grille grille)
        {
            bool similaire = true;
            int hauteur = 0;
            int largeur = 0;
            while (similaire && hauteur < grille.RecupererTaille(0) - 1 && largeur < grille.RecupererTaille(1) - 1)
            {
                similaire = similaire && grille[hauteur, largeur].Valeur == grille[hauteur + 1, largeur + 1].Valeur;
                hauteur++;
                largeur++;
            }
            return similaire;
        }

        /// <summary>
        /// Vérifie si deux grilles de même taille sont égales
        /// </summary>
        /// <param name="grille1">grille1</param>
        /// <param name="grille2">grille2</param>
        /// <param name="tailleHauteur">hauteur des grilles</param>
        /// <param name="tailleLargeur">largeur des grilles</param>
        /// <returns>vrai si égale, faux sinon</returns>
        private bool TestIdentique(Grille grille1, Grille grille2,int tailleHauteur, int tailleLargeur)
        {
            bool resultat = true;
            int hauteur = 0;
            int largeur = 0;
            while (resultat && hauteur < tailleHauteur && largeur < tailleLargeur)
            {
                resultat = resultat && grille1[hauteur, largeur].Valeur == grille2[hauteur, largeur].Valeur;
            }
            return resultat;
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
            while (nonModifiable && hauteur < grille.RecupererTaille(0) && largeur < grille.RecupererTaille(1))
            {
                nonModifiable = nonModifiable && !grille[hauteur, largeur].EstModifiable;
                hauteur++;
                largeur++;
            }
            return nonModifiable;
        }

        /// <summary>
        /// Renvoi une grille valide de test 
        /// générique
        /// </summary>
        /// <returns>grille valide (complétée) de test</returns>
        private Grille ObtenirGrilleValide()
        {
            return new Grille(new Case[,]{
            { new Case(5, false), new Case(3, false), new Case(4, false), new Case(6, false), new Case(7, false), new Case(8, false), new Case(9, false), new Case(1, false), new Case(2, false) },
            { new Case(6, false), new Case(7, false), new Case(2, false), new Case(1, false), new Case(9, false), new Case(5, false), new Case(3, false), new Case(4, false), new Case(8, false) },
            { new Case(1, false), new Case(9, false), new Case(8, false), new Case(3, false), new Case(4, false), new Case(2, false), new Case(5, false), new Case(6, false), new Case(7, false) },
            { new Case(8, false), new Case(5, false), new Case(9, false), new Case(7, false), new Case(6, false), new Case(1, false), new Case(4, false), new Case(2, false), new Case(3, false) },
            { new Case(4, false), new Case(2, false), new Case(6, false), new Case(8, false), new Case(5, false), new Case(3, false), new Case(7, false), new Case(9, false), new Case(1, false) },
            { new Case(7, false), new Case(1, false), new Case(3, false), new Case(9, false), new Case(2, false), new Case(4, false), new Case(8, false), new Case(5, false), new Case(6, false) },
            { new Case(9, false), new Case(6, false), new Case(1, false), new Case(5, false), new Case(3, false), new Case(7, false), new Case(2, false), new Case(8, false), new Case(4, false) },
            { new Case(2, false), new Case(8, false), new Case(7, false), new Case(4, false), new Case(1, false), new Case(9, false), new Case(6, false), new Case(3, false), new Case(5, false) },
            { new Case(3, false), new Case(4, false), new Case(5, false), new Case(2, false), new Case(8, false), new Case(6, false), new Case(1, false), new Case(7, false), new Case(9, false) }
            });
        }

        /// <summary>
        /// renvoi une grille invalide de test
        /// générique
        /// </summary>
        /// <returns>grille invalide (avec erreur) de test</returns>
        private Grille ObtenirGrilleInvalide()
        {
            return new Grille(new Case[,]{
            { new Case(1, true), new Case(2, false), new Case(3, true), new Case(4, false), new Case(5, true), new Case(6, false), new Case(7, true), new Case(8, false), new Case(9, true) },
            { new Case(1, false), new Case(2, true), new Case(3, false), new Case(4, true), new Case(5, false), new Case(6, true), new Case(7, false), new Case(8, true), new Case(9, false) },
            { new Case(1, true), new Case(2, false), new Case(3, true), new Case(4, false), new Case(5, true), new Case(6, false), new Case(7, true), new Case(8, false), new Case(9, true) },
            { new Case(1, false), new Case(2, true), new Case(3, false), new Case(4, true), new Case(5, false), new Case(6, true), new Case(7, false), new Case(8, true), new Case(9, false) },
            { new Case(1, true), new Case(2, false), new Case(3, true), new Case(4, false), new Case(5, true), new Case(6, false), new Case(7, true), new Case(8, false), new Case(9, true) },
            { new Case(1, false), new Case(2, true), new Case(3, false), new Case(4, true), new Case(5, false), new Case(6, true), new Case(7, false), new Case(8, true), new Case(9, false) },
            { new Case(1, true), new Case(2,false ),new Case (3,true ),new  Case (4,false ),new  Case (5,true ),new  Case (6,false ),new  Case (7,true ),new  Case (8,false ),new  Case (9,true )},
            {new  Case (1,false ),new  Case (2,true ),new  Case (3,false ),new  Case (4,true ),new  Case (5,false ),new  Case (6,true ),new  Case (7,false ),new  Case (8,true ),new  Case (9,false )},
            {new  Case (1,true ),new  Case (2,false ),new  Case (3,true ),new  Case (4,false ),new  Case (5,true ),new  Case (6,false ),new  Case (7,true ),new  Case (8,false ),new  Case (9,true )}
            });
        }

        /// <summary>
        /// revoie une grille avec quelques erreurs (en l'occurence 3)
        /// </summary>
        /// <returns>grille de test avec 3 erreurs</returns>
        private Grille ObtenirGrilleAvecErreurs()
        {
            return new Grille(new Case[,]{
            { new Case(4, true), new Case(2, true), new Case(3, true), new Case(5, true), new Case(6, true), new Case(7, true), new Case(8, true), new Case(0, true), new Case(1, true) },
            { new Case(5, true), new Case(6, true), new Case(1, true), new Case(0, true), new Case(8, true), new Case(4, true), new Case(2, true), new Case(3, true), new Case(7, true) },
            { new Case(0, true), new Case(8, true), new Case(7, true), new Case(2, true), new Case(3, true), new Case(1, true), new Case(4, true), new Case(5, true), new Case(6, true) },
            { new Case(7, true), new Case(4, true), new Case(8, true), new Case(6, true), new Case(5, true), new Case(0, true), new Case(3, true), new Case(1, true), new Case(2, true) },
            { new Case(3, true), new Case(1, true), new Case(5, true), new Case(7, true), new Case(4, true), new Case(2, true), new Case(6, true), new Case(8, true), new Case(0, true) },
            { new Case(6, true), new Case(0, true), new Case(2, true), new Case(8, true), new Case(1, true), new Case(3, true), new Case(7, true), new Case(4, true), new Case(5, true) },
            { new Case(8, true), new Case(5, true), new Case(0, true), new Case(4, true), new Case(2, true), new Case(6, true), new Case(1, true), new Case(7, true), new Case(3, true) },
            { new Case(2, true), new Case(7, true), new Case(6, true), new Case(3, true), new Case(0, true), new Case(8, true), new Case(5, true), new Case(2, true), new Case(4, true) },
            { new Case(2, true), new Case(3, true), new Case(4, true), new Case(1, true), new Case(7, true), new Case(5, true), new Case(0, true), new Case(6, true), new Case(8, true) }
            });
        }
    }
}
