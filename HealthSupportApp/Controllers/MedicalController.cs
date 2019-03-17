using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HealthSupportApp.Gateway.MedicalGateway;
using HealthSupportApp.Manager;
using HealthSupportApp.Manager.DoctorManager;
using HealthSupportApp.Manager.HomeManager;
using HealthSupportApp.Manager.MedicalManager;
using HealthSupportApp.Models;
using HealthSupportApp.Models.HomeModel;
using HealthSupportApp.Models.MedicalModel;

namespace HealthSupportApp.Controllers
{
    [Authorize(Roles = "Medical")]
    public class MedicalController : Controller
    {
        MedicalManager aMedicalManager = new MedicalManager();
        MedicalGateway aMedicalGateway = new MedicalGateway();
        DoctorManager aDoctorManager = new DoctorManager();
        HomeManager aHomeManager = new HomeManager();

        public string MedicalUsername = System.Web.HttpContext.Current.User.Identity.Name;
        //
        private MedicalAccount GetMedicalData()
        {
            MedicalAccount aMedicalAccount = new MedicalAccount();
            if (User.IsInRole("Medical"))
            {
                aMedicalAccount = aMedicalManager.GetMedicalData(MedicalUsername);
            }
            return aMedicalAccount;
        }
        // GET: /Medical/
        public ActionResult Index()
        {
            ViewBag.GetName = GetMedicalData().MedicalName;
            return View();
        }

