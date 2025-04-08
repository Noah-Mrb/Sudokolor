using Modele;
using Outils.Sauvegardes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSudokolor.Outils
{
    /// <summary>
    /// Tests de la sauvegarde d'historique de partie
    /// via json
    /// </summary>
    /// <author>Valentin Colindre</author>
    public class TestsSauvegardeHistoriqueJson
    {
        /// <summary>
        /// Test la sauvegarde et le chargement (puisque les deux sont liés)
        /// </summary>
        [Fact]
        public void Tests()
        {
            SauvegardeHistoriqueJson sauvegarde = new SauvegardeHistoriqueJson("", "testHistorique.json");

            sauvegarde.EffacerHistorique();

            //Vérifie que le chargement à "vide" renvoi bien une liste vide
            Assert.Empty(sauvegarde.ChargerHistorique());

            PartieHistorique partieHisto = new PartieHistorique(DIFFICULTE.FACILE, "test1", 3000, false);
            sauvegarde.SauvegarderPartie(partieHisto);
            
            //test de chargement de base
            List<PartieHistorique> histoCharge = sauvegarde.ChargerHistorique();
            Assert.Single(histoCharge);
            Assert.Equal(DIFFICULTE.FACILE, histoCharge[0].Difficulte);
            Assert.Equal("test1", histoCharge[0].Graine);
            Assert.Equal(3000, histoCharge[0].Score);
            Assert.False(histoCharge[0].ContreLaMontre);

            partieHisto.Score += 1;
            sauvegarde.SauvegarderPartie(partieHisto);

            //Vérifie que si même graine = remplace ancienne partie si score >
            Assert.Single(sauvegarde.ChargerHistorique());
            Assert.Equal(3001, sauvegarde.ChargerHistorique()[0].Score);

            //Vérifie que si le score est inférieur on ne remplace pas
            partieHisto.Score -= 2;
            sauvegarde.SauvegarderPartie(partieHisto);

            Assert.Equal(3001, sauvegarde.ChargerHistorique()[0].Score);

            for (int i = 1; i < 10; i++)
            {
                partieHisto = new PartieHistorique(DIFFICULTE.FACILE, $"test{i}", 3000+i, i%2==0);
                sauvegarde.SauvegarderPartie(partieHisto);
            }
            Assert.True(sauvegarde.ChargerHistorique().Count == 10);

            //Vérifie que la limite de 10 n'est pas dépassé
            partieHisto = new PartieHistorique(DIFFICULTE.FACILE, $"testo", 3000, false);
            sauvegarde.SauvegarderPartie(partieHisto);

            Assert.True(sauvegarde.ChargerHistorique().Count == 10);

        }
    }
}
