using DoctorAppointment_Android.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DoctorAppointment_Android
{
    public class AppointmentsRepository
    {
        readonly SQLiteConnection _dbContext;
        readonly AzureStorageManager _azureStorageManager;

        public AppointmentsRepository()
        {
            string directoryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string dbPath = Path.Combine(directoryPath, "Doctor_AppointmentsDb.db");
            _dbContext = new AppointmentsDbContext(dbPath);
           _azureStorageManager = new AzureStorageManager("your-storage-connection-string");
        }

        public async Task<bool> IsInternetConnectedAsync()
        {
            var current = Connectivity.NetworkAccess;
                    
            return current == NetworkAccess.Internet;
        }

        public async Task<bool> TryUploadDatabaseToAzureStorageAsync()
        {
            if (await IsInternetConnectedAsync())
            {
                string localDatabaseFilePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile), "Doctor_AppointmentsDb.db");
                string destinationBlobName = "Doctor_AppointmentsDb.db";

                _azureStorageManager.UploadDatabaseToAzureStorage(localDatabaseFilePath, destinationBlobName);
                return true; // Successfully initiated upload
            }
            else
            {
                return false; // No internet connection
            }
        }


        public void AddUser(User user)
        {
            _dbContext.Insert(user);
        }

        public void AddDoctor(Doctor doctor)
        {
            _dbContext.Insert(doctor);
        }

        public User GetUserByUsername(string username)
        {
            return _dbContext.Table<User>().FirstOrDefault(d => d.Patient_Username == username);
        }

        public List<User> GetUsers()
        {
            return _dbContext.Table<User>().ToList();
        }
        public void UpdateUser(User Updateuser)
        {
            _dbContext.Update(Updateuser);
        }

      
        public void DeleteUser(string user_name)
        {
            _dbContext.Delete<User>(user_name);
        }



        public List<Doctor> GetDoctors()
        {
            return _dbContext.Table<Doctor>().ToList();
        }

        public Doctor GetDoctorByUsername(string username)
        {
            return _dbContext.Table<Doctor>().FirstOrDefault(d => d.Doctor_Username == username);
        }



        public void AddAppointment(Appointment appointment)
        {
            _dbContext.Insert(appointment);
        }

        public List<Appointment> GetAppointmentsByUser(string Patient_username)
        {
            return _dbContext.Table<Appointment>().Where(a => a.PatientUserName == Patient_username).ToList();
        }

        public List<Appointment> GetAppointmentsByDoctor(string Doctor_Username)
        {
            return _dbContext.Table<Appointment>().Where(a => a.Doctor_UserName == Doctor_Username).ToList();
        }

        public void UpdateAppointment(Appointment appointment)
        {
            _dbContext.Update(appointment);
        }

        public void DeleteAppointment(Appointment appointment)
        {
            _dbContext.Delete(appointment);
        }

        public void DeleteAppointmentByUser(string Patient_username)
        {
            _dbContext.Delete<Appointment>(Patient_username);
        }
    }
}
