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

    private bool _isRegistrationVisible;
    public bool IsRegistrationVisible
    {
        get { return _isRegistrationVisible; }
        set
        {
            _isRegistrationVisible = value;
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

    private string _password;
    public string Password
    {
        get { return _password; }
        set { SetProperty(ref _password, value); }
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
                    Name = _name,
                    GameId = Game.Id
                };

                result = await _secretaryService.AddSecretaryAsync(secretary, _password);

                break;
            case "Responsable de salle":

                var roomManager = new RoomManager
                {
                    Name = _name,
                    GameId = Game.Id
                };

                result = await _roomManagerService.AddRoomManagerAsync(roomManager, _password);

                break;
            case "Chronométreur":

                var timekeeper = new Timekeeper
                {
                    Name = _name,
                    GameId = Game.Id
                };

                result = await _timekeeperService.AddTimekeeperAsync(timekeeper, _password);

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

