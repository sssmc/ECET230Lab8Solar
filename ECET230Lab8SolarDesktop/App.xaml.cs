namespace ECET230Lab8SolarDesktop;

public partial class App : Application
{
    public static SolarViewModel solarViewModel;
    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();

		solarViewModel = new SolarViewModel();
	}
}

