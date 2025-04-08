using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Outils;
using Outils.CalculateursScore;
using Outils.Generateurs;
using Outils.Sauvegardes;
using Sudokolor.Pages;
using VueModele;

namespace Sudokolor
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiCommunityToolkit()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //Ajout du service de générateur
            builder.Services.AddTransient<IGenerateurGrille,GenerateurAleatoire>();
            //Ajout du service de calcul de score
            builder.Services.AddTransient<ICalculateurScore,CalculateurScoreAdditionnel>();
            //Ajout des services de sauvegarde
            builder.Services.AddTransient<ISauvegardePartie>(x => new SauvegardePartieJson(FileSystem.AppDataDirectory,"partie.json"));
            builder.Services.AddTransient<ISauvegardeHistorique>(x => new SauvegardeHistoriqueJson(FileSystem.AppDataDirectory, "historique.json"));
            builder.Services.AddTransient<PartieVueModele>();
            builder.Services.AddTransient<MenuVueModele>();
            builder.Services.AddTransient<HistoriqueVueModele>();
            builder.Services.AddTransient<PagePartie>();
            builder.Services.AddTransient<PageMenu>();
            builder.Services.AddTransient<PageHistorique>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
