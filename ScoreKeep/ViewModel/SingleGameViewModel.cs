namespace ScoreKeep.ViewModel;

[QueryProperty("Game", "Game")]
public partial class SingleGameViewModel : BaseViewModel
{
    [ObservableProperty]
    Game game;

    private readonly IGameService _gameService;

    public SingleGameViewModel(IGameService gameService)
    {
        this._gameService = gameService;
    }

    public void ConnectivityChanged(bool isConnected)
    {
        if (isConnected)
        {
            ErrorMessage = "";
            IsErrorVisible = false;
        }
        else
        {
            ErrorMessage = "Erreur de connexion";
            IsErrorVisible = true;
        }
    }
}

