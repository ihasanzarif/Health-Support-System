using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthSupportApp.Models.HomeModel
{
    public class Comments
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int DoctorId { get; set; }
        public int MedicalId { get; set; }
        public string  UserName { get; set; }
        public int PostId { get; set; }
        [StringLength(200, ErrorMessage ="Maximum limit 200 characters.")]
        public string Comment { get; set; }
    }
}