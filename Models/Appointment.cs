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

        public string Doctor_UserName { get; set; }
        public string DoctorName { get; set; }

        public string PatientUserName { get; set; }
        public string PatientName { get; set; }
    }
}
