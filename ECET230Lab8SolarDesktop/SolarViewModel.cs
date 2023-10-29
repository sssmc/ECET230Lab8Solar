using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ECET230Lab8SolarDesktop;

public class SolarViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private float _analog0Voltage;
    private float _analog1Voltage;
    private float _analog2Voltage;
    private float _analog3Voltage;
    private float _analog4Voltage;

    private bool _isLoad1On;
    private bool _isLoad2On;

    public bool IsLoad1On
    {
        get => _isLoad1On;
        set
        {
            _isLoad1On = value;
            OnPropertyChanged("IsLoad1On");
            OnPropertyChanged("IsLoad1Off");
        }
    }

    public bool IsLoad2On
    {
        get => _isLoad2On;
        set
        {
            _isLoad2On = value;
            OnPropertyChanged("IsLoad2On");
            OnPropertyChanged("IsLoad2Off");
        }
    }

    public bool IsLoad1Off
    {
        get => !_isLoad1On;
        set
        {
            _isLoad1On = !value;
            OnPropertyChanged("IsLoad1On");
            OnPropertyChanged("IsLoad1Off");
        }
    }

    public bool IsLoad2Off
    {
        get => !_isLoad2On;
        set
        {
            _isLoad2On = !value;
            OnPropertyChanged("IsLoad2On");
            OnPropertyChanged("IsLoad2Off");
        }
    }

    public int Analog0Voltage {
        set
        {
            _analog0Voltage = value;
            OnPropertyChanged();
            OnPropertyChanged("SolarVoltage");
            OnPropertyChanged("SolarVoltageText");
        }
    }

    public int Analog1Voltage
    {
        set
        {
            _analog1Voltage = value;
            OnPropertyChanged();
            OnPropertyChanged("BatteryChargingCurrent");
            OnPropertyChanged("BatteryChargingCurrentText");
        }
    }

    public int Analog2Voltage
    {
        set
        {
            _analog2Voltage = value;
            OnPropertyChanged();
            OnPropertyChanged("BatteryChargingCurrent");
            OnPropertyChanged("BatteryChargingCurrentText");
            OnPropertyChanged("BatteryVoltage");
            OnPropertyChanged("BatteryVoltageText");
        }
    }

    public int Analog3Voltage
    {
        set
        {
            _analog3Voltage = value;
            OnPropertyChanged();
            OnPropertyChanged("Load2Current");
            OnPropertyChanged("Load2CurrentText");
        }
    }

    public int Analog4Voltage
    {
        set
        {
            _analog4Voltage = value;
            OnPropertyChanged("Load1Current");
            OnPropertyChanged("Load1CurrentText");
        }
    }

    public float SolarVoltage
    {
        get => _analog0Voltage;
    }

    public string SolarVoltageText
    {
        get
        {
            return ((float)SolarVoltage/1000f).ToString("0.00") + "V";
        }
    }

    public float BatteryVoltage
    {
        get => _analog2Voltage;
    }

    public string BatteryVoltageText
    {
        get
        {
            return ((float)BatteryVoltage/1000f).ToString("0.00") + "V";
        }
    }

    public float Load1Current
    {
        get 
        {
            return ((float)_analog1Voltage - (float)_analog4Voltage) / 100f;
        }
    }

    public string Load1CurrentText
    {
        get
        {
            return Load1Current.ToString(" ##00.0#mA;-##00.0#mA; ##00.0#mA");
        }
    }

    public float Load2Current
    {
        get
        {
            return ((float)_analog1Voltage - (float)_analog3Voltage) / 100f;
        }
    }

    public string Load2CurrentText
    {
        get
        {
            return Load2Current.ToString(" ##00.0#mA;-##00.0#mA; ##00.0#mA");
        }
    }

    public float BatteryChargingCurrent
    {
        get
        {
            return (float)(_analog1Voltage - _analog2Voltage) / 100f;
        }
    }

    public string BatteryChargingCurrentText
    {
        get
        {
            return BatteryChargingCurrent.ToString("###0.##") + "mA";
        }
    }

    public SolarViewModel()
    {
        _analog0Voltage = 0;
        _analog1Voltage = 0;
        _analog2Voltage = 0;
        _analog3Voltage = 0;
        _analog4Voltage = 0;

        _isLoad1On = false;
        _isLoad2On = false;


    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}

