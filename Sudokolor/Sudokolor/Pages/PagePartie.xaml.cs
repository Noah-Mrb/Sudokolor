using CommunityToolkit.Maui.Views;
using Modele;
using Sudokolor.Convertisseurs;
using Sudokolor.Popups;
using Sudokolor.Resources.Strings;
using VueModele;

namespace Sudokolor.Pages;

/// <summary>
/// Page de la partie o� il est possible de jouer au Sudoku
/// </summary>
/// <author>Nordine HIDA</author>
[QueryProperty(nameof(Graine), "graine")]
public partial class PagePartie : ContentPage
{
    private PartieVueModele? vm;
    private Button? couleurPaletteActive;

    /// <summary>
    /// Graine passée par query
    /// lors du changement de page
    /// Utilisée pour initialiser le vuemodele
    /// </summary>
    public string Graine
    {
        set
        {
            if (this.vm != null)
            {
                this.vm.InitialiserPartie(value);
            }
        }
    }

    /// <summary>
    /// Renvoi le thème de la partie
    /// </summary>
    public THEME_COULEUR Theme => this.vm.ThemeCouleur;


    // Couleur actuellement s�lectionn�e par le joueur
    private Button? CouleurPaletteActive
    {
        get => couleurPaletteActive;
        set => couleurPaletteActive = value;
    }

    /// <summary>
    /// Constructeur de la page de partie
    /// </summary>
    /// <param name="vm">vuemodele passé par injection de dépendance</param>
    /// <author>Nordine HIDA</author>
    public PagePartie(PartieVueModele vm)
    {
        //initialisation du VueModele
        this.vm = vm;

        InitializeComponent();
        this.BindingContext = vm;
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
        string theme = Preferences.Get("theme", "DEFAUT");
        this.vm.InitialiserTheme((THEME_COULEUR)Enum.Parse(typeof(THEME_COULEUR), theme));
        ConstruireGrille();
    }

    /// <summary>
    /// Renvoie une couleur � partir de la valeur d'une case
    /// </summary>
    /// <param name="valeur">valeur de la case</param>
    /// <returns>couleur �quivalente � la valeur</returns>
    /// <author>Valentin Colindre</author>
    public Color ObtenirCouleur(int valeur)
    {
        return Color.FromRgba("#" + vm.ObtenirCouleur(valeur));
    }


