using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthSupportApp.Models.HomeModel
{
    public class ForumPost
    {
        public int ForumPostId { get; set; }
        [Required(ErrorMessage ="Please select a topic category.")]
        [DisplayName("Category")]
        public int TopicCategoryId { get; set; }
        public string TopicCategory { get; set; }
        [Required(ErrorMessage = "Please enter your topic name.")]
        [StringLength(120, ErrorMessage ="Maximum limit 120 character")]
        [DisplayName("Topic")]
        public string TopicName { get; set; }
        [StringLength(500, ErrorMessage = "Maximum limit 500 character")]
        [DisplayName("Description")]
        public string Description { get; set; }
        public string ImagePath { get; set; }
        [DisplayName("Add Photo")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.bmp|.jpeg|.PNG|.JPG|.BMP|.JPEG)$", ErrorMessage = "Only Image files allowed.")]
        public HttpPostedFileBase ImageFile { get; set; }
        public int Views { get; set; }
        public int UserId { get; set; }
        public int DoctorId { get; set; }
        public int MedicalId { get; set; }
        public string UserName { get; set; }
        public string PostDate { get; set; }
        public List<Comments> Comments { get; set; }
    }
}