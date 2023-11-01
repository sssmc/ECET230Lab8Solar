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

    private GraphicsView _solarPlotField;

    private List<float> _SolarVoltagePlotPoints;
    private List<float> _BatteryVoltagePlotPoints;
    private List<float> _BatteryChargingCurrentPlotPoints;
    private List<float> _Load1CurrentPlotPoints;
    private List<float> _Load2CurrentPlotPoints;

    private List<DateTime> _plotPointsX;
    
    private const int maxPlotPoints = 600;
    private int _plotPointsCount = 0;

    public GraphicsView SolarPlotField
    {
        get => _solarPlotField;
        set
        {
            _solarPlotField = value;
            OnPropertyChanged("SolarPlotField");
        }
    }
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
            _SolarVoltagePlotPoints.Add(SolarVoltage);
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
            _BatteryChargingCurrentPlotPoints.Add(BatteryChargingCurrent * 1000f);
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
            _BatteryVoltagePlotPoints.Add(BatteryVoltage);
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
            _Load2CurrentPlotPoints.Add(Load2Current * 1000f);
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
            _Load1CurrentPlotPoints.Add(Load1Current * 1000f);
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
            if ((_analog1Voltage - _analog2Voltage) == 0f || _analog2Voltage == 0)
            {
                return 0f;
            }
            else
            {   
                return (float)(_analog1Voltage - _analog2Voltage) / 100f;
            }
        }
    }

    public string BatteryChargingCurrentText
    {
        get
        {
            return BatteryChargingCurrent.ToString("###0.##") + "mA";
        }
    }

    public float[][] PlotDataY
    {
        get
        {
            return new float[5][] { _SolarVoltagePlotPoints.ToArray(), 
                                    _BatteryVoltagePlotPoints.ToArray(),
                                    _BatteryChargingCurrentPlotPoints.ToArray(),
                                    _Load1CurrentPlotPoints.ToArray(), 
                                    _Load2CurrentPlotPoints.ToArray() };
        }
    }

    public DateTime[] PlotDataX
    {
        get
        {
            return _plotPointsX.ToArray();
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

        _plotPointsX = new List<DateTime>();

        _SolarVoltagePlotPoints = new List<float>();
        _BatteryVoltagePlotPoints = new List<float>();
        _Load1CurrentPlotPoints = new List<float>();
        _Load2CurrentPlotPoints = new List<float>();
        _BatteryChargingCurrentPlotPoints = new List<float>();

    }

    public void AddXPlotPoint(DateTime dateTime)
    {
        _plotPointsX.Add(dateTime);
        _plotPointsCount++;
        if(_plotPointsCount > maxPlotPoints)
        {
            _plotPointsX.RemoveAt(0);
            _SolarVoltagePlotPoints.RemoveAt(0);
            _BatteryVoltagePlotPoints.RemoveAt(0);
            _Load1CurrentPlotPoints.RemoveAt(0);
            _Load2CurrentPlotPoints.RemoveAt(0);
            _BatteryChargingCurrentPlotPoints.RemoveAt(0);
            _plotPointsCount--;
        }
    }

    public void OnPropertyChanged([CallerMemberName] string name = "")
    {
        if(_solarPlotField != null)
        {
            //_solarPlotField.Invalidate();
        }
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

