using DoctorAppointment_Android.Models;
using static Android.Provider.ContactsContract.CommonDataKinds;

namespace DoctorAppointment_Android;

[Activity(Label = "UserProfileActivity")]
public class UserProfileActivity : Activity
{
    private EditText _userName, _password, _fullname;
    private Button _updateButton, _deleteButton;
    public string Patient_UserName;
    AppointmentsRepository _dbManager;
    User user, update_user;
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
        Patient_UserName = Intent.GetStringExtra("Patient_UserName");

        user =_dbManager.GetUserByUsername(Patient_UserName);
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
        _dbManager.DeleteAppointmentByUser(user.Patient_Username);
        _dbManager.DeleteUser(user.Patient_Username);
        StartActivity(typeof(MainActivity));
    }

    private void _updateButton_Click(object? sender, EventArgs e)
    {
        update_user = new User()
        {
            
            Patient_Username = _userName.Text,
            Password =_password.Text,
            Patient_FullName =_fullname.Text
        };

        _dbManager.UpdateUser(update_user);
        Toast.MakeText(this, "Person Data is updated successfully", ToastLength.Long).Show();

        Finish();
    }
}