using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HealthSupportApp.Models.UserModel
{
    public class UserAccount
    {
        [Key]
        public int UserId { get; set; }
        public string Role { get; set; }

        [Required(ErrorMessage = "Please enter your full name.", AllowEmptyStrings = false)]
        [DisplayName("Full Name")]
        [Column(TypeName = "varchar")]
        [StringLength(50, ErrorMessage = "Name should be 50 character long.")]
        public string Name { get; set; }
        [StringLength(50, ErrorMessage ="Email should be 50 character long.")]
        [RegularExpression(
            @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Please enter a valid Email.")]
        public string Email { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage = "Please enter your password.", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password should be minimum 6 and maximum 50 character long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.", AllowEmptyStrings = false)]
        [NotMapped]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "Password not matched")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Mobile number must required.", AllowEmptyStrings = false)]
        [RegularExpression(@"\+?(88)?0?1[356789][0-9]{8}\b", ErrorMessage ="Enter valid mobile number.")]
        [DisplayName("Mobile No.")]
        [StringLength(13, MinimumLength = 11, ErrorMessage = "Mobile number should be 11 digit.")]
        public string MobileNo { get; set; }
        [DisplayName("Gender")]
        [StringLength(10)]
        public string Gender { get; set; }
        [DisplayName("Age")]
        [StringLength(3, ErrorMessage ="Please enter your age properly.")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Allow numbers only.")]
        public string Age { get; set; }
        [Required(ErrorMessage = "Please select your blood group.", AllowEmptyStrings = false)]
        [StringLength(10)]
        [DisplayName("Blood Group")]
        public string BloodGroup { get; set; }
        [Required(ErrorMessage = "Please select Yes or No", AllowEmptyStrings = false)]
        [DisplayName("Want to Donate Blood?")]
        public bool WantToGiveBlood { get; set; }
        [Required(ErrorMessage = "Please enter your location.", AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessage ="Maximum length exceeded.")]
        public string Location { get; set; }
    }
}