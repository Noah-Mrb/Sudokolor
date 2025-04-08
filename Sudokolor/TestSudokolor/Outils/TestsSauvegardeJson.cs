using Modele;
using Outils.Generateurs;
using Outils.Sauvegardes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestSudokolor.Outils
{
    /// <summary>
    /// Tests de la sauvegarde json
    /// </summary>
    /// <author>Valentin Colindre</author>
    public class TestsSauvegardeJson
    {
        
        /// <summary>
        /// Test les méthodes
        /// de la sauvegarde json
        /// </summary>
        [Fact]
        public async Task TestSauvegarde()
        {
            string cheminFichier = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), 
                "test.json"
                );

            if (File.Exists(cheminFichier))
                File.Delete(cheminFichier);
            SauvegardePartieJson sauvegardeJson = new(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), 
                "test.json"
                );

            Assert.False(sauvegardeJson.PartieEnCours());
            //Création d'un objet partie générique de test
            Partie partie = new Partie(new(new Case[,]{
            { new Case(1, true), new Case(2, false), new Case(3, true), new Case(4, false), new Case(5, true), new Case(6, false), new Case(7, true), new Case(8, false), new Case(9, true) },
            { new Case(1, false), new Case(2, true), new Case(3, false), new Case(4, true), new Case(5, false), new Case(6, true), new Case(7, false), new Case(8, true), new Case(9, false) },
            { new Case(1, true), new Case(2, false), new Case(3, true), new Case(4, false), new Case(5, true), new Case(6, false), new Case(7, true), new Case(8, false), new Case(9, true) },
            { new Case(1, false), new Case(2, true), new Case(3, false), new Case(4, true), new Case(5, false), new Case(6, true), new Case(7, false), new Case(8, true), new Case(9, false) },
            { new Case(1, true), new Case(2, false), new Case(3, true), new Case(4, false), new Case(5, true), new Case(6, false), new Case(7, true), new Case(8, false), new Case(9, true) },
            { new Case(1, false), new Case(2, true), new Case(3, false), new Case(4, true), new Case(5, false), new Case(6, true), new Case(7, false), new Case(8, true), new Case(9, false) },
            { new Case(1, true), new Case(2,false ),new Case (3,true ),new  Case (4,false ),new  Case (5,true ),new  Case (6,false ),new  Case (7,true ),new  Case (8,false ),new  Case (9,true )},
            {new  Case (1,false ),new  Case (2,true ),new  Case (3,false ),new  Case (4,true ),new  Case (5,false ),new  Case (6,true ),new  Case (7,false ),new  Case (8,true ),new  Case (9,false )},
            {new  Case (1,true ),new  Case (2,false ),new  Case (3,true ),new  Case (4,false ),new  Case (5,true ),new  Case (6,false ),new  Case (7,true ),new  Case (8,false ),new  Case (9,true )}
            }),
            new() {
                new int[] { 2,3}
            },
            "test",
            DIFFICULTE.FACILE,
            false,
            new());

            sauvegardeJson.SauvegarderPartie(partie);

            
            Assert.True(File.Exists(cheminFichier));
            Assert.True(sauvegardeJson.PartieEnCours());

            Partie resultat = sauvegardeJson.ChargerPartie();

            Assert.Equal(1,resultat.ObtenirTailleHistorique());
            Assert.Equal(1, resultat.RecupererCaseGrille(0, 0).Valeur);
            Assert.True(resultat.RecupererCaseGrille(0, 0).EstModifiable);
            Assert.Equal(9, resultat.RecupererCaseGrille(8, 8).Valeur);
            Assert.True(resultat.RecupererCaseGrille(8, 8).EstModifiable);
            Assert.Equal("test", resultat.Graine);
            Assert.Equal(DIFFICULTE.FACILE, resultat.Difficulte);

            sauvegardeJson.EffacerPartie();

            Assert.False(sauvegardeJson.PartieEnCours());
        }
    }
}
