using System.ComponentModel;

namespace ScoreKeep.Business.Models;

public class LocalTeam : INotifyPropertyChanged
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Logo { get; set; }
    public string HeartImagePath { get; set; }

    private bool _isLiked;
    public bool IsLiked
    {
        get => _isLiked;
        set
        {
            if (_isLiked != value)
            {
                _isLiked = value;
                OnPropertyChanged(nameof(IsLiked));
            }

            HeartImagePath = _isLiked ? "is_like.png" : "is_not_like.png";
            OnPropertyChanged(nameof(HeartImagePath));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}