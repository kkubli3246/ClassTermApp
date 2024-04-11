using Terms.Services;
using Terms.Views;

namespace Terms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //Preferences.Clear();

            if (Settings.FirstRun)
            {
                DatabaseService.LoadSampleData();

                Settings.FirstRun = false;
            }

            var dashboard = new Dashboard();
            var navPage = new NavigationPage(dashboard);
            var loginPage = new LoginPage();

            MainPage = loginPage;
        }

    }
}
