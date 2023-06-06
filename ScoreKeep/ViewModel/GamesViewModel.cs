using CommunityToolkit.Mvvm.Input;

namespace ScoreKeep.ViewModel;

public partial class GamesViewModel : BaseViewModel
{
    private IGameService gameService;

    public ObservableCollection<Game> AllGames { get; set; } = new();

    public GamesViewModel(IGameService gameService)
    {
        Title = "Tous les matchs";
        this.gameService = gameService;
        _ = LoadAllGamesAsync();
    }

    private async Task LoadAllGamesAsync()
    {
        var games = await gameService.GetGamesAsync();
        foreach (var game in games)
        {
            AllGames.Add(game);
        }
    }

    [RelayCommand]
    public async Task GoToDetailsAsync(Game game)
    {
        if (game is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(MatchDetailPage)}", true, new Dictionary<string, object>
        {
            { "Match", game }
        });
    }
}
