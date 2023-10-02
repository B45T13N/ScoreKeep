using System.Windows.Input;

namespace ScoreKeep.ViewModel;

[QueryProperty("Game", "Game")]
public partial class SingleGameViewModel : BaseViewModel
{
    [ObservableProperty]
    Game game;

    private readonly IVolunteerService _volunteerService;
    private readonly IVolunteerTypeService _volunteerTypeService;
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

    private ObservableCollection<VolunteerType> _availablePosts;
    public ObservableCollection<VolunteerType> AvailablePosts
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

    private VolunteerType _selectedPost;
    public VolunteerType SelectedPost
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

    public SingleGameViewModel(IVolunteerTypeService volunteerTypeService, IVolunteerService volunteerService, IAlertService alertService)
    {
        IsErrorVisible = false;

        _volunteerTypeService = volunteerTypeService;
        _volunteerService = volunteerService;
        _alertService = alertService;

        ToggleFormCommand = new Command(ExecuteToggleFormCommand);
        IsFormVisible = false;
        AvailablePosts = new ObservableCollection<VolunteerType>();
        SaveCommand = new Command(SaveVolunteer);

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

    private async void SaveVolunteer()
    {
        var result = false;

        var volunteer = new Volunteer
        {
            Name = _name,
            GameId = Game.Id,
            VolunteerTypeId = _selectedPost.Id,
        };

        result = await _volunteerService.AddVolunteerAsync(volunteer, _password);

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

    public async void UpdatePickerItemsSources()
    {
        AvailablePosts.Clear();
        try
        {
            IsBusy = true;
            var volunteerTypes = await _volunteerTypeService.GetAllVolunteerTypesAsync();

            foreach (var volunteerType in volunteerTypes)
            {
                AvailablePosts.Add(volunteerType);
            }

            IsErrorVisible = false;
        }
        catch (Exception)
        {
            ErrorMessage = "Une erreur est survenue lors du chargement des données matchs.";
            IsErrorVisible = true;
        }
        finally
        {
            IsBusy = false;
        }
    }

}

