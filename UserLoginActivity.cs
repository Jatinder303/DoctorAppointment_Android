using Android.Content;
using DoctorAppointment_Android.Models;

namespace DoctorAppointment_Android;

[Activity(Label = "LoginActivity")]
public class UserLoginActivity : Activity
{
    private Button loginButton;
    private EditText usernameEditText, passwordEditText;
    AppointmentsRepository repository;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        SetContentView(Resource.Layout.UserLoginLayout);

        loginButton = FindViewById<Button>(Resource.Id.loginButton);
        usernameEditText = FindViewById<EditText>(Resource.Id.usernameEditText);
        passwordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);

        loginButton.Click += LoginButton_Click;
    }

    private void LoginButton_Click(object sender, EventArgs e)
    {
        string username = usernameEditText.Text;
        string password = passwordEditText.Text;

      // Validate input
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            // Display an error message for empty username or password
            Toast.MakeText(this, "Please enter both username and password", ToastLength.Short).Show();
            return;
        }
        else
        {
            User user = repository.GetUserByUsername(username);

            if (user != null && user.Password == password)
            {
                Intent Login_intent = new Intent(this, typeof(DoctorListActivity));

                // Pass the username as an extra to the intent
                Login_intent.PutExtra("UserName", user.Patient_Username);

                // Start the ProfileActivity
                StartActivity(Login_intent);
 
            }
            else
            {
                // Display an error message or handle unsuccessful login
                Toast.MakeText(this, "Invalid username or password", ToastLength.Short).Show();
            }
        }
    }
}