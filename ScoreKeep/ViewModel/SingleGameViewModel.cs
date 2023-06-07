namespace ScoreKeep.ViewModel;

[QueryProperty("Game", "Game")]
public partial class SingleGameViewModel : BaseViewModel
{
    [ObservableProperty]
    Game game;

}

