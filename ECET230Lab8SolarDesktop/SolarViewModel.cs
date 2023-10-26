using System.ComponentModel;
using System.Runtime.CompilerServices;

using System.IO.Ports;
using System.Text;
using System.Windows.Input;

namespace ECET230Lab8SolarDesktop;

public class SolarViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private string _rawPacket;

    private SerialPort _serialPort;

    private bool _comPortOpen;

    public ICommand ChangeComPortCommand { get; private set;  }

    public ICommand StartStopComPortCommand { get; private set;}

    public bool ComPortOpen
    {
        get => _comPortOpen;
        set
        {
            _comPortOpen = value;
            OnPropertyChanged();
            if (_comPortOpen)
                _serialPort.Open();
            else
                _serialPort.Close();
        }
    }

    public string[] ComPorts
    {
        get => SerialPort.GetPortNames();

    }

    public SolarViewModel() {
        _serialPort = new();
        _serialPort.DataReceived += SerialPort_DataReceived;

        this.ComPortOpen = false;

        ChangeComPortCommand = new Command<string>(ChangeComPort);

        StartStopComPortCommand = new Command(StartStopComPort);

    }

    private void StartStopComPort()
    {
        this.ComPortOpen = !this.ComPortOpen;
    }

    private void ChangeComPort(string comPort)
    {
        _serialPort.PortName = comPort;
    }

    private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        this.RawPacket = _serialPort.ReadLine();
    }

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

