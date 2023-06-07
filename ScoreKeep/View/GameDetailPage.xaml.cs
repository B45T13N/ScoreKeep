using ScoreKeep.ViewModel;

namespace ScoreKeep.View;

public partial class MatchDetailPage : ContentPage
{

    public MatchDetailPage(SingleGameViewModel gameViewModel)
    {
        InitializeComponent();

        BindingContext = gameViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is SingleGameViewModel viewModel)
        {
            Title = $"{viewModel.Game.Category} contre {viewModel.Game.VisitorTeam.Name}";
        }
    }
}