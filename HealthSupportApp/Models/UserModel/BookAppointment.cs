using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthSupportApp.Models.UserModel
{
    public class BookAppointment
    {
        public int BookAppointId { get; set; }
        public int DoctorId { get; set; }
        public int? ChamberId { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "Doctor name required.")]
        public string DoctorName { get; set; }
        [Required(ErrorMessage = "Chamber name required.")]
        public string ChamberName { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string Schedule { get; set; }
        public string AppointDate { get; set; }
        public int AppointPrice { get; set; }
        [Required(ErrorMessage = "Please select payment method.")]
        public string PaymentMethod { get; set; }
        [Required(ErrorMessage = "Please enter the TrxID.")]
        public string TrxId { get; set; }
        public int SerialNo { get; set; }
        public string Status { get; set; }
        public bool Expired { get; set; }
    }
}