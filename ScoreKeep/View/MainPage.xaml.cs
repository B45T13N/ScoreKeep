using ScoreKeep.ViewModel;

namespace ScoreKeep.View;

public partial class MainPage : ContentPage
{
    public MainPage(GamesViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is GamesViewModel viewModel)
        {
            GameCollection.ItemsSource = viewModel.AllGames;
        }
    }
}

