using Sudokolor.Resources.Strings;
using VueModele;

namespace Sudokolor.Pages;

public partial class PageOptions : ContentPage
{
	private OptionsVueModele vm;

	public PageOptions()
    {
        vm = new OptionsVueModele();
		InitializeComponent();
		this.BindingContext = vm;

        InitialiserThemes();
        InitialiserLangues();
    }

    /// <summary>
    /// Initialise la liste des th�mes disponibles et le th�me actuel
    /// </summary>
    /// <author>Romain Card</author>
    private void InitialiserThemes()
    {
        // Initialise le theme de l'application avec les pr�f�rences de l'utilisateur
        string theme = Preferences.Get("theme", "DEFAUT");

        Dictionary<string, string> themes = ObtenirThemesCouleurs();
        Theme.ItemsSource = themes.Values.ToList();

        // S�lectionne le th�me choisi par l'utilisateur
        Theme.SelectedItem = themes[theme];
    }

    /// <summary>
    /// Initialise la liste des langues et la langue actuelle
    /// </summary>
    /// <author>Romain Card</author>
    private void InitialiserLangues()
    {
        // Initialise la langue de l'application avec les pr�f�rences de l'utilisateur
        string langue = Preferences.Get("langue", "");

        Langue.ItemsSource = vm.Langues.Keys.ToList();

        // R�cup�re la cl� de la langue actuelle dans le dictionnaire de langues
        Langue.SelectedItem = (string) vm.Langues.FirstOrDefault(x => x.Value == langue).Key ?? "";
    }

    /// <summary>
    /// R�cup�re la liste des types d'affichage disponibles
    /// </summary>
    /// <returns>Dictionnaire de th�mes couleurs avec leur valeur textuelle</returns>
    /// <author>Romain Card</author>
    private Dictionary<string, string> ObtenirThemesCouleurs()
    {
        return new Dictionary<string, string> {
            { "DEFAUT", AppResources.app_page_options_theme_defaut },
            { "DALTONIEN", AppResources.app_page_options_theme_daltonien },
            { "CHIFFRES", AppResources.app_page_options_theme_chiffres }
        };
    }
    /// <summary>
    /// Change le type d'affichage des cases de la grille
    /// </summary>
    /// <param name="sender">picker du th�me</param>
    /// <param name="e">arguments de l'appel</param>
    /// <author>Romain Card</author>
    private void ChangeTheme(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        Dictionary<string, string> themes = ObtenirThemesCouleurs();

        // d�finit le th�me en prenant l'indice (= nom du th�me) � partir de sa valeur (= texte du th�me)
        Preferences.Set("theme", themes.FirstOrDefault(x => x.Value == picker.SelectedItem.ToString()).Key);
    }

    /// <summary>
    /// Change la langue de l'application
    /// </summary>
    /// <param name="sender">picker de la langue</param>
    /// <param name="e">arguments de l'appel</param>
    /// <author>Romain Card</author>
    private void ChangeLangage(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        Preferences.Set("langue", vm.Langues[picker.SelectedItem.ToString() ?? ""]);
    }

    /// <summary>
    /// Renvoi vers le menu principal
    /// </summary>
    /// <author>Romain Card</author>
    private async void RetourAuMenu(object sender, EventArgs e)
    {
        BoutonRetourMenu.IsEnabled = false;
        RondDeChargement.IsRunning = true;
        await Shell.Current.GoToAsync("..");
        BoutonRetourMenu.IsEnabled = true;
        RondDeChargement.IsRunning = false;
    }
}