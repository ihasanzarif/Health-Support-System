using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HealthSupportApp.Manager;
using HealthSupportApp.Manager.DoctorManager;
using HealthSupportApp.Manager.HomeManager;
using HealthSupportApp.Manager.UserManager;
using HealthSupportApp.Models;
using HealthSupportApp.Models.HomeModel;
using HealthSupportApp.Models.MedicalModel;
using HealthSupportApp.Models.UserModel;

namespace HealthSupportApp.Controllers
{
     [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        private UserAccount GetUserData()
        {
            UserAccount aUserAccount = new UserAccount();
            if (User.IsInRole("User"))
            {
                string mobileNo = System.Web.HttpContext.Current.User.Identity.Name;
                aUserAccount = aUserManager.GetUserData(mobileNo);
            }
            return aUserAccount;
        }
        UserManager aUserManager = new UserManager();
        HomeManager aHomeManager = new HomeManager();
        DoctorManager aDoctorManager = new DoctorManager();
        //
        // GET: /Index/
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            List<BookAppointment> userAppointments = aUserManager.GetUserAppointments(GetUserData().UserId);
            foreach (BookAppointment userAppoint in userAppointments)
            {
                DateTime appointDate =  Convert.ToDateTime(userAppoint.AppointDate);
                if (DateTime.Now.Date > appointDate)
                {
                    userAppoint.Expired = true;
                }
                else
                {
                    userAppoint.Expired = false;
                }
            }
            ViewBag.GetUserAppointments = userAppointments;
            ViewBag.GetName = GetUserData().Name;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SearchModel aSearchModel)
        {
            Session["DoctorSearchResult"] = null;
            Session["MedicalSearchResult"] = null;
            Session["SearchKeywords"] = null;
            if (aSearchModel.SearchDoctor == false)
            {
                ModelState.Remove("SearchDoctor");
            }
            if (aSearchModel.SearchHospital == false)
            {
                ModelState.Remove("SearchHospital");
            }
            if (aSearchModel.SearchBloodDonor == false)
            {
                ModelState.Remove("SearchBloodDonor");
            }
            if (ModelState.IsValid)
            {
                if (aSearchModel.SearchDoctor)
                {
                    List<Doctors> doctors = aUserManager.GetDoctorSearchResult(aSearchModel);
                    List<MedicalAccount> medicalAccounts = new List<MedicalAccount>();
                    Session["DoctorSearchResult"] = doctors;
                    Session["MedicalSearchResult"] = medicalAccounts;
                    Session["SearchKeywords"] = aSearchModel;
                }
                if (aSearchModel.SearchHospital)
                {
                    List<Doctors> doctors = new List<Doctors>();
                    List<MedicalAccount> medicalAccounts = aUserManager.GetHospitalSearchResult(aSearchModel);
                    Session["MedicalSearchResult"] = medicalAccounts;
                    Session["DoctorSearchResult"] = doctors;
                    Session["SearchKeywords"] = aSearchModel;
                }
                if (aSearchModel.SearchBloodDonor)
                {
                    aSearchModel.SearchString = aSearchModel.SearchString;
                }

                return RedirectToAction("SearchResult", "User");
            }
            return View();
        }

        public ActionResult SearchResult()
        {
            SearchModel aSearchModel = Session["SearchKeywords"] as SearchModel;
            List<MedicalAccount> medicalAccounts = Session["MedicalSearchResult"] as List<MedicalAccount>;
            List<Doctors> doctors = Session["DoctorSearchResult"] as List<Doctors>;
            ViewBag.SearchResult = medicalAccounts;
            ViewBag.DoctorSearchResult = doctors;
            ViewBag.SearchKeywords = aSearchModel;
            ViewBag.GetName = GetUserData().Name;
            return View();
        }

