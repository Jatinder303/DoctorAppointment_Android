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
    private string Patient_FullName, Doctor_FullName, Patient_UserName, Doctor_UserName;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Create your application here
        SetContentView(Resource.Layout.AppointmentBookingLayout);
        book_Button = FindViewById<Button>(Resource.Id.bookButton);
        datePicker = FindViewById<DatePicker>(Resource.Id.datePicker);
        timePicker = FindViewById<TimePicker>(Resource.Id.timePicker);
        repository = new AppointmentsRepository();
        Doctor_FullName = Intent.GetStringExtra("Doctor_FullName");
        Patient_FullName = Intent.GetStringExtra("Patient_FullName");
        Patient_UserName = Intent.GetStringExtra("Patient_UserName");
        Doctor_UserName = Intent.GetStringExtra("Doctor_UserName");
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
            PatientUserName = Patient_UserName,
            PatientName = Patient_FullName, 
            Doctor_UserName = Doctor_UserName,
            DoctorName = Doctor_FullName
            
        };

        repository.AddAppointment(newAppointment);

        Intent AppointmentList_intent = new Intent(this, typeof(AppointmentListingActivity));
        // Pass the username as an extra to the intent
        AppointmentList_intent.PutExtra("Patient_UserName", Patient_UserName);
        StartActivity(AppointmentList_intent);


    }
}