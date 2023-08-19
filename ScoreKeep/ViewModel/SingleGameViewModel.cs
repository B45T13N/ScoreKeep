using System.Windows.Input;

namespace ScoreKeep.ViewModel;

[QueryProperty("Game", "Game")]
public partial class SingleGameViewModel : BaseViewModel
{
    [ObservableProperty]
    Game game;

    private readonly ISecretaryService _secretaryService;
    private readonly ITimekeeperService _timekeeperService;
    private readonly IRoomManagerService _roomManagerService;
    private readonly IAlertService _alertService;
    public ICommand ToggleFormCommand { get; }

    private bool isFormVisible;
    public bool IsFormVisible
    {
        get { return isFormVisible; }
        set
        {
            isFormVisible = value;
            OnPropertyChanged(nameof(IsFormVisible));
        }
    }

    private bool isRegistrationVisible;
    public bool IsRegistrationVisible
    {
        get { return isRegistrationVisible; }
        set
        {
            isRegistrationVisible = value;
            OnPropertyChanged(nameof(IsRegistrationVisible));
        }
    }

    private ObservableCollection<string> _availablePosts;
    public ObservableCollection<string> AvailablePosts
    {
        get { return _availablePosts; }
        set
        {
            if (_availablePosts != value)
            {
                _availablePosts = value;
                OnPropertyChanged(nameof(AvailablePosts));
            }
        }
    }

    private string _selectedPost;
    public string SelectedPost
    {
        get { return _selectedPost; }
        set { SetProperty(ref _selectedPost, value); }
    }

    private string _name;
    public string Name
    {
        get { return _name; }
        set { SetProperty(ref _name, value); }
    }

    private string _email;
    public string Email
    {
        get { return _email; }
        set { SetProperty(ref _email, value); }
    }

    public ICommand SaveCommand { get; }

    public SingleGameViewModel(ISecretaryService secretaryService, ITimekeeperService timekeeperService, IRoomManagerService roomManagerService, IAlertService alertService)
    {
        _secretaryService = secretaryService;
        _timekeeperService = timekeeperService;
        _roomManagerService = roomManagerService;
        _alertService = alertService;

        ToggleFormCommand = new Command(ExecuteToggleFormCommand);
        IsFormVisible = false;
        AvailablePosts = new ObservableCollection<string>();
        SaveCommand = new Command(SavePerson);
    }

    public void ConnectivityChanged(bool isConnected)
    {
        if (isConnected)
        {
            ErrorMessage = "";
            IsErrorVisible = false;
        }
        else
        {
            ErrorMessage = "Erreur de connexion";
            IsErrorVisible = true;
        }
    }

    private void ExecuteToggleFormCommand()
    {
        IsFormVisible = !IsFormVisible;
    }

    private async void SavePerson()
    {
        var result = false;
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        switch (_selectedPost)
        {
            case "Secrétaire":

                var secretary = new Secretary
                {
                    Email = _email,
                    Name = _name,
                    GameId = Game.Id
                };

                result = await _secretaryService.AddSecretaryAsync(secretary);

                break;
            case "Responsable de salle":

                var roomManager = new RoomManager
                {
                    Name = _name,
                    Email = _email,
                    GameId = Game.Id
                };

                result = await _roomManagerService.AddRoomManagerAsync(roomManager);

                break;
            case "Chronométreur":

                var timekeeper = new Timekeeper
                {
                    Email = _email,
                    Name = _name,
                    GameId = Game.Id
                };

                result = await _timekeeperService.AddTimekeeperAsync(timekeeper);

                break;
        }

        if (result)
        {
            await _alertService.ShowAlertAsync("Vous êtes enregistré !", $"Enregistrement en tant que {_selectedPost} effectué");

            await Microsoft.Maui.Controls.Application.Current.MainPage.Navigation.PopAsync();
        }
        else
        {
            await _alertService.ShowAlertAsync("Veuillez réessayer", "Erreur lors de l'enregistrement");
        }
    }



}

