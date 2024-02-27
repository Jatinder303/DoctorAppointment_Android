using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment_Android.Models
{
    public class User
    {
        [PrimaryKey]
        public string Patient_Username { get; set; }
        public string Password { get; set; }
        public string Patient_FullName { get; set; }

 
    }
}
