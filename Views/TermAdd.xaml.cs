using Terms.Services;

namespace Terms.Views;

public partial class TermAdd : ContentPage
{
	public TermAdd()
	{
		InitializeComponent();
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


    private async void SaveTerm_Clicked(object sender, EventArgs e)
    {
        bool valid = checkDates(TermStartDatePicker.Date, TermEndDatePicker.Date);

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


        await DatabaseService.AddTerm(TermName.Text, TermStartDatePicker.Date, TermEndDatePicker.Date);
        await Navigation.PopAsync();
    }

    private async void CancelTerm_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}