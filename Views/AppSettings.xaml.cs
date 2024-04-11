using Terms.Services;

namespace Terms.Views;

public partial class AppSettings : ContentPage
{
	public AppSettings()
	{
		InitializeComponent();
	}

    private void ClearPreferences_Clicked(object sender, EventArgs e)
    {
        Preferences.Clear();
    }

    private async void LoadSampleData_Clicked(object sender, EventArgs e)
    {
        //Preferences.Clear();

        if (Settings.FirstRun)
        {
            DatabaseService.LoadSampleData();
            Settings.FirstRun = false;

            await Navigation.PopToRootAsync();
        }
    }

    private async void ClearSampleData_Clicked(object sender, EventArgs e)
    {
        await DatabaseService.ClearSampleData();
    }
}