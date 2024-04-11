using CommunityToolkit.Mvvm.Input;
using System.Text;
using Terms.Services;
using System.Security.Cryptography;
using Terms.Views;

namespace Terms;

public partial class LoginPage : ContentPage
{
	private string _userName;
	private string _password;


	public LoginPage()
	{
		InitializeComponent();
	}

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        // Authenticate user
        bool isAuthenticated = await AuthenticateUser(username, password);

        if (isAuthenticated)
        {
            // Navigate to the main page
            await Navigation.PushAsync(new Dashboard());
        }
        else
        {
            await DisplayAlert("Login Failed", "Invalid username or password.", "OK");
        }
    }
    private async Task<bool> AuthenticateUser(string username, string password)
    {
        // Call the DatabaseService to check the user credentials
        var users = await DatabaseService.GetUser();

        // Compute the hash of the provided password
        string hashedPassword = ComputeHash(password);

        // Check if the provided username and hashed password match any user in the database
        foreach (var user in users)
        {
            if (user.UserName == username && user.Password == hashedPassword)
            {
                // Authentication successful
                return true;
            }
        }

        // Authentication failed
        return false;
    }

    // Helper method to compute the hash of a password
    private string ComputeHash(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Compute hash
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convert byte array to a string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}