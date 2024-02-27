using Android.Content;
using DoctorAppointment_Android.Models;

namespace DoctorAppointment_Android;

[Activity(Label = "AppointmentBookingActivity")]
public class AppointmentBookingActivity : Activity
{
    private Button bookButton;
    private DatePicker datePicker;
    private TimePicker timePicker;
    private Button book_Button;
    AppointmentsRepository repository;
    private string paitent_username , doctor_username;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Create your application here
        SetContentView(Resource.Layout.AppointmentBookingLayout);
        book_Button = FindViewById<Button>(Resource.Id.bookButton);
        datePicker = FindViewById<DatePicker>(Resource.Id.datePicker);
        timePicker = FindViewById<TimePicker>(Resource.Id.timePicker);
        paitent_username = Intent.GetStringExtra("Patient_UserName");
        doctor_username = Intent.GetStringExtra("Doctor_UserName");
        book_Button.Click += Book_Button_Click;
    }
    private void Book_Button_Click(object sender, EventArgs e)
    {
       

        // Get the selected date and time
        int year = datePicker.Year;
        int month = datePicker.Month + 1; // Month is zero-based
        int day = datePicker.DayOfMonth;
        int hour = timePicker.Hour;
        int minute = timePicker.Minute;

        DateTime selectedDateTime = new DateTime(year, month, day, hour, minute, 0);

        // Create an Appointment object
        Appointment newAppointment = new Appointment
        {
            AppointmentDateTime = selectedDateTime,
            // Set the UserId and DoctorId based on the logged-in user and doctor
            // These values would depend on your authentication and session management
            Patient_Username = paitent_username, 
            Doctor_Username = doctor_username
        };

        repository.AddAppointment(newAppointment);

        Intent AppointmentList_intent = new Intent(this, typeof(AppointmentListingActivity));
        // Pass the username as an extra to the intent
        AppointmentList_intent.PutExtra("User_Name", paitent_username);
        StartActivity(AppointmentList_intent);


    }
}