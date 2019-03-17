using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthSupportApp.Models.DoctorModel
{
    public class ChambersForOrder
    {
        public int ManageId { get; set; }
        public int DoctorId { get; set; }
        public string ChamberName { get; set; }
    }
}