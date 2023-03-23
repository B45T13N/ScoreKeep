namespace ScoreKeep.ViewModel;

public partial class MatchsViewModel : BaseViewModel
{
    private MatchService matchService;

    public ObservableCollection<Match> AllMatchs { get; set; } = new();

    public MatchsViewModel()
    {
        Title = "Tous les matchs";
    }

    public ObservableCollection<Match> LoadAllMatchs()
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
        };

        return AllMatchs;
    }
}
