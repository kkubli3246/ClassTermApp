using Terms.Models;
using Terms.Services;

namespace Terms.Views;

public partial class CourseAdd : ContentPage
{
    private readonly int _selectedTermId;
	public CourseAdd()
	{
		InitializeComponent();
	}

    public CourseAdd(int termId)
    {
        InitializeComponent();
        _selectedTermId = termId;
    }

    public static bool checkDates(DateTime start, DateTime end)
    {
        if (end < start)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    async void SaveCourse_Clicked(object sender, EventArgs e)
    {
        bool valid = checkDates(startDatePicker.Date, endDatePicker.Date);
        bool valid1 = checkDates(paStartDatePicker.Date, paEndDatePicker.Date);
        bool valid2 = checkDates(oaStartDatePicker.Date, oaEndDatePicker.Date);

        if(string.IsNullOrWhiteSpace(CourseName.Text))
        {
            await DisplayAlert("Missing Course Name", "Please enter a name", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(instructorName.Text))
        {
            await DisplayAlert("Missing Instructor Name", "Please enter a name", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(instructorPhone.Text))
        {
            await DisplayAlert("Missing Instructor Phone", "Please enter a phone number", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(instructorEmail.Text))
        {
            await DisplayAlert("Missing Instructor Email", "Please enter an email.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(CourseStatusPicker.SelectedItem.ToString()))
        {
            await DisplayAlert("Missing status", "Please enter a status.", "OK");
            return;
        }

        if(!valid)
        {
            await DisplayAlert("End date can not be less than Start Date", "Please correct End Date.", "OK");
            return;
        }

        if (!valid1)
        {
            await DisplayAlert("End date can not be less than Start Date", "Please correct End Date.", "OK");
            return;
        }

        if (!valid2)
        {
            await DisplayAlert("End date can not be less than Start Date", "Please correct End Date.", "OK");
            return;
        }

        await DatabaseService.AddCourse(_selectedTermId,CourseName.Text,instructorName.Text, instructorPhone.Text, instructorEmail.Text, CourseStatusPicker.SelectedItem.ToString(),
            DateTime.Parse(startDatePicker.Date.ToString()), DateTime.Parse(endDatePicker.Date.ToString()), NotesEditor.Text,paName.Text, oaName.Text, DateTime.Parse(oaStartDatePicker.Date.ToString()),
            DateTime.Parse(oaEndDatePicker.Date.ToString()), DateTime.Parse(paStartDatePicker.Date.ToString()),DateTime.Parse(paEndDatePicker.Date.ToString()));

        await Navigation.PopAsync();

    }

    async void CancelCourse_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    async void Home_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}