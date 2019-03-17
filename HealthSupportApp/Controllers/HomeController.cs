using HealthSupportApp.Manager;
using HealthSupportApp.Models;
using HealthSupportApp.Models.HomeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HealthSupportApp.Manager.HomeManager;
using HealthSupportApp.Models.MedicalModel;
using HealthSupportApp.Models.UserModel;

namespace HealthSupportApp.Controllers
{
    public class HomeController : Controller
    {
        HomeManager aHomeManager = new HomeManager();
        public ActionResult Index()
        {
            if (User.IsInRole("Medical"))
            {
                return RedirectToAction("Index", "Medical");
            }
            if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "User");
            }
            if (User.IsInRole("Doctor"))
            {
                return RedirectToAction("Index", "Doctor");
            }
            if (User.IsInRole("MainAdmin"))
            {
                return RedirectToAction("Index", "MainAdmin");
            }
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
                    List<Doctors> doctors = aHomeManager.GetDoctorSearchResult(aSearchModel);
                    List<MedicalAccount> medicalAccounts = new List<MedicalAccount>();
                    aSearchModel.SearchString = aSearchModel.SearchString;
                    Session["DoctorSearchResult"] = doctors;
                    Session["MedicalSearchResult"] = medicalAccounts;
                    Session["SearchKeywords"] = aSearchModel;
                }
                if (aSearchModel.SearchHospital)
                {
                    List<Doctors> doctors = new List<Doctors>();
                    List<MedicalAccount> medicalAccounts = aHomeManager.GetHospitalSearchResult(aSearchModel);
                    aSearchModel.SearchString = aSearchModel.SearchString;
                    Session["MedicalSearchResult"] = medicalAccounts;
                    Session["DoctorSearchResult"] = doctors;
                    Session["SearchKeywords"] = aSearchModel;
                }
                if (aSearchModel.SearchBloodDonor)
                {
                    aSearchModel.SearchString = aSearchModel.SearchString;
                }
               
                return RedirectToAction("SearchResult", "Home");
            }
            return View();
        }

        public ActionResult SearchResult()
        {
            if (User.IsInRole("Medical"))
            {
                return RedirectToAction("Index", "Medical");
            }
            if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "User");
            }
            if (User.IsInRole("Doctor"))
            {
                return RedirectToAction("Index", "Doctor");
            }
            if (User.IsInRole("MainAdmin"))
            {
                return RedirectToAction("Index", "MainAdmin");
            }
            SearchModel aSearchModel = Session["SearchKeywords"] as SearchModel;
            List<MedicalAccount> medicalAccounts = Session["MedicalSearchResult"] as List<MedicalAccount>;
            List<Doctors> doctors = Session["DoctorSearchResult"] as List<Doctors>;
            ViewBag.SearchResult = medicalAccounts;
            ViewBag.DoctorSearchResult = doctors;
            ViewBag.SearchKeywords = aSearchModel;
            return View();
        }

        public ActionResult ViewDetails(int id)
        {
            if (User.IsInRole("Medical"))
            {
                return RedirectToAction("Index", "Medical");
            }
            if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "User");
            }
            if (User.IsInRole("Doctor"))
            {
                return RedirectToAction("Index", "Doctor");
            }
            if (User.IsInRole("MainAdmin"))
            {
                return RedirectToAction("Index", "MainAdmin");
            }
            ViewBag.GetMedicalAllDetails = aHomeManager.GetMedicalSearchViewInfo(id);
            return View();
        }

        public ActionResult DoctorDetails(int id)
        {
            if (User.IsInRole("Medical"))
            {
                return RedirectToAction("Index", "Medical");
            }
            if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "User");
            }
            if (User.IsInRole("Doctor"))
            {
                return RedirectToAction("Index", "Doctor");
            }
            if (User.IsInRole("MainAdmin"))
            {
                return RedirectToAction("Index", "MainAdmin");
            }
            ViewBag.DoctorDetailsSearchResult = aHomeManager.GetDoctorDetailsSearchResult(id);
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
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Forum(string question)
        {
            TempData["InfoMessage"] = "Please login before ask a question. If you don't have an account please register!";
            return RedirectToAction("Login","Register");
        }
        public ActionResult BloodBank()
        {
            List<UserAccount> userAccounts = aHomeManager.GetBloodDonorUsers();
            ViewBag.GetBloodDonors = userAccounts;
            return View();
        }
        public ActionResult AllForumPosts()
        {
            List<ForumPost> forumPosts = aHomeManager.GetAllForumPosts();
            ViewBag.GetAllForumPosts = forumPosts;
            return View();
        }
        public ActionResult ForumTopicPost(int topicCategoryId)
        {
            if (topicCategoryId > 0)
            {
                List<ForumPost> forumPosts = aHomeManager.GetFormTopicPost(topicCategoryId);
                ViewBag.GetFormTopicPost = forumPosts;
            }
            return View();
        }
    }
}