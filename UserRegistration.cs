using DoctorAppointment_Android.Models;

namespace DoctorAppointment_Android;

[Activity(Label = "UserRegistration")]
public class UserRegistration : Activity
{

    private Button registerButton, loginButton;
    private EditText usernameEditText, passwordEditText, fullnameEditText;
    AppointmentsRepository repository;
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.UserRegistrationLayout);
        // Create your application here
        usernameEditText = FindViewById<EditText>(Resource.Id.usernameEditText);
        passwordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);
        fullnameEditText = FindViewById<EditText>(Resource.Id.fullnameEditText);
        registerButton = FindViewById<Button>(Resource.Id.registerButton);
        loginButton = FindViewById<Button>(Resource.Id.LoginButton);

        loginButton.Click += LoginButton_Click;
        registerButton.Click += RegisterButton_Click;
    }

    private void RegisterButton_Click(object sender, EventArgs e)
    {
        string username = usernameEditText.Text;
        string password = passwordEditText.Text;
        string fullname = fullnameEditText.Text;
        // Validate input
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(fullname))
        {
            // Display a message or handle the validation error appropriately
            Toast.MakeText(this, "Username, password and Full Name cannot be empty", ToastLength.Short).Show();
            return;
        }
        else
        {
            User newUser = new User { Patient_Username = username, Password = password, Patient_FullName = fullname };

            // Save the user to the database using the repository
            repository.AddUser(newUser);

            // Navigate to the login activity
            StartActivity(typeof(UserLoginActivity));
        }
    }
    private void LoginButton_Click(object sender, EventArgs e)
    {
        // Navigate to the login activity
        StartActivity(typeof(UserLoginActivity));
    }
}
