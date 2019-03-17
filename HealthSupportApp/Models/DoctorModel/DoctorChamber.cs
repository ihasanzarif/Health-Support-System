using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthSupportApp.Models.DoctorModel
{
    public class DoctorChamber
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Hospital/Diagnostic center name.")]
        [DisplayName("Name")]
        [StringLength(100, ErrorMessage = "Name should be 50 Character long.")]
        public string ChamberName { get; set; }
        [Required(ErrorMessage = "Hospital location must required.", AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessage = "Maximum length exceeded! Maximum length is 100 character long.")]
        [DisplayName("Address")]

        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter the area of your Hospital/Diagnostic center.", AllowEmptyStrings = false)]
        [StringLength(20, ErrorMessage = "Maximum length exceeded! Maximum length is 20 character long.")]
        [DisplayName("Area")]
        public string Area { get; set; }
        [Required(ErrorMessage = "Please enter the city of your Hospital/Diagnostic center.", AllowEmptyStrings = false)]
        [StringLength(20, ErrorMessage = "Maximum length exceeded! Maximum length is 20 character long.")]
        [DisplayName("City")]
        public string City { get; set; }
        [RegularExpression(@"\+?(88)?0?1[56789][0-9]{8}\b", ErrorMessage = "Enter valid mobile number.")]
        [DisplayName("Contact No.")]
        [StringLength(13, MinimumLength = 11, ErrorMessage = "Mobile number should be 11 digit.")]
        public string ContactNo { get; set; }
        [DisplayName("Phone No.")]
        [StringLength(11, MinimumLength = 9, ErrorMessage = "Phone number maximum 11 digit.")]
        public string PhoneNo { get; set; }
        [Required(ErrorMessage = "Please enter Doctor's fee.")]
        [RegularExpression(@"^([1-9]|[1-8][0-9]|9[0-9]|[1-8][0-9]{2}|9[0-8][0-9]|99[0-9]|[1-4][0-9]{3}|5000)$", ErrorMessage = "Seat capacity limit exceeded.")]
        public int Fee { get; set; }
        [Required(ErrorMessage = "Please enter Doctor's returning fee.")]
        [RegularExpression(@"^([1-9]|[1-8][0-9]|9[0-9]|[1-8][0-9]{2}|9[0-8][0-9]|99[0-9]|[1-4][0-9]{3}|5000)$", ErrorMessage = "Seat capacity limit exceeded.")]
        public int ReturningFee { get; set; }
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
        public ManageAppointment ManageAppointment { get; set; }
    }
}