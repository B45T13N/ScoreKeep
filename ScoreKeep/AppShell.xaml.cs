namespace ScoreKeep;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(GamesIndexPage), typeof(GamesIndexPage));
        Routing.RegisterRoute(nameof(MatchDetailPage), typeof(MatchDetailPage));
    }
}
