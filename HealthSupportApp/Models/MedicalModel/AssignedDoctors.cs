using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthSupportApp.Models.MedicalModel
{
    public class AssignedDoctors
    {
        public int Id { get; set; }
        public int MedicalId { get; set; }
        public int DoctorId { get; set; }
        [Required(ErrorMessage = "Please enter Doctor's fee.")]
        [RegularExpression(@"^([1-9]|[1-8][0-9]|9[0-9]|[1-8][0-9]{2}|9[0-8][0-9]|99[0-9]|[1-4][0-9]{3}|5000)$", ErrorMessage = "Seat capacity limit exceeded.")]
        public int DoctorFee { get; set; }
        [Required(ErrorMessage = "Please enter Doctor's returning fee.")]
        [RegularExpression(@"^([1-9]|[1-8][0-9]|9[0-9]|[1-8][0-9]{2}|9[0-8][0-9]|99[0-9]|[1-4][0-9]{3}|5000)$", ErrorMessage = "Seat capacity limit exceeded.")]
        public int DoctorReturningFee { get; set; }
        public bool Sat { get; set; }
        public bool Sun { get; set; }
        public bool Mon { get; set; }
        public bool Tue { get; set; }
        public bool Wed { get; set; }
        public bool Thu { get; set; }
        public bool Fri { get; set; }
        [Required(ErrorMessage = "*Please select time for SAT")]
        public string SatFromTime { get; set; }
        [Required(ErrorMessage = "*Please select time for SUN")]
        public string SunFromTime { get; set; }
        [Required(ErrorMessage = "*Please select time for MON")]
        public string MonFromTime { get; set; }
        [Required(ErrorMessage = "*Please select time for TUE")]
        public string TueFromTime { get; set; }
        [Required(ErrorMessage = "*Please select time for WED")]
        public string WedFromTime { get; set; }
        [Required(ErrorMessage = "*Please select time for THU")]
        public string ThuFromTime { get; set; }
        [Required(ErrorMessage = "*Please select time for FRI")]
        public string FriFromTime { get; set; }
        [Required(ErrorMessage = "*Please select time for SAT")]
        public string SatToTime { get; set; }
        [Required(ErrorMessage = "*Please select time for SUN")]
        public string SunToTime { get; set; }
        [Required(ErrorMessage = "*Please select time for MON")]
        public string MonToTime { get; set; }
        [Required(ErrorMessage = "*Please select time for TUE")]
        public string TueToTime { get; set; }
        [Required(ErrorMessage = "*Please select time for WED")]
        public string WedToTime { get; set; }
        [Required(ErrorMessage = "*Please select time for THU")]
        public string ThuToTime { get; set; }
        [Required(ErrorMessage = "*Please select time for FRI")]
        public string FriToTime { get; set; }
    }
}