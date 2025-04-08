using Outils.CalculateursScore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSudokolor.Outils
{
    /// <summary>
    /// Classe de test du calculateur additionnel
    /// </summary>
    /// <author>Valentin Colindre</author>
    public class TestsCalculateurAdditionnel
    {
        /// <summary>
        /// Test de la méthode CalculerScoreNormal
        /// </summary>
        [Fact]
        public void TestCalculerScoreNormal()
        {
            CalculateurScoreAdditionnel calculateur = new CalculateurScoreAdditionnel();

            TimeSpan testTimeSpanSuperieur = TimeSpan.FromSeconds(1900);
            TimeSpan testTimeSpanInferieur = TimeSpan.FromSeconds(10);

            int nombreAction = 10;
            int nombreAides = 0;

            int scoreNormalSup = calculateur.CalculerScoreNormal(testTimeSpanSuperieur, nombreAction, nombreAides);
            int scoreNormalInf = calculateur.CalculerScoreNormal(testTimeSpanInferieur, nombreAction, nombreAides);
            Assert.True(scoreNormalInf >= 0);
            Assert.True(scoreNormalSup < scoreNormalInf);
            Assert.True(scoreNormalSup >= 0);
        }

        /// <summary>
        /// Test de la méthode CalculerScoreContreLaMontre
        /// </summary>
        [Fact]
        public void TestCalculerScoreContreLaMontre()
        {
            CalculateurScoreAdditionnel calculateur = new CalculateurScoreAdditionnel();

            TimeSpan testTimeSpanSuperieur = TimeSpan.FromSeconds(1900);
            TimeSpan testTimeSpanInferieur = TimeSpan.FromSeconds(10);

            int nombreAction = 10;
            int nombreAides = 0;

            int scoreSuperieur = calculateur.CalculerScoreContreLaMontre(testTimeSpanSuperieur, nombreAction, nombreAides);
            int scoreInferieur = calculateur.CalculerScoreContreLaMontre(testTimeSpanInferieur, nombreAction, nombreAides);
            Assert.True(scoreSuperieur >= 0);
            Assert.True(scoreSuperieur > scoreInferieur);
            Assert.True(scoreInferieur >= 0);
        }
    }
}
