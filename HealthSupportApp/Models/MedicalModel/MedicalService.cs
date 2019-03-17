using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthSupportApp.Models.MedicalModel
{
    public class MedicalService
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please select Yes or No")]
        public string TwentyFourService { get; set; }
        [Required(ErrorMessage = "Please select Yes or No")]
        public string EmergencyService { get; set; }
        [RegularExpression(@"\+?(88)?0?1[56789][0-9]{8}\b", ErrorMessage = "Enter valid mobile number.")]
        [StringLength(13, MinimumLength = 11, ErrorMessage = "Mobile number should be 11 digit.")]
        public string EmergencyPhoneNo { get; set; }
        [StringLength(11, MinimumLength = 9, ErrorMessage = "Enter a valid phone number.")]
        public string EmergencyPhoneNo2 { get; set; }
        [Required(ErrorMessage = "Please select Yes or No")]
        public string AmbulanceService { get; set; }
        [RegularExpression(@"\+?(88)?0?1[56789][0-9]{8}\b", ErrorMessage = "Enter valid mobile number.")]
        [StringLength(13, MinimumLength = 11, ErrorMessage = "Mobile number should be 11 digit.")]
        public string AmbulancePhoneNo { get; set; }
        [StringLength(11, MinimumLength = 9, ErrorMessage = "Enter a valid phone number.")]
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "Allow numbers only.")]
        public string AmbulancePhoneNo2 { get; set; }
        [Required(ErrorMessage = "Please enter seat capacity of your medical")]
        [RegularExpression(@"^([1-9][0-9]{0,2}|1000)$", ErrorMessage = "Seat capacity limit exceeded.")]
        public int SeatCapacity { get; set; }
        public int MedicalId { get; set; }
        public DiagnosticServices DiagnosticService { get; set; }
        public HospitalServices HospitalService { get; set; }
        public OtherServices OtherService { get; set; }
        public MedicalFacilities MedicalFacility { get; set; }
        public Consultants Consultant { get; set; }
    }
}