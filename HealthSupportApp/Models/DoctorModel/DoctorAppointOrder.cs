using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthSupportApp.Models.DoctorModel
{
    public class DoctorAppointOrder
    {
        public int OrderId { get; set; }
        [Required(ErrorMessage ="Please select your chamber.")]
        public int ManageAppointId { get; set; }
        [Required(ErrorMessage = "Please enter how much you want to buy.")]
        public int AppointAmount { get; set; }
        public int TotalPrice { get; set; }
        [Required(ErrorMessage = "Please select your payment method.")]
        public string PaymentMethod { get; set; }
        [Required(ErrorMessage = "Please enter the TrxID.")]
        public string TrnxId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public int RemainingAppoint { get; set; }
        public int TotalAppoint { get; set; }
        public bool Expired { get; set; }
        public string Status { get; set; }

    }
}