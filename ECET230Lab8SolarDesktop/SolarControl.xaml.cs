namespace ECET230Lab8SolarDesktop;

public partial class SolarControl : ContentPage
{
	public SolarControl()
	{
		InitializeComponent();

		BindingContext = App.solarViewModel;
	}
}