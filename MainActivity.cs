using DoctorAppointment_Android.Models;

namespace DoctorAppointment_Android
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private AppointmentsRepository _appointmentsRepository;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            Button patientButton = FindViewById<Button>(Resource.Id.patientButton);
            Button doctorButton = FindViewById<Button>(Resource.Id.doctorButton);

            _appointmentsRepository = new AppointmentsRepository();

            patientButton.Click += async (sender, e) =>
            {
                // Handle patient button click, navigate to RegistrationChoiceActivity
                StartActivity(typeof(UserRegistration));
                bool internetConnection_check = await _appointmentsRepository.IsInternetConnectedAsync();

                if(internetConnection_check)
                {
                    //  Try to upload the database to Azure Storage when patient button is clicked
                    bool uploadInitiated = await _appointmentsRepository.TryUploadDatabaseToAzureStorageAsync();

                    if (uploadInitiated)
                    {
                        // Database upload initiated successfully
                        Toast.MakeText(this, "Database upload initiated successfully", ToastLength.Short).Show();
                    }
                    else
                    {
                        // Handle database upload failure
                        Toast.MakeText(this, "Database upload failed", ToastLength.Short).Show();
                    }
                }
                else
                {
                    // No internet connection; handle accordingly
                    Toast.MakeText(this, "No internet connection", ToastLength.Short).Show();
                }

            };

            doctorButton.Click += async (sender, e) =>
            {
                // Handle doctor button click, navigate to RegistrationChoiceActivity
                StartActivity(typeof(DoctorRegistrationActivity));

                bool internetConnection_check = await AppointmentsRepository.IsInternetConnectedAsync();

                if (internetConnection_check)
                {
                    //Try to upload the database to Azure Storage when doctor button is clicked
                    bool uploadInitiated = await _appointmentsRepository.TryUploadDatabaseToAzureStorageAsync();

                    if (uploadInitiated)
                    {
                        // Database upload initiated successfully
                        Toast.MakeText(this, "Database upload initiated successfully", ToastLength.Short).Show();
                    }
                    else
                    {
                        // Handle database upload failure
                        Toast.MakeText(this, "Database upload failed", ToastLength.Short).Show();
                    }
                }
                else
                {
                    // No internet connection; handle accordingly
                    Toast.MakeText(this, "No internet connection", ToastLength.Short).Show();
                }
            };

        }
    }
}