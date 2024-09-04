using System.ComponentModel;

public class Ponto : INotifyPropertyChanged
{
    private bool isSelected;

    public bool IsSelected
    {
        get { return isSelected; }
        set
        {
            if (isSelected != value)
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
    }

    public DateTime Data { get; set; }
    public TimeSpan? HoraInicio { get; set; }
    public TimeSpan? HoraAlmoco { get; set; }
    public TimeSpan? HoraRetorno { get; set; }
    public TimeSpan? HoraFim { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
