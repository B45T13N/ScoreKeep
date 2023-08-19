using ScoreKeep.ViewModel;

namespace ScoreKeep.View;

public partial class LocalTeamsIndexPage : ContentPage
{
    public LocalTeamsIndexPage(LocalTeamsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is LocalTeamsViewModel viewModel)
        {
            viewModel.UpdateLocalTeamsAsync();
            LocalTeamLikedCollection.ItemsSource = viewModel.LocalTeamsLiked;
            LocalTeamNotLikedCollection.ItemsSource = viewModel.LocalTeamsNotLiked;
            Connectivity.ConnectivityChanged += viewModel.ConnectivityChanged;
        }
    }

    protected override void OnDisappearing()
    {
        if (BindingContext is LocalTeamsViewModel viewModel)
        {
            Connectivity.ConnectivityChanged -= viewModel.ConnectivityChanged;
        }
    }

}

