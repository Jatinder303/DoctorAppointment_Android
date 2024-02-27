using DoctorAppointment_Android.Models;

namespace DoctorAppointment_Android;

[Activity(Label = "DoctorAppointmentListingActivity")]
public class DoctorAppointmentListingActivity : Activity
{
    private ListView obj_listview;
    AppointmentsRepository obj_databaseManager;
    List<Appointment> appointments_details;
    private string DoctorName;
    private Button LogoutButton;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Create your application here
        SetContentView(Resource.Layout.DoctorAppointmentListingLayout);
        obj_listview = FindViewById<ListView>(Resource.Id.appointmentListView);

        LogoutButton = FindViewById<Button>(Resource.Id.Logout_Button);


        obj_databaseManager = new AppointmentsRepository();
        DoctorName = Intent.GetStringExtra("Doctor_userName");
        DisplayAppointments();

        LogoutButton.Click += LogoutButton_click;


    }

    private void LogoutButton_click(object? sender, EventArgs e)
    {
        StartActivity(typeof(MainActivity));
    }

    private void DisplayAppointments()
    {
        appointments_details = obj_databaseManager.GetAppointmentsByDoctor(DoctorName);

        ArrayAdapter<string> appointments_adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1);

        foreach (var item in appointments_details)
        {
            //if(item.FullName == obj_editText.Text)
            {
                appointments_adapter.Add($"{item.AppointmentId} - {item.AppointmentDateTime} - {item.Patient_Username}");
            }

        }
        obj_listview.Adapter = appointments_adapter;
    }
}