        public ActionResult ViewDetails(int id)
        {
            ViewBag.GetMedicalAllDetails = aHomeManager.GetMedicalSearchViewInfo(id);
            ViewBag.GetName = GetUserData().Name;
            return View();
        }
        public ActionResult BookAppointment(int doctorId, string schedule, int? medicalId, int? doctorChmaberId, DayOfWeek day)
        {
            BookAppointment bookAppointment = new BookAppointment();
            if (medicalId != null)
            {
                bookAppointment = aUserManager.GetMedicalChamberForAppoint(doctorId, medicalId);
                bookAppointment.AppointDate = GetNextWeekday(day).ToLongDateString();
                bookAppointment.ChamberId = medicalId;
                bookAppointment.Schedule = GetSchedule(schedule, day);
                bookAppointment.AppointPrice = 20;
                Session["BookAppoint"] = bookAppointment;
                ViewBag.GetDoctorDataForAppoint = bookAppointment;
            }
            else if (doctorChmaberId != null)
            {
                bookAppointment = aUserManager.GetDoctorChamberForAppoint(doctorId, doctorChmaberId);
                bookAppointment.AppointDate = GetNextWeekday(day).ToLongDateString();
                bookAppointment.ChamberId = doctorChmaberId;
                bookAppointment.Schedule = GetSchedule(schedule, day);
                bookAppointment.AppointPrice = 20;
                Session["BookAppoint"] = bookAppointment;
                ViewBag.GetDoctorDataForAppoint = bookAppointment;
            }
            else
            {
                return RedirectToAction("SearchResult", "User");
            }
            ViewBag.GetName = GetUserData().Name;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookAppointment(BookAppointment bookAppointment)
        {
            if (User.IsInRole("User"))
            {
                bookAppointment.SerialNo = GetUserData().UserId;
                bookAppointment.UserId = GetUserData().UserId;
                bookAppointment.UserName = GetUserData().Name;
                bookAppointment.MobileNo = System.Web.HttpContext.Current.User.Identity.Name;
                bookAppointment.Status = "Pending";
                string message = aUserManager.ConfirmBookAppointment(bookAppointment);
                if (message == "Success")
                {
                    ViewBag.Message = "We have recieved your booking confirmation. If your payment process successful you will get confirmation message and Serial number.";
                }
                else
                {
                    ViewBag.ErrorMessage = message;
                }
            }
            ViewBag.GetName = GetUserData().Name;
            ViewBag.GetDoctorDataForAppoint = Session["BookAppoint"] as BookAppointment;
            return View();
        }
        public DateTime GetNextWeekday(DayOfWeek day)
        {
            DateTime result = DateTime.Now.AddDays(1);
            while (result.DayOfWeek != day)
                result = result.AddDays(1);
            return result;
        }
        public string GetSchedule(string scheduleTime, DayOfWeek day)
        {
            string schedule = "";
            if (day == DayOfWeek.Saturday)
            {
                schedule = "Saturday, " + scheduleTime;
            }
            if (day == DayOfWeek.Sunday)
            {
                schedule = "Sunday, " + scheduleTime;
            }
            if (day == DayOfWeek.Monday)
            {
                schedule = "Monday, " + scheduleTime;
            }
            if (day == DayOfWeek.Tuesday)
            {
                schedule = "Tuesday, " + scheduleTime;
            }
            if (day == DayOfWeek.Wednesday)
            {
                schedule = "Wednesday, " + scheduleTime;
            }
            if (day == DayOfWeek.Thursday)
            {
                schedule = "Thursday, " + scheduleTime;
            }
            if (day == DayOfWeek.Friday)
            {
                schedule = "Friday, " + scheduleTime;
            }
            return schedule;
        }
        public ActionResult DoctorDetails(int id)
        {
            ViewBag.DoctorDetailsSearchResult = aUserManager.GetDoctorDetailsSearchResult(id);
            ViewBag.GetName = GetUserData().Name;
            return View();
        }

        public ActionResult UserProfile()
        {
            if (User.IsInRole("User"))
            {
                string mobileNo = System.Web.HttpContext.Current.User.Identity.Name;
                UserAccount aUserAccount = aUserManager.GetUserData(mobileNo);
                ViewBag.GetUser = aUserAccount;
            }
            ViewBag.GetName = GetUserData().Name;
            return View();
        }

        [HttpPost]
        public ActionResult UserProfile(UserAccount aUserAccount)
        {
            string mobileNo = System.Web.HttpContext.Current.User.Identity.Name;
            if (User.IsInRole("User"))
            {
                if (aUserAccount.Gender == null)
                {
                    aUserAccount.Gender = "";
                }
                if (aUserAccount.Age == null)
                {
                    aUserAccount.Age = "";
                }
                if (aUserAccount.Email == null)
                {
                    aUserAccount.Email = "";
                }
                if (aUserAccount.Location == null)
                {
                    aUserAccount.Location = "";
                }
                string message = aUserManager.EditUserProfile(aUserAccount);
                if (message == "Success")
                {
                    ViewBag.Message = "Your profile updated successfully";
                }
                else
                {
                    ViewBag.ErrorMessage = message;
                }
            }
            aUserAccount = aUserManager.GetUserData(mobileNo);
            ViewBag.GetUser = aUserAccount;
            ViewBag.GetName = GetUserData().Name;
            return View();
        }

        public ActionResult Appointments()
        {
            List<BookAppointment> usersAppointments = aUserManager.GetUserAllAppointments(GetUserData().UserId);
            ViewBag.GetAllAppointments = usersAppointments;
            ViewBag.GetName = GetUserData().Name;
            return View();
        }

        public ActionResult ChangePassword()
        {
            ViewBag.GetName = GetUserData().Name;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangeUserPassword changeUserPassword)
        {
            if (User.IsInRole("User"))
            {
                changeUserPassword.UserId = GetUserData().UserId;
                changeUserPassword.NewPassword = Crypto.Hash(changeUserPassword.NewPassword);
                changeUserPassword.OldPassword = Crypto.Hash(changeUserPassword.OldPassword);
                string message = aUserManager.ChangeUserPassword(changeUserPassword);
                if (message == "Success")
                {
                    ViewBag.Message = "Your password has changed successfully";
                }
                else
                {
                    ViewBag.ErrorMessage = message;
                }
            }
            ViewBag.GetName = GetUserData().Name;
            return View();
        }
        public ActionResult Forum()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            List<ForumPost> forumPosts = aHomeManager.GetForumPosts();
            ViewBag.GetForumPosts = forumPosts;
            List<TopicCategory> topicCategories = aHomeManager.GetTopicCategories();
            ViewBag.GetName = GetUserData().Name;
            ViewBag.GetTopiCategories = topicCategories;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Forum(ForumPost forumPost)
        {
            if (User.IsInRole("User"))
            {
                forumPost.UserId = GetUserData().UserId;
                forumPost.DoctorId = 0;
                forumPost.MedicalId = 0;
                forumPost.Views = 0;
                forumPost.PostDate = DateTime.Now.ToLongDateString();
                int countTopicCategory = aHomeManager.GetCountCategory(forumPost.TopicCategoryId);
                countTopicCategory = countTopicCategory + 1;
                if (forumPost.Description == null)
                {
                    forumPost.Description = "";
                }
                forumPost.ImagePath = DoctorImage(forumPost.ImageFile);
                string message = aUserManager.SavePost(forumPost, countTopicCategory);
                if (message == "Success")
                {
                    ViewBag.Message = "Your post published successfully!";
                }
                else
                {
                    ViewBag.ErrorMessage = message;
                }
            }
            List<ForumPost> forumPosts = aHomeManager.GetForumPosts();
            ViewBag.GetForumPosts = forumPosts;
            List<TopicCategory> topicCategories = aHomeManager.GetTopicCategories();
            ViewBag.GetTopiCategories = topicCategories;
            ViewBag.GetName = GetUserData().Name;
            return View();
        }
        private string DoctorImage(HttpPostedFileBase doctorImage)
        {
            var fileName = String.Empty;
            if (doctorImage != null)
            {
                try
                {
                    byte[] bytes = new byte[doctorImage.ContentLength];
                    using (BinaryReader theReader = new BinaryReader(doctorImage.InputStream))
                    {
                        bytes = theReader.ReadBytes(doctorImage.ContentLength);
                    }
                    System.Drawing.Image image;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        image = new Bitmap(System.Drawing.Image.FromStream(ms));
                        Bitmap target = new Bitmap(350, 420, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
                        string name = "doctor" + Guid.NewGuid() + ".png";
                        fileName = "~/Image/" + name;
                        var imageFile = Path.Combine(Server.MapPath("~/Image/"), name);
                        Graphics graphic = Graphics.FromImage(target);
                        graphic.DrawImage(image, 0, 0, 350, 420);
                        graphic.Dispose();
                        target.Save(imageFile);
                    }
                }
                catch (Exception ex)
                {
                    string st = ex.Message;
                }
            }
            else
            {
                fileName = "";
            }
            return fileName;
        }
        public ActionResult Comment(string comment, int postId)
        {
            if (User.IsInRole("User"))
            {
                Comments aComment = new Comments();
                aComment.PostId = postId;
                aComment.Comment = comment;
                aComment.UserId = GetUserData().UserId;
                aComment.DoctorId = 0;
                aComment.MedicalId = 0;
                string message = aUserManager.SaveComment(aComment);
                if (message != "Success")
                {
                    TempData["ErrorMessage"] = message;
                }
            }
            return RedirectToAction("Forum", "User");
        }
        public ActionResult BloodBank()
        {
            List<UserAccount> userAccounts = aHomeManager.GetBloodDonorUsers();
            ViewBag.GetBloodDonors = userAccounts;
            UserAccount aUserAccount = aUserManager.GetBloodDonateStatus(GetUserData().UserId);
            ViewBag.GetBloodDonateStatus = aUserAccount;
            ViewBag.GetName = GetUserData().Name;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BloodBank(UserAccount userAccount)
        {
            if (User.IsInRole("User"))
            {
                userAccount.UserId = GetUserData().UserId;
                string message = aUserManager.UpdateBloodDonateStatus(userAccount);
                if (message == "Success")
                {
                    ViewBag.Message = "Blood donate status updated successfully.";
                }
                else
                {
                    ViewBag.ErrorMessage = message;
                }
            }
            List<UserAccount> userAccounts = aHomeManager.GetBloodDonorUsers();
            ViewBag.GetBloodDonors = userAccounts;
            UserAccount aUserAccount = aUserManager.GetBloodDonateStatus(GetUserData().UserId);
            ViewBag.GetBloodDonateStatus = aUserAccount;
            ViewBag.GetName = GetUserData().Name;
            return View();
        }
        public ActionResult AllForumPosts()
        {
            List<ForumPost> forumPosts = aHomeManager.GetAllForumPosts();
            ViewBag.GetAllForumPosts = forumPosts;
            ViewBag.GetName = GetUserData().Name;
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
                aComment.UserId = GetUserData().UserId;
                aComment.DoctorId = 0;
                aComment.MedicalId = 0;
                string message = aUserManager.SaveComment(aComment);
                if (message != "Success")
                {
                    ViewBag.Message = message;
                }
            }
            List<ForumPost> forumPosts = aHomeManager.GetAllForumPosts();
            ViewBag.GetAllForumPosts = forumPosts;
            ViewBag.GetName = GetUserData().Name;
            return View();
        }
        public ActionResult ForumTopicPost(int topicCategoryId)
        {
            if (topicCategoryId > 0)
            {
                List<ForumPost> forumPosts = aHomeManager.GetFormTopicPost(topicCategoryId);
                ViewBag.GetFormTopicPost = forumPosts;
            }
            ViewBag.GetName = GetUserData().Name;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForumTopicPost(string comment, int postId, int categoryId)
        {
            if (User.IsInRole("User"))
            {
                Comments aComment = new Comments();
                aComment.PostId = postId;
                aComment.Comment = comment;
                aComment.UserId = GetUserData().UserId;
                aComment.DoctorId = 0;
                aComment.MedicalId = 0;
                string message = aUserManager.SaveComment(aComment);
                if (message != "Success")
                {
                    ViewBag.ErrorMessage = message;
                }
            }
            List<ForumPost> forumPosts = aHomeManager.GetFormTopicPost(categoryId);
            ViewBag.GetFormTopicPost = forumPosts;
            ViewBag.GetName = GetUserData().Name;
            return View();
        }
        public ActionResult CancelAppointment(int id, int doctorId)
        {
            if (User.IsInRole("User"))
            {
                if (id != 0 && doctorId != 0)
                {
                    int userId = GetUserData().UserId;
                    string mobileNo = GetUserData().MobileNo;
                    string message = aDoctorManager.CancelUserAppointment(id, doctorId, userId, mobileNo);
                    if (message == "Success")
                    {
                        TempData["Message"] = "Appointment canceled successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = message;
                    }
                }
            }
            return RedirectToAction("Index", "User");
        }

        public ActionResult Be_a_donor()
        {
            ViewBag.GetName = GetUserData().Name;
            return View();
        }
        public ActionResult Blood_fact()
        {
            ViewBag.GetName = GetUserData().Name;
            return View();
        }
        public ActionResult Eligibility()
        {
            ViewBag.GetName = GetUserData().Name;
            return View();
        }
    }
}