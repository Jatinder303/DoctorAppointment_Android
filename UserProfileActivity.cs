using DoctorAppointment_Android.Models;
using static Android.Provider.ContactsContract.CommonDataKinds;

namespace DoctorAppointment_Android;

[Activity(Label = "UserProfileActivity")]
public class UserProfileActivity : Activity
{
    private EditText _userName, _password, _fullname;
    private Button _updateButton, _deleteButton;
    private string _username;
    AppointmentsRepository _dbManager;
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Create your application here
        SetContentView(Resource.Layout.UserProfileLayout);

        _userName = FindViewById<EditText>(Resource.Id.UserName_EditText);
        _password = FindViewById<EditText>(Resource.Id.Password_EditText);
        _fullname = FindViewById<EditText>(Resource.Id.FullName_EditText);
      
        _updateButton = FindViewById<Button>(Resource.Id.Update_button);
        _deleteButton = FindViewById<Button>(Resource.Id.Delete_button);


        _dbManager = new AppointmentsRepository();
        _username = Intent.GetStringExtra("UserName");

        User user =_dbManager.GetUserByUsername(_username);
        if (user != null)
        {
            _userName.Text = user.Patient_Username;
            _password.Text = user.Password;
            _fullname.Text = user.Patient_FullName;
        }
        else
        {
            Toast.MakeText(this, "Person Data Not Found", ToastLength.Long).Show();
        }

        _updateButton.Click += _updateButton_Click;
        _deleteButton.Click += _deleteButton_Click;

    }

    private void _deleteButton_Click(object? sender, EventArgs e)
    {
        _dbManager.DeleteAppointmentByUser(_username);
        _dbManager.DeleteUser(_username);
        StartActivity(typeof(MainActivity));
    }

    private void _updateButton_Click(object? sender, EventArgs e)
    {
        User Update_user = new User()
        {
            Patient_Username = _userName.Text,
            Password =_password.Text,
            Patient_FullName =_fullname.Text
        };

        _dbManager.UpdateUser(Update_user);
        Toast.MakeText(this, "Person Data is updated successfully", ToastLength.Long).Show();

        Finish();
    }
}