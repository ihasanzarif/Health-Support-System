using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthSupportApp.Models.MedicalModel
{
    public class DoctorsSpecialities
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter speciality name.")]
        [StringLength(100)]
        public string Speciality { get; set; }
    }
}