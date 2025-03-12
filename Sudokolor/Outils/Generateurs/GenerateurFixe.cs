using Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outils.Generateurs
{
    /// <summary>
    /// Classe de génération aléatoire de la grille qui implimente l'interface IGenerateurGrille
    /// </summary>
    /// <author>Noah Mirbel</author>
    public class GenerateurFixe : IGenerateurGrille
    {
        private const int TAILLE_GRILLE = 9;
        private readonly int[,] solution;

        private readonly int[,] nonResolue;

        /// <summary>
        /// Créer le générateur fixe en initialisant 
        /// les deux attributs solution & nonResolue
        /// </summary>
        public GenerateurFixe()
        {
            solution = new int[TAILLE_GRILLE, TAILLE_GRILLE] {
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
            nonResolue = new int[TAILLE_GRILLE, TAILLE_GRILLE] {
                {5, 3, 0, 0, 7, 0, 0, 0, 0},
                {6, 0, 0, 1, 9, 5, 0, 0, 0},
                {0, 9, 8, 0, 0, 0, 0, 6, 0},
                {8, 0, 0, 0, 6, 0, 0, 0, 3},
                {4, 0, 0, 8, 0, 3, 0, 0, 1},
                {7, 0, 0, 0, 2, 0, 0, 0, 6},
                {0, 6, 0, 0, 0, 0, 2, 8, 0},
                {0, 0, 0, 4, 1, 9, 0, 0, 5},
                {0, 0, 0, 0, 8, 0, 0, 7, 9}
            };
        }

        /// <inheritdoc />
        public Grille GenererGrille(string graine = "")
        {
            Case[,] contenu = new Case[TAILLE_GRILLE, TAILLE_GRILLE];

            for (int i = 0; i < TAILLE_GRILLE; i++)
                for (int j = 0; j < TAILLE_GRILLE; j++)
                    contenu[i, j] = new Case(nonResolue[i, j], true);

            Grille grille = new Grille(contenu);

            return grille;
        }

        /// <inheritdoc />
        public Grille ObtenirSolution(Partie partie)
        {
            Case[,] contenu = new Case[TAILLE_GRILLE, TAILLE_GRILLE];

            for (int i = 0; i < TAILLE_GRILLE; i++)
                for (int j = 0; j < TAILLE_GRILLE; j++)
                    contenu[i, j] = new Case(solution[i, j], true);

            Grille grilleSolution = new Grille(contenu);

            return grilleSolution;
        }

        /// <inheritdoc />
        public bool VerifierGrille(Partie partie)
        {
            bool res = true;
            for (int i = 0; i < TAILLE_GRILLE; i++)
                for (int j = 0; j < TAILLE_GRILLE; j++)
                    res = partie.RecupererCaseGrille(i, j).Valeur == solution[i, j] && res;
            return res;
        }
    }
}
