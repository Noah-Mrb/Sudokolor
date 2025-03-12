using CommunityToolkit.Maui.Views;

namespace Sudokolor.Popups;

/// <summary>
/// Popup générique avec un texte customisable et un bouton ok
/// </summary>
/// <author>Nordine</author>
public partial class PopupCustomTexteEtOk : Popup
{
    /// <summary>
    /// Construit une pop up avec le texte en parametre et un bouton ok
    /// </summary>
    /// <param name="TexteAfficher">Texte sur la popup</param>
    /// <author>Nordine</author>
    public PopupCustomTexteEtOk(string TexteAfficher)
    {
        InitializeComponent();
        LabelTexteAfficher.Text = TexteAfficher;
    }

    /// <summary>
    /// ferme la popup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <author>Nordine</author>
    private void FermerPopup(object sender, EventArgs e)
    {
        Close();
    }
}