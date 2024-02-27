using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment_Android.Models
{
    public class Appointment
    {
        [PrimaryKey, AutoIncrement]
        public int AppointmentId { get; set; }
        public DateTime AppointmentDateTime { get; set; }

        public string Patient_Username { get; set; }
        public string Doctor_Username { get; set; }

        [ForeignKey("Patient_Username")]
        public User User { get; set; }

        [ForeignKey("Doctor_Username")]
        public Doctor Doctor { get; set; }
    }
}
