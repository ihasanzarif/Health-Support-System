using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSupportApp.Models.MedicalModel
{
    public class MedicalAccount
    {
        [Key]
        public int MedicalId { get; set; }
        public string Role { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select type of your institution.")]
        [DisplayName("Institution Type")]
        public string MedicalType { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Hospital/Diagnostic center name.")]
        [DisplayName("Name of Hospital/Diagnostic Center")]
        [StringLength(50, ErrorMessage = "Name should be 50 Character long.")]
        public string MedicalName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Contact person's name.")]
        [DisplayName("Contact Person's Name")]
        [StringLength(50, ErrorMessage = "Name should be 50 Character long.")]
        public string ContactPersonName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Contact person's position required.")]
        [DisplayName("Contact Person's Position")]
        [StringLength(50, ErrorMessage = "Position name should be 50 Character long.")]
        public string ContactPersonPosition { get; set; }
        [RegularExpression(@"\+?(88)?0?1[356789][0-9]{8}\b", ErrorMessage = "Enter valid mobile number.")]
        [Required(ErrorMessage = "Contact person's position must required.", AllowEmptyStrings = false)]
        [DisplayName("Contact Person's Phone No.")]
        public string ContactPersonPhoneNo { get; set; }

        [Required(ErrorMessage = "Email must required.", AllowEmptyStrings = false)]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid Email.")]
        [DisplayName("Hospital/Diagnostic Center's Email")]
        [StringLength(50, ErrorMessage ="Email should be Maximum 50 character long.")]
        public string MedicalEmail { get; set; }
        
        [RegularExpression(@"\+?(88)?0?1[356789][0-9]{8}\b", ErrorMessage = "Enter valid mobile number.")]
        [Required(ErrorMessage = "Please enter Hospital's/Diagnostic center's contact number.", AllowEmptyStrings = false)]
        [DisplayName("Hospital Contact No. 1")]
        [StringLength(13, MinimumLength =11, ErrorMessage = "Mobile number should be 11 digit.")]
        public string MedicalContactNo1 { get; set; }
        [DisplayName("Hospital Contact No. 2")]
        [StringLength(11, MinimumLength = 9, ErrorMessage = "Enter a valid phone number.")]
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "Allow numbers only.")]
        public string MedicalContactNo2 { get; set; }

        [Required(ErrorMessage = "Hospital location must required.", AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessage ="Maximum length exceeded! Maximum length is 100 character long.")]
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
        
        [Required(ErrorMessage = "Please select an option")]
        [StringLength(20)]
        [DisplayName("Ambulance Service")]
        public string AmbulanceService { get; set; }
        [RegularExpression(@"\+?(88)?0?1[56789][0-9]{8}\b", ErrorMessage = "Enter valid mobile number.")]
        [DisplayName("Ambulance Contact")]
        [StringLength(13, MinimumLength = 11, ErrorMessage = "Mobile number should be 11 digit.")]
        public string AmbulanceContact { get; set; }
        [Required(ErrorMessage = "Please Enter Your Password.", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password should be Minimum 8 character long.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Confirm Your Password.", AllowEmptyStrings = false)]
        [NotMapped]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "Password not matched")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        public bool IsEmailVerified { get; set; }
        public Guid ActivationCode { get; set; }
        public string Status { get; set; }
        public string AccountCreatedDate { get; set; }
        public List<ViewDoctors> ViewDoctors { get; set; }
        public List<HospitalServices> HospitalSerives { get; set; }
        public List<DiagnosticServices> DiagnosticServices { get; set; }
        public List<MedicalFacilities> MedicalFacilities { get; set; }
        public MedicalService MedicalService { get; set; }
        public List<OtherServices> OtherServices { get; set; }
        public List<Consultants> Consultants { get; set; }
    }
}


//[RegularExpression(@"^0*(?:[1-9] [0-9] [0-9]?|1000)$", ErrorMessage = "Seat capacity limit exceeded.")]
//[DisplayName("Seat Capacity")]
//public string SeatCapacity { get; set; }
//[Required(ErrorMessage = "Please Select Yes or No.")]
//[StringLength(10)]
//[DisplayName("ICU/CCU")]
//public string IcuCcu { get; set; }