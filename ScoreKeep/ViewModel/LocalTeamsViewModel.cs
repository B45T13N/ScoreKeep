namespace ScoreKeep.ViewModel;

public partial class LocalTeamsViewModel : BaseViewModel
{
    private readonly ILocalTeamService _localTeamService;

    public ObservableCollection<LocalTeam> LocalTeamsNotLiked { get; set; } = new();
    public ObservableCollection<LocalTeam> LocalTeamsLiked { get; set; } = new();

    public LocalTeamsViewModel(ILocalTeamService localTeamService)
    {
        this._localTeamService = localTeamService;

        Title = "Scorekeep".ToUpper();

    }

    private async Task LoadLocalTeamsAsync()
    {
        LocalTeamsNotLiked.Clear();
        LocalTeamsLiked.Clear();
        try
        {
            IsBusy = true;
            var localTeams = await _localTeamService.GetLocalTeamsAsync();

            if (localTeams.Count == 0)
            {
                ErrorMessage = "Aucune équipe trouvée.";
                IsErrorVisible = true;
            }
            else
            {
                foreach (var localTeam in localTeams)
                {
                    var updatedLocalTeam = CheckIfLocalTeamIsAlreadyLiked(localTeam);
                    if (updatedLocalTeam.IsLiked)
                    {
                        LocalTeamsLiked.Add(updatedLocalTeam);

                    }
                    else
                    {
                        LocalTeamsNotLiked.Add(updatedLocalTeam);
                    }
                }

                IsErrorVisible = false;
            }

        }
        catch (Exception e)
        {
            ErrorMessage = "Une erreur est survenue lors du chargement des équipes.";
            IsErrorVisible = true;
        }
        finally
        {
            IsBusy = false;
        }
    }

    public void UpdateLocalTeamsAsync()
    {
        _ = LoadLocalTeamsAsync();
    }

    public void ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
        if (e.NetworkAccess == NetworkAccess.Internet)
        {
            _ = LoadLocalTeamsAsync();
            ErrorMessage = "";
            IsErrorVisible = false;
        }
        else
        {
            LocalTeamsNotLiked.Clear();
            LocalTeamsLiked.Clear();
            ErrorMessage = "La connexion internet a été perdue";
            IsErrorVisible = true;
        }
    }

    [RelayCommand]
    public async Task GotToGameListAsync(LocalTeam localTeam)
    {
        if (localTeam is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(GamesIndexPage)}", true, new Dictionary<string, object>
        {
            { "LocalTeam", localTeam}
        });
    }

    [RelayCommand]
    public void HandleHeartButtonClick(LocalTeam localTeam)
    {
        localTeam.IsLiked = !localTeam.IsLiked;

        if (localTeam.IsLiked)
        {
            Preferences.Set(localTeam.Id.ToString(), localTeam.Name);
            LocalTeamsNotLiked.Remove(localTeam);
            LocalTeamsLiked.Add(localTeam);
        }
        else
        {
            Preferences.Remove(localTeam.Id.ToString());
            LocalTeamsNotLiked.Add(localTeam);
            LocalTeamsLiked.Remove(localTeam);
        }
    }

    private LocalTeam CheckIfLocalTeamIsAlreadyLiked(LocalTeam localTeam)
    {
        if (Preferences.Get(localTeam.Id.ToString(), "") != "")
        {
            localTeam.IsLiked = true;
        }
        else
        {
            localTeam.IsLiked = false;
        }

        return localTeam;
    }
}
