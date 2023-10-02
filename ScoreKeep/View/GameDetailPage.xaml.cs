using ScoreKeep.ViewModel;

namespace ScoreKeep.View;

public partial class GameDetailPage : ContentPage
{

    const string ErrorTitle = "Erreur de connexion";
    const string ErrorText = "La connexion internet a été perdue";

    public GameDetailPage(SingleGameViewModel singleGameViewModel)
    {
        InitializeComponent();

        BindingContext = singleGameViewModel;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is SingleGameViewModel viewModel)
        {
            this.PageTitle.Text = $"{viewModel.Game.VisitorTeam.Name} / {viewModel.Game.Category}";
            viewModel.IsFormVisible = false;
            viewModel.SelectedPost = new VolunteerType();
            viewModel.IsRegistrationVisible = viewModel.AvailablePosts.Count != 0 && viewModel.Game.IsHomeMatch;
            viewModel.UpdatePickerItemsSources();
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
                viewModel.IsRegistrationVisible = viewModel.AvailablePosts.Count != 0;
                viewModel.ConnectivityChanged(true);
            }
        }
        else
        {
            DisplayAlert(ErrorTitle, ErrorText, "OK");
            if (BindingContext is SingleGameViewModel viewModel)
            {
                viewModel.IsRegistrationVisible = false;
                viewModel.ConnectivityChanged(false);
            }
        }
    }

}