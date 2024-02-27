using DoctorAppointment_Android.Models;

namespace DoctorAppointment_Android
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            Button patientButton = FindViewById<Button>(Resource.Id.patientButton);
            Button doctorButton = FindViewById<Button>(Resource.Id.doctorButton);

            patientButton.Click += (sender, e) =>
            {
                // Handle patient button click, navigate to RegistrationChoiceActivity
                StartActivity(typeof(UserRegistration));
            };

            doctorButton.Click += (sender, e) =>
            {
                // Handle doctor button click, navigate to RegistrationChoiceActivity
                StartActivity(typeof(DoctorRegistrationActivity));
            };

        }
    }
}