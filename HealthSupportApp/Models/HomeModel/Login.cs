using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace HealthSupportApp.Models.HomeModel
{
    public class Login
    {
        [Required(ErrorMessage = "Please enter your email or phone number.")]
        [StringLength(30, ErrorMessage = "Maximum length exceeded! Maximum length is 30 character.")]
        [Display(Name = "Login ID")]
        public string LoginId { get; set; }
        [Required(ErrorMessage = "Please enter your password.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password should be minimum 6 and maximum 50 character long.")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DisplayName("Remember Me!")]
        public bool RememberMe { get; set; }
    }
}