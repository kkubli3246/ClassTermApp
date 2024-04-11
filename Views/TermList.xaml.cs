using Terms.Models;
using Terms.Services;

namespace Terms.Views;

public partial class TermList : ContentPage
{
	public TermList()
	{
		InitializeComponent();
    }

    private async void TermCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
		if(e.CurrentSelection != null)
		{
			Term term = (Term)e.CurrentSelection.FirstOrDefault();
			await Navigation.PushAsync(new TermEdit(term));
		}
    }

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		TermCollectionView.ItemsSource = await DatabaseService.GetTerms();
	}


}