using Terms.Models;
using Terms.Services;

namespace Terms.Views;

public partial class TermEdit : ContentPage
{
    private readonly int _selectedTermId; //Used in OnAppearing method below

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        //TODO Return count from a table async
        //Note that the await unwraps a Task<T> to a T value (T may be a string, int, etc. In this case an int)

        int countCourse = await DatabaseService.GetCourseCountAsync(_selectedTermId);

        CountLabel.Text = countCourse.ToString();

        CourseCollectionView.ItemsSource = await DatabaseService.GetCourses(_selectedTermId); //Retrieve Courses for a specific Term based on the TermID
    }
	public TermEdit(Term term)
	{
		InitializeComponent();

        _selectedTermId = term.termId;

        TermId.Text = term.termId.ToString();
        TermName.Text = term.termName;
        termStartDatePicker.Date = term.start;
        termEndDatePicker.Date = term.end;

	}

    public static bool checkDates(DateTime start, DateTime end)
    {
        if(end < start)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    async void SaveTerm_Clicked(object sender, EventArgs e)
    {

        bool valid = checkDates(termStartDatePicker.Date, termEndDatePicker.Date);

        if(string.IsNullOrWhiteSpace(TermName.Text))
        {
            await DisplayAlert("Missing Name", "Please enter a name.", "OK");
            return;
        }

        if(!valid)
        {
            await DisplayAlert("End date can not be less than Start Date", "Please correct End Date.", "OK");
            return;
        }

        await DatabaseService.UpdateTerm(Int32.Parse(TermId.Text), TermName.Text, DateTime.Parse(termStartDatePicker.Date.ToString()), DateTime.Parse(termEndDatePicker.Date.ToString()));

        await Navigation.PopAsync();
    }

    async void CancelTerm_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    async void DeleteTerm_Clicked(object sender, EventArgs e)
    {
        var answer = await DisplayAlert("Delete Term and Related Courses?", "Delete this Term?", "Yes", "No");

        if (answer == true)
        {
            var id = int.Parse(TermId.Text);

            await DatabaseService.RemoveTerm(id);

            await DisplayAlert("Term Deleted", "Term Deleted", "OK");
        }
        else
        {
            await DisplayAlert("Delete Canceled", "Nothing Deleted", "OK");
        }

        await Navigation.PopAsync();
    }

    async void AddCourse_Clicked(object sender, EventArgs e)
    {
        var termId = Int32.Parse(TermId.Text);

        await Navigation.PushAsync(new CourseAdd(termId));
    }

    async void CourseCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var course = (Course)e.CurrentSelection.FirstOrDefault();
        if(e.CurrentSelection != null)
        {
            await Navigation.PushAsync((new CourseEdit(course)));
        }
    }
}