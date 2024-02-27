using DoctorAppointment_Android.Models;

namespace DoctorAppointment_Android;

[Activity(Label = "AppointmentListingActivity")]
public class AppointmentListingActivity : Activity
{
    private ListView obj_listview;
    AppointmentsRepository obj_databaseManager;
    List<Appointment> appointments_details;
    private string UserName;
    private Button LogoutButton;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Create your application here
        SetContentView(Resource.Layout.AppointmentListingLayout);
        obj_listview = FindViewById<ListView>(Resource.Id.appointmentListView);

        LogoutButton = FindViewById<Button>(Resource.Id.Logout_Button);


        obj_databaseManager = new AppointmentsRepository();
        UserName = Intent.GetStringExtra("User_Name");

        DisplayAppointments();

        LogoutButton.Click += LogoutButton_click;


    }

    private void LogoutButton_click(object? sender, EventArgs e)
    {
        StartActivity(typeof(MainActivity));
    }

    private void DisplayAppointments()
    {
        appointments_details = obj_databaseManager.GetAppointmentsByUser(UserName);

        ArrayAdapter<string> appointments_adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1);

        foreach (var item in appointments_details)
        {
            //if(item.FullName == obj_editText.Text)
            {
                appointments_adapter.Add($"{item.AppointmentId} - {item.AppointmentDateTime} - {item.Doctor_Username}");
            }

        }
        obj_listview.Adapter = appointments_adapter;
    }
}