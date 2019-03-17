using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthSupportApp.Models.MedicalModel
{
    public class AboutMedical
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter a Title")]
        [StringLength(100, ErrorMessage ="Type limit exceeded. Write the title shortly")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter desctiption about your medical")]
        [StringLength(250, ErrorMessage = "Type limit exceeded. Write the description within 250 character")]
        public string Description { get; set; }
        public int MedicalId { get; set; }
    }
}