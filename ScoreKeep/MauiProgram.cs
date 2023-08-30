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
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Barlow-Regular.ttf", "Barlow");
                fonts.AddFont("Barlow-SemiBoldItalic.ttf", "BarlowSemiBoldItalic");
                fonts.AddFont("Barlow-SemiBold.ttf", "BarlowSemiBold");
            });

        builder.Services.AddSingleton<IHttpClientProvider, HttpClientProvider>();

        builder.Services.AddSingleton<IGameService, GameService>();
        builder.Services.AddSingleton<ILocalTeamService, LocalTeamService>();
        builder.Services.AddSingleton<ISecretaryService, SecretaryService>();
        builder.Services.AddSingleton<IRoomManagerService, RoomManagerService>();
        builder.Services.AddSingleton<ITimekeeperService, TimekeeperService>();
        builder.Services.AddSingleton<IAlertService, AlertService>();

        builder.Services.AddSingleton<GamesViewModel>();
        builder.Services.AddSingleton<LocalTeamsViewModel>();
        builder.Services.AddSingleton<SingleGameViewModel>();

        builder.Services.AddSingleton<GameDetailPage>();
        builder.Services.AddSingleton<LocalTeamsIndexPage>();
        builder.Services.AddSingleton<GamesIndexPage>();

        return builder.Build();
    }
}
