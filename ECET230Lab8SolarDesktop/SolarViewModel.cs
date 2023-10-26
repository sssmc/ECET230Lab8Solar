using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ECET230Lab8SolarDesktop;

public class SolarViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private string _rawPacket;

    public SolarViewModel() { }

    public string RawPacket
    {
        get => _rawPacket;
        set
        {
            _rawPacket = value;
            OnPropertyChanged();
        }
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}

