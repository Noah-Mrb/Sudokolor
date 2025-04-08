using Sudokolor.Pages;

namespace Sudokolor
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Menu", typeof(PageMenu));
            Routing.RegisterRoute("Partie", typeof(PagePartie));
            Routing.RegisterRoute("Options",typeof(PageOptions));
            Routing.RegisterRoute("Historique", typeof(PageHistorique));
        }
    }
}
