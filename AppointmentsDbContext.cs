using DoctorAppointment_Android.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment_Android
{
    public class AppointmentsDbContext : SQLiteConnection
    {
        public AppointmentsDbContext(string path) : base(path)
        {
            CreateTable<User>();
            CreateTable<Doctor>();
            CreateTable<Appointment>();
        }
    }
}
