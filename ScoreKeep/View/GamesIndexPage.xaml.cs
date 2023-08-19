using ScoreKeep.ViewModel;

namespace ScoreKeep.View;

public partial class GamesIndexPage : ContentPage
{
    public GamesIndexPage(GamesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is GamesViewModel viewModel)
        {
            viewModel.Title = viewModel.LocalTeam.Name.ToUpper();

            viewModel.UpdateGames();
            GameCollection.ItemsSource = viewModel.AllGames;
            Connectivity.ConnectivityChanged += viewModel.ConnectivityChanged;
        }
    }



    protected override void OnDisappearing()
    {
        if (BindingContext is GamesViewModel viewModel)
        {
            Connectivity.ConnectivityChanged -= viewModel.ConnectivityChanged;
        }
    }
}

