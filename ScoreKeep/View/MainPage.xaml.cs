using ScoreKeep.ViewModel;

namespace ScoreKeep.View;

public partial class MainPage : ContentPage
{
    private MatchsViewModel _matchsViewModel = new();

    public MainPage(MatchsViewModel matchsViewModel)
    {
        InitializeComponent();

        _matchsViewModel = matchsViewModel;

        BindingContext = _matchsViewModel;
    }

    protected override void OnAppearing()
    {

    }
}

