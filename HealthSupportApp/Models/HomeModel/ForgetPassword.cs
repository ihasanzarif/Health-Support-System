using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HealthSupportApp.Models.HomeModel
{
    public class ForgetPassword
    {
        [Required(ErrorMessage = "Please enter your Login ID.", AllowEmptyStrings = false)]
        public string LoginId { get; set; }
        [Required(ErrorMessage = "Please enter contact person's no.", AllowEmptyStrings = false)]
        public string ContactPersonMobileNo { get; set; }
        [Required(ErrorMessage = "Please enter the verification code.", AllowEmptyStrings = false)]
        public int VerificationCode { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage = "Please enter your password.", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password should be minimum 6 and maximum 50 character long.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please confirm your password.", AllowEmptyStrings = false)]
        [NotMapped]
        [System.Web.Mvc.Compare("NewPassword", ErrorMessage = "Password not matched")]
        [DisplayName("Confirm Password")]
        public int ConfirmNewPassword { get; set; }
    }
}