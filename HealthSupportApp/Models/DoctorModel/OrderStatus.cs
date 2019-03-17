using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthSupportApp.Models.DoctorModel
{
    public class OrderStatus
    {
        public string ChamberName { get; set; }
        public int AppointAmount { get; set; }
        public int TotalPrice { get; set; }
        public string TrxId { get; set; }
        public string Status { get; set; }
    }
}