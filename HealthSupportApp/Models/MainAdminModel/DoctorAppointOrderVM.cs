using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthSupportApp.Models.MainAdminModel
{
    public class DoctorAppointOrderVM
    {
        public int OrderId { get; set; }
        public int ManageAppointId { get; set; }
        public int DoctorId { get; set; }
        public int MedicalId { get; set; }
        public int DoctorChamberId { get; set; }
        public string DoctorName { get; set; }
        public string ChamberName { get; set; }
        public int AppointAmount { get; set; }
        public int TotalPrice { get; set; }
        public string PaymentMethod { get; set; }
        public string TrnxId { get; set; }
        public string OrderDate { get; set; }
        public string ExpireDate { get; set; }
        public int RemainingAppoint { get; set; }
        public bool Expired { get; set; }
        public string Status { get; set; }
    }
}