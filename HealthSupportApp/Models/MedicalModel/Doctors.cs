using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using HealthSupportApp.Models.DoctorModel;

namespace HealthSupportApp.Models.MedicalModel
{
    public class Doctors
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public bool PasswordVerified { get; set; }
        [DisplayName("")]
        public string ImagePath { get; set; }
        [DisplayName("Upload Profile Image")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.bmp|.jpeg|.PNG|.JPG|.BMP|.JPEG)$", ErrorMessage = "Only Image files allowed.")]
        public HttpPostedFileBase ImageFile { get; set; }
        [Required(ErrorMessage ="Please enter a name")]
        [StringLength(100, ErrorMessage ="Type limit exceeded. Enter a valid name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please select gender.")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please enter title/designation")]
        [StringLength(100, ErrorMessage = "Type limit exceeded. Enter a valid title/designation.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter speciality.")]
        [StringLength(100, ErrorMessage = "Type limit exceeded. Enter a valid speciality name.")]
        public string Speciality { get; set; }
        [Required(ErrorMessage = "Please enter work email")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid Email.")]
        [StringLength(80, ErrorMessage = "Type limit exceeded. Enter a valid email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter mobile number")]
        [RegularExpression(@"\+?(88)?0?1[356789][0-9]{8}\b", ErrorMessage = "Enter valid mobile number.")]
        [StringLength(13, MinimumLength = 11, ErrorMessage = "Mobile number should be 11 digit.")]
        [DisplayName("Mobile No.")]
        public string MobileNo { get; set; }
        public string Password { get; set; }
        [StringLength(11, MinimumLength = 9, ErrorMessage = "Enter a valid phone number.")]
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "Allow numbers only.")]
        [DisplayName("Phone No.")]
        public string PhoneNo { get; set; }
        [StringLength(6, ErrorMessage = "Type limit exceeded. Enter a valid BMDC reg.")]
        [Required(ErrorMessage = "Please enter BMDC Registration number.")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Allow numbers only.")]
        [DisplayName("BMDC Reg No.")]
        public string BmdcReg { get; set; }
        [Required(ErrorMessage = "Please enter degree or other specifications.")]
        [StringLength(100, ErrorMessage = "Type limit exceeded. Enter degree or other specifications in short.")]
        [DisplayName("Degree & Specifications")]
        public string OtherSpecification { get; set; }
        public int MedicalId { get; set; }
        public int VerificationCode { get; set; }
        public string Status { get; set; }
        public string AccountCreatedDate { get; set; }
        public AssignedDoctors AssignedDoctors { get; set; }
        public List<ViewDoctors> ViewDoctors { get; set; }
        public List<DoctorChamber> ViewDoctorChambers { get; set; }
    }
}