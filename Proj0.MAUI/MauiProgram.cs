using Microsoft.Extensions.Logging;

namespace Proj0.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("underwood_champion.ttf", "UnderwoodChampion");
                    fonts.AddFont("Harting_plain.ttf", "HartingPlain");
                    fonts.AddFont("Compagnon-Roman.otf", "CompagnonRoman");
                    fonts.AddFont("Compagnon-Light.otf", "CompagnonLight");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
