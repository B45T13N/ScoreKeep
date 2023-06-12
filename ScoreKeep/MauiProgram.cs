using CommunityToolkit.Maui;
using ScoreKeep.ViewModel;

namespace ScoreKeep;

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
            })
            .UseMauiCommunityToolkit();

        builder.Services.AddSingleton<IGameService, GameService>();

        builder.Services.AddSingleton<GamesViewModel>();
        builder.Services.AddSingleton<SingleGameViewModel>();

        builder.Services.AddSingleton<MatchDetailPage>();
        builder.Services.AddSingleton<GamesIndexPage>();

        return builder.Build();
    }
}
