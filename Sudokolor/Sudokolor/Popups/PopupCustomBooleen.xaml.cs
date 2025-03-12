using CommunityToolkit.Maui.Views;
using VueModele;

namespace Sudokolor.Popups;
/// <summary>
/// Popup booleen custom :
/// deux options de retour, vrai ou feux
/// </summary>
public partial class PopupCustomBooleen : Popup
{
    /// <summary>
    /// Créer la popup à partir des paramètres
    /// </summary>
    /// <param name="texte">texte central de la popup</param>
    /// <param name="texteBoutonVrai">texte du bouton vrai</param>
    /// <param name="texteBoutonFaux">texte du bouton faux</param>
    public PopupCustomBooleen(
        string texte, 
        string texteBoutonVrai, 
        string texteBoutonFaux
    )
	{
		InitializeComponent();
        Texte.Text = texte;
        BoutonFaux.Text = texteBoutonFaux;
        BoutonVrai.Text = texteBoutonVrai;
	}

    /// <summary>
    /// Ferme le popup sans soumettre la solution 
    /// </summary>
    /// <author>Noah Mirbel</author>
    private void Faux(object sender, EventArgs e)
    {
        Close(false);
    }

    /// <summary>
    /// ferme la popup avec un retour permetant de v�rifier la solution
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Vrai(object sender, EventArgs e)
    {
        Close(true);
    }
}