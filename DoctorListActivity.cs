using Android.Content;
using DoctorAppointment_Android.Models;

namespace DoctorAppointment_Android;

[Activity(Label = "DoctorListActivity")]
public class DoctorListActivity : Activity
{
    ListView obj_listview;
    AppointmentsRepository obj_databaseManager;
    Button obj_editProfileButton;
    List<Doctor> Doctordetails;
    private string Patient_UserName, Patient_FullName;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Create your application here

        SetContentView(Resource.Layout.DoctorListLayout);
        obj_listview = FindViewById<ListView>(Resource.Id.AllUser_ListView);
        obj_editProfileButton = FindViewById<Button>(Resource.Id.EditProfileButton);

        obj_databaseManager = new AppointmentsRepository();
        DoctorListDisplay();
        Patient_UserName = Intent.GetStringExtra("Patient_UserName");
        Patient_FullName = Intent.GetStringExtra("Patient_FullName");
        obj_editProfileButton.Click += obj_editProfileButton_Click;
        obj_listview.ItemClick += obj_listview_item_Selected;
    }

    private void obj_editProfileButton_Click(object? sender, EventArgs e)
    {
        Intent profile_intent = new Intent(this, typeof(UserProfileActivity));

        // Pass the username as an extra to the intent
        profile_intent.PutExtra("Patient_UserName", Patient_UserName);

        // Start the ProfileActivity
        StartActivity(profile_intent);
    }

    protected override void OnResume()
    {
        base.OnResume();

        DoctorListDisplay();
    }

    private void DoctorListDisplay()
    {
        Doctordetails = obj_databaseManager.GetDoctors();

        ArrayAdapter<string> Doctordetails_adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1);

        foreach (var item in Doctordetails)
        {
            //if(item.FullName == obj_editText.Text)
            {
                Doctordetails_adapter.Add($"Doctor Name : {item.Doctor_FullName}");
            }

        }
        obj_listview.Adapter = Doctordetails_adapter;
    }

    private void obj_listview_item_Selected(object sender, AdapterView.ItemClickEventArgs e)
    {
        Doctor selectedDoctor = Doctordetails[e.Position];

        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.SetTitle("Options");
        builder.SetMessage("Please select one option");
        builder.SetPositiveButton("Book an Appointment", (s, a) => BookAppointment(selectedDoctor));
        builder.SetNeutralButton("Cancel", (s, a) => { ((Dialog)s).Dismiss(); });
        builder.Show();
    }

    private void BookAppointment(Doctor doctor)
    {
        Intent BookAppointmentIntent = new Intent(this, typeof(AppointmentBookingActivity));
        BookAppointmentIntent.PutExtra("Patient_FullName", Patient_FullName);
        BookAppointmentIntent.PutExtra("Patient_UserName", Patient_UserName);
        BookAppointmentIntent.PutExtra("Doctor_FullName", doctor.Doctor_FullName);
        BookAppointmentIntent.PutExtra("Doctor_UserName", doctor.Doctor_Username);
        StartActivity(BookAppointmentIntent);
    }
}