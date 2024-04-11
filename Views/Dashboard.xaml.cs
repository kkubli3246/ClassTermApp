using Terms.Models;
using Terms.Services;
using System.Threading.Tasks;
using Plugin.LocalNotification;

namespace Terms.Views;

public partial class Dashboard : ContentPage
{
    public Dashboard()
    {
        InitializeComponent();

        GetNotificationList();
    }

    async void AddTerm_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TermAdd());
    }

    private async void ViewTerm_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TermList());
    }

    //private async void Settings_Clicked(object sender, EventArgs e)
    //{
    //    await Navigation.PushAsync(new AppSettings());
    //}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var courseList = await DatabaseService.GetCourses();
        var notifyRandom = new Random();
        var notifyId = notifyRandom.Next(1000);

        foreach (Course courseRecord in courseList)
        {
            if (courseRecord.startNotification == true)
            {
                if (courseRecord.start == DateTime.Today)
                {
                    await DisplayAlert("Notice", $"Course: {courseRecord.CourseName} begins today!", "OK");
                    courseRecord.startNotification = false;
                }
            }
            else if (courseRecord.end.Date == DateTime.Today)
            {
                    await DisplayAlert("Notice", $"Course: {courseRecord.CourseName} ends today!", "OK");
            }
            else if (courseRecord.paStart.Date == DateTime.Today)
            {
                await DisplayAlert("Notice", $"PA: {courseRecord.paName} begins today!", "OK");
            }
            else if (courseRecord.paEnd.Date == DateTime.Today)
            {
                await DisplayAlert("Notice", $"PA: {courseRecord.paName} ends today!", "OK");
            }
            else if (courseRecord.oaStart.Date == DateTime.Today)
            {
                await DisplayAlert("Notice", $"OA: {courseRecord.oaName} begins today!", "OK");
            }
            else if (courseRecord.oaEnd.Date == DateTime.Today)
            {
                await DisplayAlert("Notice", $"OA: {courseRecord.oaName} ends today!", "OK");
            }
        }

        if (Services.Settings.FirstRun == true)
        {
            DatabaseService.LoadSampleData();
        }
    }

    private async Task GetNotificationList()
    {
        var notificationList = await DatabaseService.GetNotificationTerms();

        string message = "Notification for";
    }
}