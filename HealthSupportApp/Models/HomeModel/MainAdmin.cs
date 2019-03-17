using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthSupportApp.Models.HomeModel
{
    public class MainAdmin
    {
        public int MainAdminId { get; set; }
        public string Role { get; set; }
        [Required(ErrorMessage = "Full name must required.")]
        [StringLength(80, ErrorMessage ="Maximum length exceeded! Maximum length of Name is 80 character.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Username must required.")]
        [StringLength(20, ErrorMessage = "Maximum length exceeded! Maximum length of Username is 20 character.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mobile number must required.", AllowEmptyStrings = false)]
        [RegularExpression(@"\+?(88)?0?1[356789][0-9]{8}\b", ErrorMessage = "Enter valid mobile number.")]
        [DisplayName("Mobile No.")]
        [StringLength(13, MinimumLength = 11, ErrorMessage = "Mobile number should be 11 digit.")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Password must required.")]
        [StringLength(50, ErrorMessage = "Maximum length exceeded! Maximum length of Password is 50 character.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password must required.")]
        [DisplayName("Confirm Password")]
        [System.Web.Mvc.Compare("Password", ErrorMessage ="Confirm Password not matched with Password")]
        public string ConfirmPassword { get; set; }
        [DisplayName("Main Admin Password")]
        [Required(ErrorMessage = "Main Admin password must required.")]
        [StringLength(50, ErrorMessage = "Maximum length exceeded! Maximum length of Main Admin Password is 50 character.")]
        public string MainAdminPassword { get; set; }
        public bool RememberMe { get; set; }
    }
}