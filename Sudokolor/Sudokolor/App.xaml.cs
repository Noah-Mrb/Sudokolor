namespace Sudokolor
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Forcer le thème clair
            Application.Current.UserAppTheme = AppTheme.Light;
            MainPage = new AppShell();

        }
    }
}
