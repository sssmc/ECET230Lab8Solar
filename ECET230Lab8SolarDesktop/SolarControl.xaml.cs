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
		App.solarViewModel.SolarPlotField = SolarPlotField;
    }
}