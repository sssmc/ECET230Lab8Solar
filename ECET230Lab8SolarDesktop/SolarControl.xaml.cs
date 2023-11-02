namespace ECET230Lab8SolarDesktop;

public partial class SolarControl : ContentPage
{
	public SolarControl()
	{
		InitializeComponent();

		BindingContext = App.solarViewModel;
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
		//Set the SolarPlotField property of the SolarViewModel to the SolarPlotField object
		App.solarViewModel.SolarPlotField = SolarPlotField;
    }
}