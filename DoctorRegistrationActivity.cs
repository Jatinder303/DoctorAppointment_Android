using DoctorAppointment_Android.Models;

namespace DoctorAppointment_Android;

[Activity(Label = "DoctorRegistrationActivity")]
public class DoctorRegistrationActivity : Activity
{
    private EditText usernameEditText, passwordEditText, fullnameEditText;
    private Button registerButton, loginButton;
    AppointmentsRepository repository;
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.DoctorRegistrationLayout);

        usernameEditText = FindViewById<EditText>(Resource.Id.usernameEditText);
        passwordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);
        fullnameEditText = FindViewById<EditText>(Resource.Id.fullnameEditText);
        registerButton = FindViewById<Button>(Resource.Id.registerButton);
        loginButton = FindViewById<Button>(Resource.Id.LoginButton);
        repository = new AppointmentsRepository();
        loginButton.Click += LoginButton_Click;
        registerButton.Click += RegisterButton_Click;
    }

    private void RegisterButton_Click(object sender, EventArgs e)
    {
          string username = usernameEditText.Text;
        string password = passwordEditText.Text;
        string fullname = fullnameEditText.Text;
        // Validate input
        // Validate input
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(fullname))
        {
            // Display a message or handle the validation error appropriately
            Toast.MakeText(this, "Username, password and Full Name cannot be empty", ToastLength.Short).Show();
            return;
        }
        else
        {
            Doctor newDoctor = new Doctor { Doctor_Username = username, Password = password , Doctor_FullName = fullname};
            Toast.MakeText(this, username, ToastLength.Short).Show();
            repository.AddDoctor(newDoctor);

            // Navigate to the login activity
            StartActivity(typeof(DoctorLoginActivity));
        }
    }
    private void LoginButton_Click(object sender, EventArgs e)
    {
            // Navigate to the login activity
            StartActivity(typeof(DoctorLoginActivity));
    }
}