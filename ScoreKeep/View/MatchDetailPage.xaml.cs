using ScoreKeep.ViewModel;

namespace ScoreKeep.View;

public partial class MatchDetailPage : ContentPage
{
	private SingleMatchViewModel _singleMatchViewModel = new();

	public MatchDetailPage(SingleMatchViewModel matchViewModel)
	{
		InitializeComponent();

		matchViewModel = _singleMatchViewModel;

		BindingContext = matchViewModel;

	}
}