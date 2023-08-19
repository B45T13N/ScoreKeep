namespace ScoreKeep.ViewModel;

[QueryProperty("LocalTeam", "LocalTeam")]

public partial class GamesViewModel : BaseViewModel
{
    [ObservableProperty]
    LocalTeam localTeam;

    private readonly IGameService _gameService;

    public ObservableCollection<Game> AllGames { get; set; } = new();

    public GamesViewModel(IGameService gameService)
    {
        this._gameService = gameService;

        Title = LocalTeam.Name;

    }

    private async Task LoadAllGamesAsync()
    {
        AllGames.Clear();
        try
        {
            IsBusy = true;
            var games = await _gameService.GetGamesAsync(LocalTeam.Id);

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

    public void UpdateGames()
    {
        _ = LoadAllGamesAsync();
    }

    public void ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
        if (e.NetworkAccess == NetworkAccess.Internet)
        {
            _ = LoadAllGamesAsync();
            ErrorMessage = "";
            IsErrorVisible = false;
        }
        else
        {
            AllGames.Clear();
            ErrorMessage = "La connexion internet a été perdue";
            IsErrorVisible = true;
        }
    }

    [RelayCommand]
    public async Task GoToDetailsAsync(Game game)
    {
        if (game is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(GameDetailPage)}", true, new Dictionary<string, object>
        {
            { "Game", game }
        });
    }
}
