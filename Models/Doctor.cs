using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment_Android.Models
{
    public class Doctor
    {
        [PrimaryKey]
        public string Doctor_Username { get; set; }
        public string Password { get; set; }
        public string Doctor_FullName { get; set; }
    }
}
