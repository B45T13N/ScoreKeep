using CommunityToolkit.Mvvm.Input;

namespace ScoreKeep.ViewModel;

public partial class GamesViewModel : BaseViewModel
{
    private readonly IGameService _gameService;

    public ObservableCollection<Game> AllGames { get; set; } = new();

    public GamesViewModel(IGameService gameService)
    {
        this._gameService = gameService;

        Title = "Avon Handball";

        _ = LoadAllGamesAsync();
    }

    private async Task LoadAllGamesAsync()
    {
        try
        {
            IsBusy = true;
            var games = await _gameService.GetGamesAsync();

            if (games.Count == 0)
            {
                ErrorMessage = "Aucun match trouvé.";
                IsErrorVisible = true;
            }
            else
            {
                foreach (var game in games)
                {
                    AllGames.Add(game);
                }

                IsErrorVisible = false;
            }

        }
        catch (Exception e)
        {
            ErrorMessage = "Une erreur est survenue lors du chargement des matchs.";
            IsErrorVisible = true;
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public async Task GoToDetailsAsync(Game game)
    {
        if (game is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(MatchDetailPage)}", true, new Dictionary<string, object>
        {
            { "Game", game }
        });
    }
}
