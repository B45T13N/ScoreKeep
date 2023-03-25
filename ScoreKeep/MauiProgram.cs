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
            });

        builder.Services.AddSingleton<IMatchService, MatchService>();

        builder.Services.AddSingleton<MatchsViewModel>();
        builder.Services.AddSingleton<SingleMatchViewModel>();

        builder.Services.AddSingleton<MatchDetailPage>();
        builder.Services.AddSingleton<MainPage>();

        return builder.Build();
    }
}
