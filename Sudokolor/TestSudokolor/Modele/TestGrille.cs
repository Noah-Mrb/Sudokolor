using Modele;
using Modele.Exceptions;

namespace TestSudokolor.Modele
{
    /// <summary>
    /// Classe de test d'une grille de jeu
    /// </summary>
    public class TestGrille
    {
        /// <summary>
        /// Teste le bon fonctionnement de la méthode de modification de case
        /// </summary>
        /// <author>Romain Card</author>
        [Fact]
        public void TestModifierCase()
        {
            Case[,] contenu = new Case[9, 9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    contenu[i, j] = new Case(0, false);
                }
            }

            Grille grille = new Grille(contenu);

            Assert.Throws<CaseNonModifiableException>(() => grille.ModifierCase(2, 8, 5));
            
            Assert.Throws<HorsLimiteException>(() => grille.ModifierCase(10, 1, 4));
            Assert.Throws<HorsLimiteException>(() => grille.ModifierCase(4, 12, 2));
            Assert.Throws<HorsLimiteException>(() => grille.ModifierCase(6, 8, 10));
        }
    }
}
