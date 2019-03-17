using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthSupportApp.Models.DoctorModel
{
    public class ManageAppointment
    {
        public int Id { get; set; }
        public int SatCapacity { get; set; }
        public int SunCapacity { get; set; }
        public int MonCapacity { get; set; }
        public int TueCapacity { get; set; }
        public int WedCapacity { get; set; }
        public int ThuCapacity { get; set; }
        public int FriCapacity { get; set; }
        public int SatAvailableAppoint { get; set; }
        public int SunAvailableAppoint { get; set; }
        public int MonAvailableAppoint { get; set; }
        public int TueAvailableAppoint { get; set; }
        public int WedAvailableAppoint { get; set; }
        public int ThuAvailableAppoint { get; set; }
        public int FriAvailableAppoint { get; set; }
        public bool OnlineAppointment { get; set; }
        public int TotalAvailableAppoint { get; set; }
        public int UsedAppoint { get; set; }
        public int OrderedAppoint { get; set; }
        public int RemainingAppoint { get; set; }
        public int TotalAppoint { get; set; }
        public string Status { get; set; }
        public int DoctorId { get; set; }
        public int? MedicalId { get; set; }
        public int? DoctorChamberId { get; set; }
        public DateTime OrderExpireDate { get; set; }
        
        public int UserId { get; set; }
    }
}