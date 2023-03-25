namespace ScoreKeep.ViewModel;

public class SingleMatchViewModel : BaseViewModel
{
    private IMatchService _matchService;
    public Match Match { get; set; }

    public SingleMatchViewModel()
    {
        Match = LoadMatchDetail();
        Title = $"{Match.LocalTeam.Name} contre {Match.VisitorTeamName}";

    }

    private Match LoadMatchDetail()
    {
        return new Match
        {
            Categorie = "Sénior",
            IdGymnase = 1,
            IdLocalTeam = 1,
            IdRoomManager = 2,
            IdSecretaty = 2,
            IdTimeKeeper = 3,
            LocalTeam = new LocalTeam
            {
                Name = "Avon Handball"
            },
            MatchDate = DateTime.UtcNow,
            VisitorTeamName = "Nangis 3"
        };
    }
}

