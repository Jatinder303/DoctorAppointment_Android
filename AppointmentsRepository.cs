using DoctorAppointment_Android.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment_Android
{
    public class AppointmentsRepository
    {
        readonly SQLiteConnection _dbContext;

        public AppointmentsRepository()
        {
            string directoryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
            if (Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string dbPath = Path.Combine(directoryPath, "DoctorAppointmentsDb.db");
            _dbContext = new SQLiteConnection(dbPath);
            _dbContext.CreateTable<User>();
            _dbContext.CreateTable<Doctor>();
            _dbContext.CreateTable<Appointment>(); 
        }

        public void AddUser(User user)
        {
            _dbContext.Insert(user);
        }

        public User GetUserByUsername(string username)
        {
            return _dbContext.Table<User>().FirstOrDefault(u => u.Patient_Username == username);
        }

        public List<User> GetUsers()
        {
            return _dbContext.Table<User>().ToList();
        }
        public void UpdateUser(User Updateuser)
        {
            _dbContext.Update(Updateuser);
        }

      
        public void DeleteUser(string username)
        {
            _dbContext.Delete<User>(username);
        }

        public void AddDoctor(Doctor doctor)
        {
            _dbContext.Insert(doctor);
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

        public List<Appointment> GetAppointmentsByUser(string username)
        {
            return _dbContext.Table<Appointment>().Where(a => a.Patient_Username == username).ToList();
        }

        public List<Appointment> GetAppointmentsByDoctor(string doctor_username)
        {
            return _dbContext.Table<Appointment>().Where(a => a.Doctor_Username == doctor_username).ToList();
        }

        public void UpdateAppointment(Appointment appointment)
        {
            _dbContext.Update(appointment);
        }

        public void DeleteAppointment(Appointment appointment)
        {
            _dbContext.Delete(appointment);
        }

        public void DeleteAppointmentByUser(string username)
        {
            _dbContext.Delete<Appointment>(username);
        }
    }
}
