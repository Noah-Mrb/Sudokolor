using Modele;
using Modele.Exceptions;
using Outils;
using Outils.CalculateursScore;
using Outils.Generateurs;
using Outils.Sauvegardes;
using VueModele;

namespace TestSudokolor.VueModele
{
    /// <summary>
    /// Classe de test du VueModele de 
    /// Partie
    /// </summary>
    /// <author>Valentin Colindre</author>
    public class TestsPartieVueModele
    {
        /// <summary>
        /// Test la création d'un 
        /// PartieVueModele
        /// </summary>
        [Fact]
        public void TestPartieVueModeleCreation()
        {
            GenerateurFixe gf = new GenerateurFixe();
            SauvegardePartieJson spj = new SauvegardePartieJson("", "test.txt");
            SauvegardeHistoriqueJson shj = new SauvegardeHistoriqueJson("", "histo.txt");
            CalculateurScoreAdditionnel csa = new CalculateurScoreAdditionnel();
            Grille grille = gf.GenererGrille();

            Case[,] contenuDouble = new Case[9, 9];

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    contenuDouble[i, j] = grille[i, j];

            PartieVueModele pvm = new PartieVueModele(gf, spj, csa, shj);
            pvm.InitialiserPartie("test", DIFFICULTE.FACILE,false);

            Case[] contenu = contenuDouble.Cast<Case>().ToArray();
            for (int i = 0; i < contenu.Length; i++)
                Assert.True(contenu[i].Valeur == pvm.Contenu[i].Valeur && contenu[i].EstModifiable == pvm.Contenu[i].EstModifiable);
        }

        /// <summary>
        /// Test la méthode ObtenirCouleur
        /// de PartieVueModele
        /// </summary>
        [Fact]
        public void TestObtenirCouleur()
        {
            CalculateurScoreAdditionnel csa = new CalculateurScoreAdditionnel();
            SauvegardePartieJson spj = new SauvegardePartieJson("", "");
            SauvegardeHistoriqueJson shj = new SauvegardeHistoriqueJson("", "histo.txt");
            PartieVueModele pvm = new PartieVueModele(new GenerateurFixe(),spj, csa, shj);
            Assert.Matches(@"^#[0-9A-Fa-f]{6}$", "#" +pvm.ObtenirCouleur(0));
        }

        /// <summary>
        /// Test la méthode ToutesLesCasesRemplies à travers la propriété ToutesLesCasesSontRemplies
        /// </summary>
        [Fact]
        public void TestCasesGrillePleines()
        {
            CalculateurScoreAdditionnel csa = new CalculateurScoreAdditionnel();
            PartieVueModele pvm = new PartieVueModele(new GenerateurFixe(),new SauvegardePartieJson("",""),csa, new SauvegardeHistoriqueJson("", ""));
            Assert.False(pvm.ToutesLesCasesSontRemplies);
        }
        
        /// <summary>
        /// Test la méthode Retour
        /// de PartieVueModele
        /// </summary>
        [Fact]
        public void TestRetour()
        {
            SauvegardePartieJson spj = new SauvegardePartieJson("", "test.txt");
            SauvegardeHistoriqueJson shj = new SauvegardeHistoriqueJson("", "histo.txt");
            CalculateurScoreAdditionnel csa = new CalculateurScoreAdditionnel();
            PartieVueModele pvm = new PartieVueModele(new GenerateurFixe(), spj, csa, shj);
            pvm.InitialiserPartie("test", DIFFICULTE.FACILE,false);
            int ancienneCouleur = pvm.Contenu[3].Valeur;
            int nouvelleValeur = 1;
            while (nouvelleValeur == ancienneCouleur && nouvelleValeur < 9)
            {
                nouvelleValeur++;
            }
            pvm.CouleurActive = nouvelleValeur;
            pvm.ChangerCouleurCommand.Execute(3);
            pvm.RetourCommand.Execute(null);
            Assert.Equal(ancienneCouleur, pvm.Contenu[3].Valeur);
            //Verification qu'un retour alors qu'il n'y a plus de 
            //modifications ne pose pas de problème
            pvm.RetourCommand.Execute(null);
            Assert.Equal(ancienneCouleur, pvm.Contenu[3].Valeur);
        }

        /// <summary>
        /// Test le changement de couleur
        /// du vuemodele
        /// </summary>
        [Fact]
        public void TestChangerCouleur()
        {
            CalculateurScoreAdditionnel csa = new CalculateurScoreAdditionnel();
            SauvegardePartieJson spj = new SauvegardePartieJson(Environment.CurrentDirectory, "test2.json");
            SauvegardeHistoriqueJson shj = new SauvegardeHistoriqueJson("", "histo.txt");
            PartieVueModele pvm = new PartieVueModele(new GenerateurFixe(), spj, csa, shj);
            pvm.InitialiserPartie("", DIFFICULTE.FACILE,false);
            pvm.ChangerCouleurActiveCommand.Execute(3);
            int index = 0;
            while (pvm.Contenu[index].Valeur ==3)
                index++;
            int indexInvalide = index + 1;
            pvm.Contenu[index].EstModifiable = true;
            pvm.Contenu[indexInvalide].EstModifiable = false;
            pvm.ChangerCouleurCommand.Execute(index);
            Assert.Equal(3, pvm.Contenu[index].Valeur);
            Assert.Throws<HorsLimiteException>(() => pvm.ChangerCouleurCommand.Execute(-1));
            Assert.Throws<CaseNonModifiableException>(() => pvm.ChangerCouleurCommand.Execute(indexInvalide));
        }
    }
}
