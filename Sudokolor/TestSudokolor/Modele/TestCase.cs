using Modele;
using Modele.Exceptions;

namespace TestSudokolor.Modele
{
    /// <summary>
    /// Classe de test d'une case de la grille
    /// </summary>
    public class TestCase
    {
        /// <summary>
        /// Teste la validité d'une case en vérifiant que sa valeur est bien dans l'intervalle autorisé
        /// </summary>
        /// <author>Romain Card</author>
        [Fact]
        public void TestCaseInvalide()
        {
            Assert.Throws<HorsLimiteException>(() => new Case(10, true));
            Assert.Throws<HorsLimiteException>(() => new Case(-1, true));
        }
    }
}
