using Modele;
using Modele.Exceptions;
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
            SauvegardePartieJson spj = new SauvegardePartieJson("","");
            Grille grille = gf.GenererGrille();

            Case[,] contenuDouble = new Case[9, 9];

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    contenuDouble[i, j] = grille[i, j];

            PartieVueModele pvm = new PartieVueModele(gf,spj);
            pvm.Initialiser("",THEME_COULEUR.DEFAUT);

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
            SauvegardePartieJson spj = new SauvegardePartieJson("", "");
            PartieVueModele pvm = new PartieVueModele(new GenerateurFixe(),spj);
            Assert.Matches(@"^#[0-9A-Fa-f]{6}$", "#" +pvm.ObtenirCouleur(0));
        }

        /// <summary>
        /// Test la méthode ToutesLesCasesRemplies à travers la propriété ToutesLesCasesSontRemplies
        /// </summary>
        [Fact]
        public void TestCasesGrillePleines()
        {
            PartieVueModele pvm = new PartieVueModele(new GenerateurFixe(),new SauvegardePartieJson("",""));
            Assert.False(pvm.ToutesLesCasesSontRemplies);
        }
        
        /// <summary>
        /// Test la méthode Retour
        /// de PartieVueModele
        /// </summary>
        [Fact]
        public void TestRetour()
        {
            SauvegardePartieJson spj = new SauvegardePartieJson("", "");
            PartieVueModele pvm = new PartieVueModele(new GenerateurFixe(), spj);
            pvm.Initialiser("", THEME_COULEUR.DEFAUT);
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
            SauvegardePartieJson spj = new SauvegardePartieJson(Environment.CurrentDirectory, "test2.json");
            PartieVueModele pvm = new PartieVueModele(new GenerateurFixe(), spj);
            pvm.Initialiser("",THEME_COULEUR.DEFAUT);
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
