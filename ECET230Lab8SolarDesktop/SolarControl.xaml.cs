namespace ECET230Lab8SolarDesktop;

public partial class SolarControl : ContentPage
{
	string rawPacket;

	public SolarControl()
	{
		InitializeComponent();

		BindingContext = App.solarViewModel;
	}
}