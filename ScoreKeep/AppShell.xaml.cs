namespace ScoreKeep;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(GameDetailPage), typeof(GameDetailPage));
        Routing.RegisterRoute(nameof(GamesIndexPage), typeof(GamesIndexPage));
    }
}
