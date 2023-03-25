using CommunityToolkit.Mvvm.Input;

namespace ScoreKeep.ViewModel;

public partial class MatchsViewModel : BaseViewModel
{
    private IMatchService matchService;

    public ObservableCollection<Match> AllMatchs { get; set; } = new();

    public MatchsViewModel()
    {
        Title = "Tous les matchs";
        AllMatchs = new ObservableCollection<Match>();
        AllMatchs = LoadAllMatchs();
    }

    private ObservableCollection<Match> LoadAllMatchs()
    {
        AllMatchs = new ObservableCollection<Match>
        {
            new Match
            {
                Id = 1,
                Categorie = "Sénior M2",
                MatchDate = DateTime.Now,
                VisitorTeamName = "Cession 3",
            },
            new Match
            {
                Id = 1,
                Categorie = "Sénior M1",
                MatchDate = DateTime.Now,
                VisitorTeamName = "Brie 3",
            },
            new Match
            {
                Id = 1,
                Categorie = "Sénior M1",
                MatchDate = DateTime.Now,
                VisitorTeamName = "Brie 3",
            },
            new Match
            {
                Id = 1,
                Categorie = "Sénior M1",
                MatchDate = DateTime.Now,
                VisitorTeamName = "Brie 3",
            },
            new Match
            {
                Id = 1,
                Categorie = "Sénior M1",
                MatchDate = DateTime.Now,
                VisitorTeamName = "Brie 3",
            },
            new Match
            {
                Id = 1,
                Categorie = "Sénior M1",
                MatchDate = DateTime.Now,
                VisitorTeamName = "Brie 3",
            },
            new Match
            {
                Id = 1,
                Categorie = "Sénior M1",
                MatchDate = DateTime.Now,
                VisitorTeamName = "Brie 3",
            },
        };

        return AllMatchs;
    }

    [RelayCommand]
    public async Task GoToDetailsAsync(Match match)
    {
        if (match is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(MatchDetailPage)}", true, new Dictionary<string, object>
        {
            { "Match", match }
        });
    }
}
