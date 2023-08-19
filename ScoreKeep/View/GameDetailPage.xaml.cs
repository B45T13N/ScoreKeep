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
            viewModel.SelectedPost = String.Empty;
            UpdatePickerItemsSources(viewModel);
            viewModel.IsRegistrationVisible = viewModel.AvailablePosts.Count != 0;

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
                UpdatePickerItemsSources(viewModel);
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
    private void UpdatePickerItemsSources(SingleGameViewModel viewModel)
    {
        viewModel.AvailablePosts.Clear();

        if (viewModel.Game.Secretary is null)
            viewModel.AvailablePosts.Add("Secrétaire");

        if (viewModel.Game.RoomManager is null)
            viewModel.AvailablePosts.Add("Responsable de salle");

        if (viewModel.Game.Timekeeper is null)
            viewModel.AvailablePosts.Add("Chronométreur");

    }

}