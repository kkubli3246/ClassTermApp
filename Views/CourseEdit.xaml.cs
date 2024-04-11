using Terms.Models;
using Terms.Services;

namespace Terms.Views;

public partial class CourseEdit : ContentPage
{
	public CourseEdit(Course selectedCourse)
	{
		InitializeComponent();

        //Populate controls next
        CourseId.Text = selectedCourse.courseId.ToString();
        CourseName.Text = selectedCourse.CourseName;
        instructorName.Text = selectedCourse.instructorName;
        instructorPhone.Text = selectedCourse.instructorPhone;
        instructorEmail.Text = selectedCourse.instructorEmail;
        courseStatusPicker.SelectedItem = selectedCourse.Status;
        startDatePicker.Date = selectedCourse.start;
        endDatePicker.Date = selectedCourse.end;
        paName.Text = selectedCourse.paName;
        oaName.Text = selectedCourse.oaName;
        paStartDatePicker.Date = selectedCourse.paStart;
        paEndDatePicker.Date = selectedCourse.paEnd;
        oaStartDatePicker.Date = selectedCourse.oaStart;
        oaEndDatePicker.Date = selectedCourse.oaEnd;
        NotesEditor.Text = selectedCourse.noteDetails;
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

        if (string.IsNullOrWhiteSpace(CourseName.Text))
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

        if (string.IsNullOrWhiteSpace(courseStatusPicker.SelectedItem.ToString()))
        {
            await DisplayAlert("Missing status", "Please enter a status.", "OK");
            return;
        }

        if (!valid)
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

        await DatabaseService.UpdateCourse(Int32.Parse(CourseId.Text), CourseName.Text, instructorName.Text, instructorPhone.Text, instructorEmail.Text, courseStatusPicker.SelectedItem.ToString(),
            DateTime.Parse(startDatePicker.Date.ToString()), DateTime.Parse(endDatePicker.Date.ToString()), NotesEditor.Text,paName.Text, oaName.Text, DateTime.Parse(oaStartDatePicker.Date.ToString()),
            DateTime.Parse(oaEndDatePicker.Date.ToString()), DateTime.Parse(paStartDatePicker.Date.ToString()), DateTime.Parse(paEndDatePicker.Date.ToString()));
        await Navigation.PopAsync();
    }

    async void CancelCourse_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    async void DeleteCourse_Clicked(object sender, EventArgs e)
    {
        var answer = await DisplayAlert("Delete this Course?", "Delete this Course?", "Yes", "No");

        if (answer == true)
        {
            var id = int.Parse(CourseId.Text);

            await DatabaseService.RemoveCourse(id);

            await DisplayAlert("Course Deleted", "Course Deleted", "OK");
        }
        else
        {
            await DisplayAlert("Delete Canceled", "Nothing Deleted", "OK");
        }

        await Navigation.PopAsync();
    }

    async void ShareButton_Clicked(object sender, EventArgs e)
    {
        var text = CourseName.Text;
        await Share.RequestAsync(new ShareTextRequest
        {
            Text = text,
            Title = "Share Text"
        });
    }

    async void ShareUri_Clicked(object sender, EventArgs e)
    {
        string uri = "https://docs.microsoft.com/en-us/xamarin/essentials/share?tabs=android";
        await Share.RequestAsync(new ShareTextRequest
        {
            Uri = uri,
            Title = "Share Web Link"
        });
    }
}