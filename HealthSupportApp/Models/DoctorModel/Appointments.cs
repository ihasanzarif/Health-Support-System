using HealthSupportApp.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthSupportApp.Models.DoctorModel
{
    public class Appointments
    {
        public int ChamberId { get; set; }
        public string ChamberName { get; set; }
        public List<BookAppointment> BookAppointments { get; set; }
    }
}