    // Construire la grille de jeu avec des boutons ainsi que les traits de s�paration
    private void ConstruireGrille()
    {
        // Suppression des boutons pr�c�dents
        GrilleJeu.Children.Clear();
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                // Cr�ation du bouton
                Button bouton = new Button
                {
                    CornerRadius = 20,
                    HeightRequest = 30,
                    WidthRequest = 30,
                    FontSize = 14,
                    FontAttributes = FontAttributes.Bold,
                    Padding=0,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Margin = new(5)
                };

                int caseIndex = row * 9 + col;

                // Liaison de la couleur du bouton � la valeur de la case
                bouton.SetBinding(Button.BackgroundColorProperty, new Binding($"Contenu[{caseIndex}].Valeur",
                converter: new ConvertisseurCouleur(),
                converterParameter: this));

                if (this.vm.ThemeCouleur == THEME_COULEUR.CHIFFRES)
                {
                    //Couleur blanche pour le texte
                    bouton.TextColor = Color.FromArgb("#ffffff");
                    bouton.SetBinding(Button.TextProperty, new Binding($"Contenu[{caseIndex}].Valeur",
                    converter: new ConvertisseurChiffre(),
                    converterParameter: this));
                }

                // Change le visuel des cases donn�es au d�but non modifiables
                bouton.IsEnabled = vm.Contenu[caseIndex].EstModifiable;

                // Liaison avec le changement de couleur
                bouton.SetBinding(Button.CommandProperty, new Binding("ChangerCouleurCommand", source: vm));
                var commandParameter = row * 9 + col;
                bouton.CommandParameter = commandParameter;

                DefinirPosition(bouton, row, col);

                GrilleJeu.Children.Add(bouton);
            }
        }

        ConstruireLigneSeparation();
    }


    // D�finit la position d'un bouton dans la
    // grille des cases
    private void DefinirPosition(Button bouton, int row, int col)
    {
        //Si on est dans les 3 premi�res cases
        if (row < 3)
            //On ajoute simplement le bouton � la ligne
            Grid.SetRow(bouton, row);
        //Si on est entre la ligne 3 et 5 (en partant de 0)
        else if (row >= 3 && row < 6)
            //On d�cale de un pour gerer la colonne de s�paration
            Grid.SetRow(bouton, row + 1);
        //pour les 3 derni�res cases on d�cale de 2 pour g�rer les 2 s�parateurs
        else
            Grid.SetRow(bouton, row + 2);

        //Pareil que pour les lignes
        if (col < 3)
            Grid.SetColumn(bouton, col);
        else if (col >= 3 && col < 6)
            Grid.SetColumn(bouton, col + 1);
        else
            Grid.SetColumn(bouton, col + 2);
    }

    // Cr�er les lignes de s�parations toutes les 3 cases
    private void ConstruireLigneSeparation()
    {
        // D�finir la couleur avec 74% d'opacit�
        Color lineColor = Color.FromArgb("#4F391F");
        lineColor = lineColor.MultiplyAlpha(0.74f);
        int epaisseur = 2;

        // Traits verticaux
        BoxView verticalLine1 = new BoxView { Color = lineColor, WidthRequest = epaisseur };
        Grid.SetRowSpan(verticalLine1, 11);
        Grid.SetColumn(verticalLine1, 3);
        GrilleJeu.Children.Add(verticalLine1);

        BoxView verticalLine2 = new BoxView { Color = lineColor, WidthRequest = epaisseur };
        Grid.SetRowSpan(verticalLine2, 11);
        Grid.SetColumn(verticalLine2, 7);
        GrilleJeu.Children.Add(verticalLine2);

        // Traits horizontaux
        BoxView horizontalLine1 = new BoxView { Color = lineColor, HeightRequest = epaisseur };
        Grid.SetColumnSpan(horizontalLine1, 11);
        Grid.SetRow(horizontalLine1, 3);
        GrilleJeu.Children.Add(horizontalLine1);

        BoxView horizontalLine2 = new BoxView { Color = lineColor, HeightRequest = epaisseur };
        Grid.SetColumnSpan(horizontalLine2, 11);
        Grid.SetRow(horizontalLine2, 7);
        GrilleJeu.Children.Add(horizontalLine2);
    }


    // Renvoi vers le menu principal
    private async void RetourAuMenu(object sender, EventArgs e)
    {
        BoutonRetourMenu.IsEnabled = false;
        await Shell.Current.GoToAsync("..");
        BoutonRetourMenu.IsEnabled = true;   
    }

    // Change le visuel de la couleur active
    private void CouleurCliquee(object sender, EventArgs e)
    {
        if (sender is Button bouton && bouton != null)
        {
            if (CouleurPaletteActive != null)
            {
                // R�initialiser la bordure du bouton pr�c�demment s�lectionn�
                CouleurPaletteActive.BorderColor = Colors.Transparent;
                CouleurPaletteActive.BorderWidth = 0;
            }

            // Mise � jour du dernier bouton
            CouleurPaletteActive = (Button)sender;

            // Appliquer la bordure au nouveau bouton s�lectionn�
            CouleurPaletteActive.BorderColor = Colors.White;
            CouleurPaletteActive.BorderWidth = 3;
        }
    }


    // affiche la popup pour la soumission de la grille
    private async void AffichagePopupValidationGrille(Object sender, EventArgs e)
    {
        PopupCustomBooleen popup = new PopupCustomBooleen(
            AppResources.app_popup_soumission_grille_titre,
            AppResources.app_popup_soumission_grille_btn_soumettre,
            AppResources.app_popup_soumission_grille_btn_annuler
        );
        bool? soumettreGrille = (bool?) (await this.ShowPopupAsync(popup));
        if (soumettreGrille == true)
        {
            if (!vm.VerifierGrille())
                AfficherPopupGrilleFausse();
            else
                imageVictoire.IsVisible = true;
        }
    }


    // copie la graine dans le presse-papier et affiche une popup si la copie a r�ussi
    private async void CopierGrainePressePapier(object sender, TappedEventArgs e)
    {
        string texte = labelGraine.Text;
        if (!string.IsNullOrEmpty(texte))
        {
            CopierDansPressePapier(texte);
            AfficherPopUpSuccesCopie();
        }
    }


    // affiche la popup message grille fausse
    private async void AfficherPopupGrilleFausse()
    {
        PopupCustomTexteEtOk popup = new(AppResources.app_popup_grille_fausse);
        await this.ShowPopupAsync(popup);
    }
   
    // copie dans le presse papier le texte pass� en param�tre
    private async void CopierDansPressePapier(string texte)
    {
        await Clipboard.Default.SetTextAsync(texte);
    }

    // Affiche une popup pour indiquer que la copie a �t� effectu�e
    private async void AfficherPopUpSuccesCopie()
    {      
        PopupCustomTexteEtOk popup = new(AppResources.app_page_jeu_confirmation);
        await this.ShowPopupAsync(popup);
    }

    // Ouvre la page des options
    private async void Options(object sender, EventArgs e)
    {
        BoutonOptions.IsEnabled = false;

        await Shell.Current.GoToAsync("Options");

        BoutonOptions.IsEnabled = true;
    }
}
