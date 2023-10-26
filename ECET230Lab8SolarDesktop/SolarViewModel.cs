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
            return ((float)_analog4Voltage - (float)_analog1Voltage) / 100f;
        }
    }

    public string Load1CurrentText
    {
        get
        {
            return Load1Current.ToString("###0.##") + "mA";
        }
    }

    public float Load2Current
    {
        get
        {
            return ((float)_analog3Voltage - (float)_analog1Voltage) / 100f;
        }
    }

    public string Load2CurrentText
    {
        get
        {
            return Load2Current.ToString("###0.##") + "mA";
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


    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}

