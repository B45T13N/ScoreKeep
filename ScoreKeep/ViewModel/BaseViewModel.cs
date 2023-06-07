using System.ComponentModel;

namespace ScoreKeep.ViewModel;

public partial class BaseViewModel : ObservableObject, INotifyPropertyChanged
{
    private string _errorMessage;
    public string ErrorMessage
    {
        get { return _errorMessage; }
        set { SetProperty(ref _errorMessage, value); }
    }

    private bool _isErrorVisible;
    public bool IsErrorVisible
    {
        get { return _isErrorVisible; }
        set { SetProperty(ref _isErrorVisible, value); }
    }

    public BaseViewModel()
    {

    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool isBusy;

    [ObservableProperty]
    private string title;
    public bool IsNotBusy => !isBusy;
}