        public ActionResult MedicalProfile()
        {
            if (User.IsInRole("Medical"))
            {
                MedicalAccount aMedicalAccount = aMedicalManager.GetMedicalData(MedicalUsername);
                ViewBag.GetMedical = aMedicalAccount;
            }
            ViewBag.GetName = GetMedicalData().MedicalName;
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult MedicalProfile(MedicalAccount aMedicalAccount)
        {
            if (aMedicalAccount.MedicalContactNo2 == null)
            {
                aMedicalAccount.MedicalContactNo2 = "";
            }
            if (aMedicalAccount.AmbulanceContact == null)
            {
                aMedicalAccount.AmbulanceContact = "";
            }
            if (User.IsInRole("Medical"))
            {
                ViewBag.Message = aMedicalManager.UpdateMedicalProfile(aMedicalAccount);
                aMedicalAccount = aMedicalManager.GetMedicalData(MedicalUsername);
                ViewBag.GetMedical = aMedicalAccount;
            }
            ViewBag.GetName = GetMedicalData().MedicalName;
            return View();
        }

        public ActionResult Services()
        {
            ViewBag.Message = TempData["Message"];
            int medicalId = GetMedicalData().MedicalId;
            ViewBag.GetHospitalServices =
                aMedicalGateway.GetHospitalServices(medicalId);
            ViewBag.GetDiagnosticServices = aMedicalGateway.GetDiagnosticServices(medicalId);
            ViewBag.GetOtherServices = aMedicalGateway.GetOtherServices(medicalId);
            ViewBag.GetMedicalFacilities = aMedicalGateway.GetMedicalFacilities(medicalId);
            ViewBag.GetMedicalConsultants = aMedicalGateway.GetMedicalConsultants(medicalId);
            ViewBag.GetEmergencyDetails = aMedicalGateway.GetEmergencyDetails(medicalId);
            List<DoctorsSpecialities> doctorsSpecialities = aDoctorManager.GetSpecialities();
            ViewBag.GetSpecialities = doctorsSpecialities;
            ViewBag.GetName = GetMedicalData().MedicalName;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Services(MedicalService aMedicalService)
        {
            int medicalId = GetMedicalData().MedicalId;
            if (User.IsInRole("Medical"))
            {
                if (aMedicalService.HospitalService != null)
                {
                    aMedicalService.HospitalService.MedicalId = medicalId;
                    bool rowAffected = aMedicalGateway.SaveHospitalService(aMedicalService.HospitalService.ServiceName, aMedicalService.HospitalService.MedicalId);
                    if (rowAffected)
                    {
                        ViewBag.Message = "Medical Service added successfully";
                    }
                    else
                    {
                        ViewBag.Error = "Medical Service not added. Try again.";
                    }
                }
                if (aMedicalService.DiagnosticService != null)
                {
                    aMedicalService.DiagnosticService.MedicalId = medicalId;
                    bool rowAffected = aMedicalGateway.SaveDiagnosticService(aMedicalService.DiagnosticService.ServiceName, aMedicalService.DiagnosticService.MedicalId);
                    if (rowAffected)
                    {
                        ViewBag.Message = "Diagnostic Service added successfully";
                    }
                    else
                    {
                        ViewBag.Error = "Diagnostic Service not added. Try again.";
                    }
                }
                if(aMedicalService.OtherService == null && aMedicalService.HospitalService == null && aMedicalService.DiagnosticService== null && aMedicalService.MedicalFacility == null && aMedicalService.Consultant ==null)
                {
                    MedicalService medicalService = aMedicalGateway.GetEmergencyDetails(medicalId);
                    if (medicalService != null)
                    {
                        aMedicalService.MedicalId = medicalId;
                        if (aMedicalService.EmergencyService == null)
                        {
                            aMedicalService.EmergencyService = medicalService.EmergencyService;
                        }
                        if (aMedicalService.AmbulanceService == null)
                        {
                            aMedicalService.AmbulanceService = medicalService.AmbulanceService;
                        }
                        if (aMedicalService.TwentyFourService == null)
                        {
                            aMedicalService.TwentyFourService = medicalService.TwentyFourService;
                        }
                        if (aMedicalService.EmergencyPhoneNo == null)
                        {
                            aMedicalService.EmergencyPhoneNo = medicalService.EmergencyPhoneNo;
                        }
                        if (aMedicalService.EmergencyPhoneNo2 == null)
                        {
                            aMedicalService.EmergencyPhoneNo2 = medicalService.EmergencyPhoneNo2;
                        }
                        if (aMedicalService.AmbulancePhoneNo == null)
                        {
                            aMedicalService.AmbulancePhoneNo = medicalService.AmbulancePhoneNo;
                        }
                        if (aMedicalService.AmbulancePhoneNo2 == null)
                        {
                            aMedicalService.AmbulancePhoneNo2 = medicalService.AmbulancePhoneNo2;
                        }
                        bool rowAffected = aMedicalGateway.EditEmergencyDetails(aMedicalService);
                        if (rowAffected)
                        {
                            ViewBag.Message = "Emergency details updated successfully";
                        }
                        else
                        {
                            ViewBag.Error = "Emergency details updating falied. Try again.";
                        }
                    }
                    else
                    {
                        aMedicalService.MedicalId = medicalId;
                        if (aMedicalService.EmergencyPhoneNo == null)
                        {
                            aMedicalService.EmergencyPhoneNo = "";
                        }
                        if (aMedicalService.EmergencyPhoneNo2 == null)
                        {
                            aMedicalService.EmergencyPhoneNo2 = "";
                        }
                        if (aMedicalService.AmbulancePhoneNo == null)
                        {
                            aMedicalService.AmbulancePhoneNo = "";
                        }
                        if (aMedicalService.AmbulancePhoneNo2 == null)
                        {
                            aMedicalService.AmbulancePhoneNo2 = "";
                        }
                        bool rowAffected = aMedicalGateway.SaveEmergencyDetails(aMedicalService);
                        if (rowAffected)
                        {
                            ViewBag.Message = "Emergency details saved successfully";
                        }
                        else
                        {
                            ViewBag.Error = "Emergency details saving falied. Try again.";
                        }
                    }
                }
            }
            ViewBag.GetHospitalServices = aMedicalGateway.GetHospitalServices(medicalId);
            ViewBag.GetDiagnosticServices = aMedicalGateway.GetDiagnosticServices(medicalId);
            ViewBag.GetOtherServices = aMedicalGateway.GetOtherServices(medicalId);
            ViewBag.GetMedicalFacilities = aMedicalGateway.GetMedicalFacilities(medicalId);
            ViewBag.GetMedicalConsultants = aMedicalGateway.GetMedicalConsultants(medicalId);
            ViewBag.GetEmergencyDetails= aMedicalGateway.GetEmergencyDetails(medicalId);
            List<DoctorsSpecialities> doctorsSpecialities = aDoctorManager.GetSpecialities();
            ViewBag.GetSpecialities = doctorsSpecialities;
            ViewBag.GetName = GetMedicalData().MedicalName;
            return View();
        }
        public ActionResult DeleteHospitalService(int id)
        {
            string message = "";
            if (User.IsInRole("Medical"))
            {
                bool delete = aMedicalGateway.DeleteHospitalService(id);
                if (delete)
                {
                    message = "Hospital service deleted.";
                }
                else
                {
                    message = "Hospital service not delete.";
                }
            }
            TempData["Message"] = message;
            return RedirectToAction("Services", "Medical");
        }
        public ActionResult DeleteDiagnosticService(int id)
        {
            string message = "";
            if (User.IsInRole("Medical"))
            {
                bool delete = aMedicalGateway.DeleteDiagnosticService(id);
                if (delete)
                {
                    message = "Diagnostic service deleted.";
                }
                else
                {
                    message = "Diagnostic service not delete.";
                }
            }
            TempData["Message"] = message;
            return RedirectToAction("Services", "Medical");
        }

        public ActionResult DoctorAssign()
        {
            List<DoctorsSpecialities> doctorsSpecialities = aDoctorManager.GetSpecialities();
            ViewBag.GetSpecialities = doctorsSpecialities;
            ViewBag.GetName = GetMedicalData().MedicalName;
            return View();
        }

        
        public ActionResult ViewDoctors()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            int medicalId = GetMedicalData().MedicalId;
            List<ViewDoctors> viewDoctors = aMedicalManager.GetAllDoctors(medicalId);
            ViewBag.GetAllDoctors = viewDoctors;
            ViewBag.GetName = GetMedicalData().MedicalName;
            return View();
        }
        
        public ActionResult ChangePassword()
        {
            ViewBag.GetName = GetMedicalData().MedicalName;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangeMedicalPassword changeMedicalPassword)
        {
            if (User.IsInRole("Medical"))
            {
                changeMedicalPassword.MedicalId = GetMedicalData().MedicalId;
                changeMedicalPassword.NewPassword = Crypto.Hash(changeMedicalPassword.NewPassword);
                changeMedicalPassword.OldPassword = Crypto.Hash(changeMedicalPassword.OldPassword);
                string message = aMedicalManager.ChangeMedicalPassword(changeMedicalPassword);
                if (message == "Success")
                {
                    ViewBag.Message = "Password has changed successfully";
                }
                else
                {
                    ViewBag.ErrorMessage = message;
                }
            }
            ViewBag.GetName = GetMedicalData().MedicalName;
            return View();
        }
        public ActionResult Forum()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            if (Session["GetRequestForumPosts"] == null)
            {
                List<ForumPost> forumPosts = aHomeManager.GetForumPosts();
                ViewBag.GetForumPosts = forumPosts;
            }
            else
            {
                ViewBag.GetForumPosts = Session["GetRequestForumPosts"];
            }
            List<TopicCategory> topicCategories = aHomeManager.GetTopicCategories();
            ViewBag.GetTopiCategories = topicCategories;
            ViewBag.GetName = GetMedicalData().MedicalName;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Forum(ForumPost forumPost)
        {
            if (User.IsInRole("Medical"))
            {
                forumPost.UserId = 0;
                forumPost.DoctorId = 0;
                forumPost.MedicalId = GetMedicalData().MedicalId;
                forumPost.Views = 0;
                forumPost.PostDate = DateTime.Now.ToLongDateString();
                int countTopicCategory = aHomeManager.GetCountCategory(forumPost.TopicCategoryId);
                countTopicCategory = countTopicCategory + 1;
                if (forumPost.Description == null)
                {
                    forumPost.Description = "";
                }
                forumPost.ImagePath = "";
                string message = aMedicalManager.SavePost(forumPost, countTopicCategory);
                if (message == "Success")
                {
                    ViewBag.Message = "Your post published successfully!";
                }
                else
                {
                    ViewBag.ErrorMessage = message;
                }
            }
            if (Session["GetRequestForumPosts"] == null)
            {
                List<ForumPost> forumPosts = aHomeManager.GetForumPosts();
                ViewBag.GetForumPosts = forumPosts;
            }
            else
            {
                ViewBag.GetForumPosts = Session["GetRequestForumPosts"];
            }
            List<TopicCategory> topicCategories = aHomeManager.GetTopicCategories();
            ViewBag.GetTopiCategories = topicCategories;
            ViewBag.GetName = GetMedicalData().MedicalName;
            return View();
        }
        public ActionResult Comment(string comment, int postId)
        {
            if (User.IsInRole("Medical"))
            {
                Comments aComment = new Comments();
                aComment.PostId = postId;
                aComment.Comment = comment;
                aComment.UserId = 0;
                aComment.DoctorId = 0;
                aComment.MedicalId = GetMedicalData().MedicalId;
                string message = aMedicalManager.SaveComment(aComment);
                if (message != "Success")
                {
                    TempData["ErrorMessage"] = message;
                }
            }
            return RedirectToAction("Forum", "Medical");
        }
        public ActionResult ForumTopicPost(int topicCategoryId)
        {
            if (topicCategoryId > 0)
            {
                List<ForumPost> forumPosts = aHomeManager.GetFormTopicPost(topicCategoryId);
                ViewBag.GetFormTopicPost = forumPosts;
            }
            ViewBag.GetName = GetMedicalData().MedicalName;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForumTopicPost(string comment, int postId, int categoryId)
        {
            if (User.IsInRole("Medical"))
            {
                Comments aComment = new Comments();
                aComment.PostId = postId;
                aComment.Comment = comment;
                aComment.UserId = 0;
                aComment.DoctorId = 0;
                aComment.MedicalId = GetMedicalData().MedicalId; ;
                string message = aMedicalManager.SaveComment(aComment);
                if (message != "Success")
                {
                    ViewBag.ErrorMessage = message;
                }
            }
            List<ForumPost> forumPosts = aHomeManager.GetFormTopicPost(categoryId);
            ViewBag.GetFormTopicPost = forumPosts;
            ViewBag.GetName = GetMedicalData().MedicalName;
            return View();
        }
        public ActionResult AllForumPosts()
        {
            List<ForumPost> forumPosts = aHomeManager.GetAllForumPosts();
            ViewBag.GetAllForumPosts = forumPosts;
            ViewBag.GetName = GetMedicalData().MedicalName;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AllForumPosts(string comment, int postId)
        {
            if (User.IsInRole("User"))
            {
                Comments aComment = new Comments();
                aComment.PostId = postId;
                aComment.Comment = comment;
                aComment.UserId = 0;
                aComment.DoctorId = 0;
                aComment.MedicalId = GetMedicalData().MedicalId;
                string message = aMedicalManager.SaveComment(aComment);
                if (message != "Success")
                {
                    ViewBag.ErrorMessage = message;
                }
            }
            List<ForumPost> forumPosts = aHomeManager.GetAllForumPosts();
            ViewBag.GetAllForumPosts = forumPosts;
            ViewBag.GetName = GetMedicalData().MedicalName;
            return View();
        }
    }
}