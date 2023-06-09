using ScoreKeep.ViewModel;

namespace ScoreKeep.View;

public partial class MatchDetailPage : ContentPage
{

    const string ErrorTitle = "Erreur de connexion";
    const string ErrorText = "La connexion internet a été perdue";

    public MatchDetailPage(SingleGameViewModel singleGameViewModel)
    {
        InitializeComponent();

        BindingContext = singleGameViewModel;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is SingleGameViewModel viewModel)
        {
            Title = $"{viewModel.Game.Category} contre {viewModel.Game.VisitorTeam.Name}";
        }

        Connectivity.ConnectivityChanged += ConnectivityChanged;
    }

    protected override void OnDisappearing()
    {
        Connectivity.ConnectivityChanged -= ConnectivityChanged;
    }

    private void ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
        if (e.NetworkAccess == NetworkAccess.Internet)
        {
            if (BindingContext is SingleGameViewModel viewModel)
            {
                viewModel.ConnectivityChanged(true);
            }
        }
        else
        {
            DisplayAlert(ErrorTitle, ErrorText, "OK");
            if (BindingContext is SingleGameViewModel viewModel)
            {
                viewModel.ConnectivityChanged(false);
            }
        }
    }
}