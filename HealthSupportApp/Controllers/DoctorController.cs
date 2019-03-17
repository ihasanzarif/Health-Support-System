using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using HealthSupportApp.Manager;
using HealthSupportApp.Manager.DoctorManager;
using HealthSupportApp.Manager.HomeManager;
using HealthSupportApp.Models;
using HealthSupportApp.Models.DoctorModel;
using HealthSupportApp.Models.HomeModel;
using HealthSupportApp.Models.MedicalModel;
using HealthSupportApp.Models.UserModel;
using ChangePassword = HealthSupportApp.Models.DoctorModel.ChangePassword;

namespace HealthSupportApp.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorController : Controller
    {
        DoctorManager aDoctorManager = new DoctorManager();
        HomeManager aHomeManager = new HomeManager();
        MainAdminManager aMainAdminManager = new MainAdminManager();
        private Doctors GetDoctorData()
        {
            string username = System.Web.HttpContext.Current.User.Identity.Name;
            Doctors aDoctor = new Doctors();
            if (User.IsInRole("Doctor"))
            {
                aDoctor = aDoctorManager.GetDoctorData(username);
            }
            return aDoctor;
        }
        // GET: Doctor
        public ActionResult Index()
        {
            int doctorId = GetDoctorData().Id;
            ViewBag.ChangeTempPassMsg = TempData["ChangeTempPassMsg"];
            ViewBag.GetName = GetDoctorData().Name;
            ViewBag.Message = TempData["Message"];
            List<ViewDoctors> doctorsChambers = aDoctorManager.GetDoctorChambers(doctorId);
            List<DoctorChamber> doctorOwnChamber = aDoctorManager.GetDoctorOwnChamber(doctorId);
            List<OrderStatus> orderDetails = aDoctorManager.GetOrderStatus(doctorId);
            List<Appointments> userAppointments = aDoctorManager.GetUserAppointments(doctorId);
            ViewBag.DoctorChambers = doctorsChambers;
            ViewBag.DoctorOwnChambers = doctorOwnChamber;
            ViewBag.OrderStatus = orderDetails;
            ViewBag.GetUserAppointments = userAppointments;
            return View();
        }
        public ActionResult ManageAppointment()
        {
            int doctorId = GetDoctorData().Id;
            ViewBag.Message = TempData["Message"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            ViewBag.GetName = GetDoctorData().Name;
            List<ViewDoctors> doctorsChambers = aDoctorManager.GetDoctorChambers(doctorId);
            List<DoctorChamber> doctorOwnChamber = aDoctorManager.GetDoctorOwnChamber(doctorId);
            List<OrderStatus> orderDetails = aDoctorManager.GetOrderStatus(doctorId);
            ViewBag.DoctorChambers = doctorsChambers;
            ViewBag.DoctorOwnChambers = doctorOwnChamber;
            ViewBag.OrderStatus = orderDetails;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageAppointment(bool onlineAvailability, int? medicalId, int? doctorChamberId)
        {
            int doctorId = GetDoctorData().Id;
            if (User.IsInRole("Doctor"))
            {
                if (medicalId != null)
                {
                    if (aDoctorManager.CheckManageAppointment(doctorId, medicalId))
                    {
                        string message = aDoctorManager.ExistAppointmentAvailability(onlineAvailability, doctorId, medicalId);
                        if (message == "Success")
                        {
                            ViewBag.Message = "Manage Appointment availability changed successfully";
                        }
                        else
                        {
                            ViewBag.ErrorMessage = message;
                        }
                    }
                    else
                    {
                        ManageAppointment manageAppointment = new ManageAppointment();
                        manageAppointment.UsedAppoint = 0;
                        manageAppointment.TotalAvailableAppoint = 0;
                        manageAppointment.DoctorId = doctorId;
                        manageAppointment.MedicalId = Convert.ToInt32(medicalId);
                        manageAppointment.DoctorChamberId = 0;
                        manageAppointment.OnlineAppointment = onlineAvailability;
                        string message = aDoctorManager.AppointmentAvailability(manageAppointment);
                        if (message == "Success")
                        {
                            CreateOrderForChamber(manageAppointment);
                            ViewBag.Message = "Manage Appointment availability changed successfully";
                        }
                        else
                        {
                            ViewBag.ErrorMessage = message;
                        }
                    }
                }
                if (doctorChamberId != null)
                {
                    if (aDoctorManager.CheckChamberManageAppointment(doctorId, doctorChamberId))
                    {
                        string message = aDoctorManager.ExistChamberAppointmentAvailability(onlineAvailability, doctorId, doctorChamberId);
                        if (message == "Success")
                        {
                            ViewBag.Message = "Manage Appointment availability changed successfully";
                        }
                        else
                        {
                            ViewBag.ErrorMessage = message;
                        }
                    }
                    else
                    {
                        ManageAppointment manageAppointment = new ManageAppointment();
                        manageAppointment.UsedAppoint = 0;
                        manageAppointment.TotalAvailableAppoint = 0;
                        manageAppointment.DoctorId = doctorId;
                        manageAppointment.MedicalId = 0;
                        manageAppointment.DoctorChamberId = (int)doctorChamberId;
                        manageAppointment.OnlineAppointment = onlineAvailability;
                        string message = aDoctorManager.ChamberAppointmentAvailability(manageAppointment);
                        if (message == "Success")
                        {
                            CreateOrderForChamber(manageAppointment);
                            ViewBag.Message = "Manage Appointment availability changed successfully";
                        }
                        else
                        {
                            ViewBag.ErrorMessage = message;
                        }
                    }
                }
            }
            List<ViewDoctors> doctorsChambers = aDoctorManager.GetDoctorChambers(doctorId);
            List<DoctorChamber> doctorOwnChamber = aDoctorManager.GetDoctorOwnChamber(doctorId);
            List<OrderStatus> orderDetails = aDoctorManager.GetOrderStatus(doctorId);
            ViewBag.DoctorChambers = doctorsChambers;
            ViewBag.DoctorOwnChambers = doctorOwnChamber;
            ViewBag.OrderStatus = orderDetails;
            ViewBag.GetName = GetDoctorData().Name;
            return View();
        }
        private void CreateOrderForChamber(ManageAppointment manageAppointment)
        {
            int manageId = aDoctorManager.GetManageId(manageAppointment.DoctorId, manageAppointment.MedicalId, manageAppointment.DoctorChamberId);
            DoctorAppointOrder doctorAppointOrder = new DoctorAppointOrder();
            doctorAppointOrder.ManageAppointId = manageId;
            doctorAppointOrder.AppointAmount = 0;
            doctorAppointOrder.TotalPrice = 0;
            DateTime orderDate = new DateTime(1971, 2, 1); 
            doctorAppointOrder.OrderDate = orderDate;
            doctorAppointOrder.ExpireDate = orderDate;
            doctorAppointOrder.RemainingAppoint = 0;
            doctorAppointOrder.PaymentMethod = "";
            doctorAppointOrder.TrnxId = "";
            doctorAppointOrder.Expired = false;
            doctorAppointOrder.Status = "Active";
            int rowAffected = aDoctorManager.CreateOrderForChamber(doctorAppointOrder);
        }

        public ActionResult UpdateManageAppointment(ManageAppointment manageAppointment)
        {
            if (User.IsInRole("Doctor"))
            {
                ManageAppointment previousManageInfo = aDoctorManager.GetDoctorChamberManageInfo(GetDoctorData().Id, manageAppointment.MedicalId, manageAppointment.DoctorChamberId);
                manageAppointment.DoctorId = GetDoctorData().Id;
                manageAppointment.UsedAppoint = manageAppointment.SatCapacity + manageAppointment.SunCapacity +
                                                manageAppointment.MonCapacity + manageAppointment.TueCapacity +
                                                manageAppointment.WedCapacity + manageAppointment.ThuCapacity +
                                                manageAppointment.FriCapacity;
                if (manageAppointment.TotalAvailableAppoint == 0)
                {
                    manageAppointment.SatAvailableAppoint = manageAppointment.SatCapacity;
                    manageAppointment.SunAvailableAppoint = manageAppointment.SunCapacity;
                    manageAppointment.MonAvailableAppoint = manageAppointment.MonCapacity;
                    manageAppointment.TueAvailableAppoint = manageAppointment.TueCapacity;
                    manageAppointment.WedAvailableAppoint = manageAppointment.WedCapacity;
                    manageAppointment.ThuAvailableAppoint = manageAppointment.ThuCapacity;
                    manageAppointment.FriAvailableAppoint = manageAppointment.FriCapacity;
                    manageAppointment.TotalAvailableAppoint = manageAppointment.UsedAppoint;
                }
                else
                {
                    if (manageAppointment.SatCapacity > previousManageInfo.SatCapacity)
                    {
                        manageAppointment.SatAvailableAppoint = (manageAppointment.SatCapacity - previousManageInfo.SatAvailableAppoint) + previousManageInfo.SatAvailableAppoint;
                    }
                    if (manageAppointment.SunCapacity > previousManageInfo.SunCapacity)
                    {
                        manageAppointment.SunAvailableAppoint = (manageAppointment.SunCapacity - previousManageInfo.SunAvailableAppoint) + previousManageInfo.SunAvailableAppoint;
                    }
                    if (manageAppointment.MonCapacity > previousManageInfo.MonCapacity)
                    {
                        manageAppointment.MonAvailableAppoint = (manageAppointment.MonCapacity - previousManageInfo.MonAvailableAppoint) + previousManageInfo.MonAvailableAppoint;
                    }
                    if (manageAppointment.TueCapacity > previousManageInfo.TueCapacity)
                    {
                        manageAppointment.TueAvailableAppoint = (manageAppointment.TueCapacity - previousManageInfo.TueAvailableAppoint) + previousManageInfo.TueAvailableAppoint;
                    }
                    if (manageAppointment.WedCapacity > previousManageInfo.WedCapacity)
                    {
                        manageAppointment.WedAvailableAppoint = (manageAppointment.WedCapacity - previousManageInfo.WedAvailableAppoint) + previousManageInfo.WedAvailableAppoint;
                    }
                    if (manageAppointment.ThuCapacity > previousManageInfo.ThuCapacity)
                    {
                        manageAppointment.ThuAvailableAppoint = (manageAppointment.ThuCapacity - previousManageInfo.ThuAvailableAppoint) + previousManageInfo.ThuAvailableAppoint;
                    }
                    if (manageAppointment.FriCapacity > previousManageInfo.FriCapacity)
                    {
                        manageAppointment.FriAvailableAppoint = (manageAppointment.FriCapacity - previousManageInfo.FriAvailableAppoint) + previousManageInfo.FriAvailableAppoint;
                    }
                    if (manageAppointment.SatCapacity < previousManageInfo.SatCapacity)
                    {
                        manageAppointment.SatAvailableAppoint = previousManageInfo.SatAvailableAppoint - (previousManageInfo.SatAvailableAppoint - manageAppointment.SatCapacity);
                    }
                    if (manageAppointment.SunCapacity < previousManageInfo.SunCapacity)
                    {
                        manageAppointment.SunAvailableAppoint = previousManageInfo.SunAvailableAppoint - (previousManageInfo.SunAvailableAppoint - manageAppointment.SunCapacity);
                    }
                    if (manageAppointment.MonCapacity < previousManageInfo.MonCapacity)
                    {
                        manageAppointment.MonAvailableAppoint = previousManageInfo.MonAvailableAppoint - (previousManageInfo.MonAvailableAppoint - manageAppointment.MonCapacity);
                    }
                    if (manageAppointment.TueCapacity < previousManageInfo.TueCapacity)
                    {
                        manageAppointment.TueAvailableAppoint = previousManageInfo.TueAvailableAppoint - (previousManageInfo.TueAvailableAppoint - manageAppointment.TueCapacity);
                    }
                    if (manageAppointment.WedCapacity < previousManageInfo.WedCapacity)
                    {
                        manageAppointment.WedAvailableAppoint = previousManageInfo.WedAvailableAppoint - (previousManageInfo.WedAvailableAppoint - manageAppointment.WedCapacity);
                    }
                    if (manageAppointment.ThuCapacity < previousManageInfo.ThuCapacity)
                    {
                        manageAppointment.ThuAvailableAppoint = previousManageInfo.ThuAvailableAppoint - (previousManageInfo.ThuAvailableAppoint - manageAppointment.ThuCapacity);
                    }
                    if (manageAppointment.FriCapacity < previousManageInfo.FriCapacity)
                    {
                        manageAppointment.FriAvailableAppoint = previousManageInfo.FriAvailableAppoint - (previousManageInfo.FriAvailableAppoint - manageAppointment.FriCapacity);
                    }
                    if (manageAppointment.SatCapacity == previousManageInfo.SatCapacity)
                    {
                        manageAppointment.SatAvailableAppoint = previousManageInfo.SatCapacity;
                    }
                    if (manageAppointment.SunCapacity == previousManageInfo.SunCapacity)
                    {
                        manageAppointment.SunAvailableAppoint = previousManageInfo.SunCapacity;
                    }
                    if (manageAppointment.MonCapacity == previousManageInfo.MonCapacity)
                    {
                        manageAppointment.MonAvailableAppoint = previousManageInfo.MonCapacity;
                    }
                    if (manageAppointment.TueCapacity == previousManageInfo.TueCapacity)
                    {
                        manageAppointment.TueAvailableAppoint = previousManageInfo.TueCapacity;
                    }
                    if (manageAppointment.WedCapacity == previousManageInfo.WedCapacity)
                    {
                        manageAppointment.WedAvailableAppoint = previousManageInfo.WedCapacity;
                    }
                    if (manageAppointment.ThuCapacity == previousManageInfo.ThuCapacity)
                    {
                        manageAppointment.ThuAvailableAppoint = previousManageInfo.ThuCapacity;
                    }
                    if (manageAppointment.FriCapacity == previousManageInfo.FriCapacity)
                    {
                        manageAppointment.FriAvailableAppoint =  previousManageInfo.FriCapacity;
                    }
                    manageAppointment.TotalAvailableAppoint = manageAppointment.SatAvailableAppoint + manageAppointment.SunAvailableAppoint + manageAppointment.MonAvailableAppoint + manageAppointment.TueAvailableAppoint + manageAppointment.WedAvailableAppoint + manageAppointment.ThuAvailableAppoint + manageAppointment.FriAvailableAppoint;
                }
                if (DateTime.Now.Date > manageAppointment.OrderExpireDate)
                {
                    TempData["ErrorMessage"] = "Your ordered appointments are expired. Please Buy Appointment and your remaining appointments will be added.";
                }
                else
                {
                    if (manageAppointment.TotalAvailableAppoint > manageAppointment.RemainingAppoint)
                    {
                        TempData["ErrorMessage"] = "Your appointment capacity limit exceeded! Please buy more appointment.";
                    }
                    else
                    {
                        string message = aDoctorManager.UpdateManageAppointment(manageAppointment);
                        if (message == "Success")
                        {
                            TempData["Message"] = "Manage Appointment updated successfully.";
                        }
                        else
                        {
                            TempData["ErrorMessage"] = message;
                        }
                    }
                }
            }
            return RedirectToAction("ManageAppointment", "Doctor");
        }

        public ActionResult BuyAppointment()
        {
            ViewBag.GetName = GetDoctorData().Name;
            List<ChambersForOrder> chambersForOrders = aDoctorManager.GetChambersForOrder(GetDoctorData().Id); 
            ViewBag.GetChambersForOrder = chambersForOrders;
            ViewBag.AppointPrice = 20;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuyAppointment(DoctorAppointOrder doctorAppointOrder)
        {
            if (User.IsInRole("Doctor"))
            {
                int remainingAppoint = aDoctorManager.GetRemainingAppoint(doctorAppointOrder.ManageAppointId);
                doctorAppointOrder.OrderDate = DateTime.Now.Date;
                doctorAppointOrder.ExpireDate = doctorAppointOrder.OrderDate.AddDays(28);
                doctorAppointOrder.RemainingAppoint = doctorAppointOrder.AppointAmount + remainingAppoint;
                doctorAppointOrder.TotalAppoint = doctorAppointOrder.RemainingAppoint;
                doctorAppointOrder.Expired = false;
                doctorAppointOrder.Status = "Pending";
                string message = aDoctorManager.SaveOrderAppointment(doctorAppointOrder);
                if (message == "Success")
                {
                    TempData["Message"] =
                        "Your Appointment order successfully done. Your order will be recharged in 20-90 minutes";
                    return RedirectToAction("Index", "Doctor");
                }
                else
                {
                    ViewBag.ErrorMessage = message;
                }
            }
            ViewBag.GetName = GetDoctorData().Name;
            List<ChambersForOrder> chambersForOrders = aDoctorManager.GetChambersForOrder(GetDoctorData().Id);
            ViewBag.GetChambersForOrder = chambersForOrders;
            ViewBag.AppointPrice = 20;
            return View();
        }

        public ActionResult DoctorProfile()
        {
            Doctors aDoctor = aDoctorManager.GetDoctorInfo(GetDoctorData().Id);
            List<DoctorsSpecialities> doctorsSpecialities = aDoctorManager.GetSpecialities();
            ViewBag.GetSpecialities = doctorsSpecialities;
            ViewBag.GetDoctorInfo = aDoctor;
            ViewBag.GetName = GetDoctorData().Name;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoctorProfile(Doctors aDoctor)
        {
            if (User.IsInRole("Doctor"))
            {
                if (aDoctor.PhoneNo == null)
                {
                    aDoctor.PhoneNo = "";
                }
                if (aDoctor.ImagePath == null)
                {
                    aDoctor.ImagePath = "";
                }
                else
                {
                    aDoctor.ImagePath = DoctorImage(aDoctor.ImageFile);
                }
                string message = aDoctorManager.EditDoctorProfile(aDoctor);
                if(message == "Success")
                {
                    ViewBag.Message = "Doctor info updated successfully";
                }
                else
                {
                    ViewBag.ErrorMessage = message;
                }
            }
            List<DoctorsSpecialities> doctorsSpecialities = aDoctorManager.GetSpecialities();
            ViewBag.GetSpecialities = doctorsSpecialities;
            aDoctor = aDoctorManager.GetDoctorInfo(GetDoctorData().Id);
            ViewBag.GetDoctorInfo = aDoctor;
            ViewBag.GetName = GetDoctorData().Name;
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
                        Bitmap target = new Bitmap(280, 350, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
                        string name = "doctor" + Guid.NewGuid() + ".png";
                        fileName = "~/Image/" + name;
                        var imageFile = Path.Combine(Server.MapPath("~/Image/"), name);
                        Graphics graphic = Graphics.FromImage(target);
                        graphic.DrawImage(image, 0, 0, 250, 320);
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
        public ActionResult ManageChamber()
        {
            ViewBag.Message = TempData["Message"];
            List<ViewDoctors> doctorsChambers = aDoctorManager.GetDoctorChambers(GetDoctorData().Id);
            List<DoctorChamber> doctorOwnChamber = aDoctorManager.GetDoctorOwnChamber(GetDoctorData().Id);
            ViewBag.DoctorChambers = doctorsChambers;
            ViewBag.DoctorOwnChambers = doctorOwnChamber;
            ViewBag.GetName = GetDoctorData().Name;
            ViewBag.GetDoctorId = GetDoctorData().Id;
            return View();
        }

        
        
        private void GenerateDoctorChamberSchedule(DoctorChamber aDoctorChamber)
        {
            if (aDoctorChamber.SatFromTime != null && aDoctorChamber.SatToTime != null)
            {
                aDoctorChamber.SatFromTime = aDoctorChamber.SatFromTime + " - " + aDoctorChamber.SatToTime;
            }
            if (aDoctorChamber.SunFromTime != null && aDoctorChamber.SunToTime != null)
            {
                aDoctorChamber.SunFromTime = aDoctorChamber.SunFromTime + " - " + aDoctorChamber.SunToTime;
            }
            if (aDoctorChamber.MonFromTime != null && aDoctorChamber.MonToTime != null)
            {
                aDoctorChamber.MonFromTime = aDoctorChamber.MonFromTime + " - " + aDoctorChamber.MonToTime;
            }
            if (aDoctorChamber.TueFromTime != null && aDoctorChamber.TueToTime != null)
            {
                aDoctorChamber.TueFromTime = aDoctorChamber.TueFromTime + " - " + aDoctorChamber.TueToTime;
            }
            if (aDoctorChamber.WedFromTime != null && aDoctorChamber.WedToTime != null)
            {
                aDoctorChamber.WedFromTime = aDoctorChamber.WedFromTime + " - " + aDoctorChamber.WedToTime;
            }
            if (aDoctorChamber.ThuFromTime != null && aDoctorChamber.ThuToTime != null)
            {
                aDoctorChamber.ThuFromTime = aDoctorChamber.ThuFromTime + " - " + aDoctorChamber.ThuToTime;
            }
            if (aDoctorChamber.FriFromTime != null && aDoctorChamber.FriToTime != null)
            {
                aDoctorChamber.FriFromTime = aDoctorChamber.FriFromTime + " - " + aDoctorChamber.FriToTime;
            }
            if (aDoctorChamber.SatFromTime == null && aDoctorChamber.SatToTime == null)
            {
                aDoctorChamber.SatFromTime = "";
                aDoctorChamber.SatToTime = "";
            }
            if (aDoctorChamber.SunFromTime == null && aDoctorChamber.SunToTime == null)
            {
                aDoctorChamber.SunFromTime = "";
                aDoctorChamber.SunToTime = "";
            }
            if (aDoctorChamber.MonFromTime == null && aDoctorChamber.MonToTime == null)
            {
                aDoctorChamber.MonFromTime = "";
                aDoctorChamber.MonToTime = "";
            }
            if (aDoctorChamber.TueFromTime == null && aDoctorChamber.TueToTime == null)
            {
                aDoctorChamber.TueFromTime = "";
                aDoctorChamber.TueToTime = "";
            }
            if (aDoctorChamber.WedFromTime == null && aDoctorChamber.WedToTime == null)
            {
                aDoctorChamber.WedFromTime = "";
                aDoctorChamber.WedToTime = "";
            }
            if (aDoctorChamber.ThuFromTime == null && aDoctorChamber.ThuToTime == null)
            {
                aDoctorChamber.ThuFromTime = "";
                aDoctorChamber.ThuToTime = "";
            }
            if (aDoctorChamber.FriFromTime == null && aDoctorChamber.FriToTime == null)
            {
                aDoctorChamber.FriFromTime = "";
                aDoctorChamber.FriToTime = "";
            }
        }
       
        public ActionResult DeleteDoctorChamber(int id)
        {
            string message = "";
            if (User.IsInRole("Doctor"))
            {
                message = aDoctorManager.DeleteDoctorChamber(id, GetDoctorData().Id);
                if (message == "Success")
                {
                    TempData["Message"] = "Chamber deleted successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = message;
                }
            }
            
            return RedirectToAction("ManageChamber", "Doctor");
        }
        public ActionResult DeleteMedicalChamber(int id)
        {
            string message = "";
            if (User.IsInRole("Doctor"))
            {
                message = aDoctorManager.DeleteMedicalChamber(id, GetDoctorData().Id);
                if (message == "Success")
                {
                    TempData["Message"] = "Medical chamber deleted successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = message;
                }
            }
            return RedirectToAction("ManageChamber", "Doctor");
        }

        public ActionResult AppointHistory()
        {
            List<BookAppointment> usersAppointments = aDoctorManager.GetAllUserAppointments(GetDoctorData().Id);
            ViewBag.GetAllAppointments = usersAppointments;
            ViewBag.GetName = GetDoctorData().Name;
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
            ViewBag.GetName = GetDoctorData().Name;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Forum(ForumPost forumPost)
        {
            if (User.IsInRole("Doctor"))
            {
                forumPost.UserId = 0;
                forumPost.DoctorId = GetDoctorData().Id;
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
                string message = aDoctorManager.SavePost(forumPost, countTopicCategory);
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
            ViewBag.GetName = GetDoctorData().Name;
            return View();
        }
        public ActionResult Comment(string comment, int postId)
        {
            if (User.IsInRole("Doctor"))
            {
                Comments aComment = new Comments();
                aComment.PostId = postId;
                aComment.Comment = comment;
                aComment.UserId = 0;
                aComment.DoctorId = GetDoctorData().Id;
                aComment.MedicalId = 0;
                string message = aDoctorManager.SaveComment(aComment);
                if (message != "Success")
                {
                    TempData["ErrorMessage"] = message;
                }
            }
            return RedirectToAction("Forum", "Doctor");
        }
        public ActionResult AllForumPosts()
        {
            List<ForumPost> forumPosts = aHomeManager.GetAllForumPosts();
            ViewBag.GetAllForumPosts = forumPosts;
            ViewBag.GetName = GetDoctorData().Name;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AllForumPosts(string comment, int postId)
        {
            if (User.IsInRole("Doctor"))
            {
                Comments aComment = new Comments();
                aComment.PostId = postId;
                aComment.Comment = comment;
                aComment.UserId = 0;
                aComment.DoctorId = GetDoctorData().Id;
                aComment.MedicalId = 0;
                string message = aDoctorManager.SaveComment(aComment);
                if (message != "Success")
                {
                    ViewBag.ErrorMessage = message;
                }
            }
            List<ForumPost> forumPosts = aHomeManager.GetAllForumPosts();
            ViewBag.GetAllForumPosts = forumPosts;
            ViewBag.GetName = GetDoctorData().Name;
            return View();
        }
        public ActionResult ForumTopicPost(int topicCategoryId)
        {
            if (topicCategoryId > 0)
            {
                List<ForumPost> forumPosts = aHomeManager.GetFormTopicPost(topicCategoryId);
                ViewBag.GetFormTopicPost = forumPosts;
            }
            ViewBag.GetName = GetDoctorData().Name;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForumTopicPost(string comment, int postId, int categoryId)
        {
            if (User.IsInRole("Doctor"))
            {
                Comments aComment = new Comments();
                aComment.PostId = postId;
                aComment.Comment = comment;
                aComment.UserId = 0;
                aComment.DoctorId = GetDoctorData().Id;
                aComment.MedicalId = 0;
                string message = aDoctorManager.SaveComment(aComment);
                if (message != "Success")
                {
                    ViewBag.ErrorMessage = message;
                }
            }
            List<ForumPost> forumPosts = aHomeManager.GetFormTopicPost(categoryId);
            ViewBag.GetFormTopicPost = forumPosts;
            ViewBag.GetName = GetDoctorData().Name;
            return View();
        }

        public ActionResult CancelUserAppointment(int id, int chamberId, int userId, string mobileNo)
        {
            if (User.IsInRole("Doctor"))
            {
                if (id != 0 && userId != 0 && mobileNo != null)
                {
                    int doctorId = GetDoctorData().Id;
                    string message = aDoctorManager.CancelUserAppointment(id, doctorId, userId, mobileNo);
                    if (message == "Success")
                    {
                        BookAppointment bookAppointInfo = new BookAppointment();
                        bookAppointInfo.BookAppointId = id;
                        bookAppointInfo.DoctorId = doctorId;
                        bookAppointInfo.ChamberId = chamberId;
                        bookAppointInfo.UserId = userId;
                        SendCencelMessageToUser(mobileNo, bookAppointInfo);
                        TempData["Message"] = "Appointment canceled successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = message;
                    }
                }
            }
            return RedirectToAction("Index", "Doctor");
        }
        private void SendCencelMessageToUser(string mobileNo, BookAppointment bookAppointment)
        {
            BookAppointment bookAppointInfo = aMainAdminManager.GetBookAppointInfo(bookAppointment);
            string sendMessage = "Sorry! Your Appointment Booking has been cenceled for " + bookAppointInfo.DoctorName + ". Appointment Date : " + bookAppointInfo.AppointDate + ", Schedule : " + bookAppointInfo.Schedule + " and Your Serial NO. : " + bookAppointInfo.SerialNo + ". Thank you for using HSSMI service.";
            SendSms sendSms = new SendSms();
            sendSms.SendMessage(mobileNo, sendMessage);
        }
    }
}