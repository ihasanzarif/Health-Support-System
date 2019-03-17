using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using HealthSupportApp.Gateway.MedicalGateway;
using HealthSupportApp.Manager;
using HealthSupportApp.Manager.DoctorManager;
using HealthSupportApp.Manager.HomeManager;
using HealthSupportApp.Manager.MedicalManager;
using HealthSupportApp.Manager.UserManager;
using HealthSupportApp.Models;
using HealthSupportApp.Models.HomeModel;
using HealthSupportApp.Models.MedicalModel;
using HealthSupportApp.Models.UserModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Login = HealthSupportApp.Models.HomeModel.Login;
using ChangePassword = HealthSupportApp.Models.DoctorModel.ChangePassword;

namespace HealthSupportApp.Controllers
{
    public class RegisterController : Controller
    {

        UserManager aUserManager = new UserManager();
        MedicalManager aMedicalManager = new MedicalManager();
        DoctorManager aDoctorManager = new DoctorManager();
        HomeManager aHomeManager = new HomeManager();
        MainAdminManager aMainAdminManager = new MainAdminManager();
        public ActionResult Register()
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
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Register(UserAccount aUserAccount)
        {
            if (aUserAccount.WantToGiveBlood == false)
            {
                ModelState.Remove("Location");
                ModelState.Remove("BloodGroup");
            }
            if (ModelState.IsValid)
            {
                if (aUserAccount.Email == null)
                {
                    aUserAccount.Email = "";
                }
                if (aUserAccount.Gender == null)
                {
                    aUserAccount.Gender = "";
                }
                if (aUserAccount.Location == null)
                {
                    aUserAccount.Location = "";
                }
                if (aUserAccount.BloodGroup == null)
                {
                    aUserAccount.BloodGroup = "";
                }
                if (aUserAccount.Age == null)
                {
                    aUserAccount.Age = "";
                }
                aUserAccount.Role = "User";
                aUserAccount.Password = Crypto.Hash(aUserAccount.Password);
                aUserAccount.ConfirmPassword = Crypto.Hash(aUserAccount.ConfirmPassword);
                string message = aUserManager.Save(aUserAccount);
                ModelState.Clear();
                if (message == "Success")
                {
                    TempData["Message"] = "Your Registration is successfully completed.";
                    return RedirectToAction("Login");
                }
                ViewBag.ErrorMessage = message;
            }
           
            return View();
        }

        public ActionResult MedicalRegister()
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult MedicalRegister(MedicalAccount aMedicalAccount)
        {
            string verifying = null;
            if (ModelState.IsValid)
            {
                if (aMedicalAccount.MedicalContactNo2 == null)
                {
                    aMedicalAccount.MedicalContactNo2 = "";
                }
                if (aMedicalAccount.AmbulanceContact == null)
                {
                    aMedicalAccount.AmbulanceContact = "";
                }
                aMedicalAccount.Role = "Medical";
                aMedicalAccount.Password = Crypto.Hash(aMedicalAccount.Password);
                aMedicalAccount.ActivationCode = Guid.NewGuid();
                aMedicalAccount.IsEmailVerified = true;
                aMedicalAccount.Status = "Active";
                aMedicalAccount.AccountCreatedDate = DateTime.Now.ToLongDateString();
                verifying = aMedicalManager.Save(aMedicalAccount);
            }
            if (verifying == "Success")
            {
                //SendEmailVerificationLink(aMedicalAccount.MedicalEmail, aMedicalAccount.ActivationCode.ToString());
                ViewBag.Message = "Account request successfully completed. An activation link has been sent to your email. Please check your email to verified the account";
                ViewBag.Status = true;
            }
            else
            {
                ViewBag.Message = verifying;
                ViewBag.Status = false;
            }
            return View();
        }
        private void SendEmailVerificationLink(string medicalEmail, string activationCode)
        {
            var verifyUrl = "/Register/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEmail = new MailAddress("zattrif0000@gmail.com", "Zarfi Records");
            var toEmail = new MailAddress(medicalEmail);
            var fromEmailPassword = "01715740302";

            string subject = "Your account rquest successfully sent!";
            string body = "<br/><br/>Thank you for sending request. We have recieved your request. Please click on the below link to verify your account." + "<br/><br/><a href='" + link + "'>" + link + "</a>";
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };
            using (MailMessage message = new MailMessage(fromEmail, toEmail))
            {
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                smtp.Send(message);
            }
        }
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool isValid = aMedicalManager.IsActivationCodeValid(id);
            if (isValid)
            {
                bool isEmailVerified = true;
                bool updateEmailVerification = aMedicalManager.UpdateEmailVerification(isEmailVerified, id);
                if (updateEmailVerification)
                {
                    ViewBag.Message = "Your account has been successfully verified by email. Your account will activate in 12-24 hours.";
                    ViewBag.Status = true;
                }
            }
            else
            {
                ViewBag.Message = "Invalid email verification request. Please check your email and verify your account!";
                ViewBag.Status = false;
            }
            return View();
        }

        public ActionResult DoctorRegister()
        {
            List<DoctorsSpecialities> doctorsSpecialities = aDoctorManager.GetSpecialities();
            ViewBag.GetSpecialities = doctorsSpecialities;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoctorRegister(Doctors aDoctor)
        {
            if (ModelState.IsValid)
            {
                if (aDoctor.PhoneNo == null)
                {
                    aDoctor.PhoneNo = "";
                }
                Random rand = new Random();
                aDoctor.Password = rand.Next(0, 999999).ToString();
                string password = aDoctor.Password;
                aDoctor.Password = Crypto.Hash(aDoctor.Password);
                aDoctor.ImagePath = DoctorImage(aDoctor.ImageFile);
                aDoctor.Role = "Doctor";
                aDoctor.PasswordVerified = false;
                aDoctor.Status = "Active";
                aDoctor.AccountCreatedDate = DateTime.Now.ToLongDateString();
                string message = aDoctorManager.SaveDoctor(aDoctor);
                if (message == "Success")
                {
                    message = "Hello Doctor! Your registration successfully completed. Please login with your temporary password which has been sent to your mobile number.";
                    //SendEmailToDoctor(aDoctorAssign); //Send email to doctor
                    SendMessageToDoctor(aDoctor.MobileNo, password); //Send sms to doctor mobile no.
                    TempData["Message"] = message;
                    return RedirectToAction("Login");
                }
                else if (message == "Warning")
                {
                    message =
                        "Your account already created by a Hospital/Diagnostic center and a confirmation message has been sent to your number. Please check the message to get your Login ID & Password.";
                    TempData["WarningMessage"] = message;
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.ErrorMessage = message;
                }
            }
            List<DoctorsSpecialities> doctorsSpecialities = aDoctorManager.GetSpecialities();
            ViewBag.GetSpecialities = doctorsSpecialities;
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
                        Bitmap target = new Bitmap(250, 320, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
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
        private void SendMessageToDoctor(string mobileNo, string password)
        {
            string sendMessage = "Congrates! Your HSSMI account has been created. Your Login ID : " + mobileNo + " and Your password : " + password;
            SendSms sendSms = new SendSms();
            sendSms.SendMessage(mobileNo, sendMessage);
        }

       
        public ActionResult Login()
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
            ViewBag.Message = TempData["Message"];
            ViewBag.WarningMessage = TempData["WarningMessage"];
            ViewBag.InfoMessage = TempData["InfoMessage"];
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Login(Login aLogin, string returnUrl="")
        {
            if (ModelState.IsValid)
            {
                aLogin.Password = Crypto.Hash(aLogin.Password);
                //Check User Login
                if (aUserManager.IsValid(aLogin.LoginId, aLogin.Password))
                {
                    int timeout = aLogin.RememberMe ? 525600 : 60; // 525600 min = 1year
                    var ticket = new FormsAuthenticationTicket(aLogin.LoginId, aLogin.RememberMe, timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    cookie.Expires = DateTime.Now.AddMinutes(timeout);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "User");
                    }
                }
                //Check Doctor Login
                else if (aDoctorManager.IsValid(aLogin.LoginId, aLogin.Password))
                {
                    Doctors doctor = aDoctorManager.IsLoginVerified(aLogin.LoginId);
                    if (doctor.PasswordVerified)
                    {
                        if(doctor.Status == "Active")
                        {
                            int timeout = aLogin.RememberMe ? 525600 : 60; // 525600 min = 1year
                            var ticket = new FormsAuthenticationTicket(aLogin.LoginId, aLogin.RememberMe, timeout);
                            string encrypted = FormsAuthentication.Encrypt(ticket);
                            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                            cookie.Expires = DateTime.Now.AddMinutes(timeout);
                            cookie.HttpOnly = true;
                            Response.Cookies.Add(cookie);

                            if (Url.IsLocalUrl(returnUrl))
                            {
                                return Redirect(returnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Doctor");
                            }
                        }
                        else
                        {
                            ViewBag.AccountWarningMessage = "Your account has been suspended. Please contact us to activate your account.";
                        }
                    }
                    else
                    {
                        TempData["WarningMessage"] = "Please change your temporary password";
                        ChangePassword aChangePassword = new ChangePassword();
                        aChangePassword.DoctorLoginId = aLogin.LoginId;
                        aChangePassword.OldPassword = aLogin.Password;
                        Session["UserLoginId"] = aChangePassword;
                        return RedirectToAction("ChangeTemporaryPassword", "Register");
                    }
                }
                //Check Medical Login
                else if (aMedicalManager.IsValid(aLogin.LoginId, aLogin.Password))
                {
                    MedicalAccount medicalAccount = aMedicalManager.IsMedicalLoginVerified(aLogin.LoginId);
                    if (medicalAccount.IsEmailVerified)
                    {
                        if (medicalAccount.Status == "Active")
                        {
                            int timeout = aLogin.RememberMe ? 525600 : 60; // 525600 min = 1year
                            var ticket = new FormsAuthenticationTicket(aLogin.LoginId, aLogin.RememberMe, timeout);
                            string encrypted = FormsAuthentication.Encrypt(ticket);
                            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                            cookie.Expires = DateTime.Now.AddMinutes(timeout);
                            cookie.HttpOnly = true;
                            Response.Cookies.Add(cookie);

                            if (Url.IsLocalUrl(returnUrl))
                            {
                                return Redirect(returnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Medical");
                            }
                        }
                        else if(medicalAccount.Status == "Pending")
                        {
                            ViewBag.AccountWarningMessage = "This account request is pending. Please contact us if you want to activate.";
                        }
                        else
                        {
                            ViewBag.AccountWarningMessage = "This account has been suspended. Please contact us to activate the account.";
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Your email has not verified yet. Please check your email and verified your account";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Your Email or Password is incorrect!";
                }
            }
            return View();
        }
        public ActionResult ChangeTemporaryPassword()
        {
            ViewBag.WarningMessage = TempData["WarningMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeTemporaryPassword(ChangePassword changePassword,string returnUrl)
        {
            ChangePassword aChangePassword = Session["UserLoginId"] as ChangePassword;
            if (aChangePassword != null)
            {
                if (aDoctorManager.IsValid(aChangePassword.DoctorLoginId, aChangePassword.OldPassword))
                {
                    changePassword.DoctorLoginId = aChangePassword.DoctorLoginId;
                    changePassword.NewPassword = Crypto.Hash(changePassword.NewPassword);
                    changePassword.PasswordVerified = true;
                    string message = aDoctorManager.ChangeTemporaryPassword(changePassword);
                    if (message == "Success")
                    {
                        bool rememberMe = false;
                        int timeout = rememberMe ? 525600 : 60; // 525600 min = 1year
                        var ticket = new FormsAuthenticationTicket(aChangePassword.DoctorLoginId, rememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            Session["UserLoginId"] = null;
                            TempData["ChangeTempPassMsg"] = "Your temporary password changed successfully.";
                            return RedirectToAction("Index", "Doctor");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = message;
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Register");
                }
            }
            else
            {
                return RedirectToAction("Login", "Register");
            }
        }

        public ActionResult ForgetPassword()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgetPassword(ForgetPassword aForgetPassword)
        {
            Session["UserForgetPassword"] = null;
            Session["HospitalForgetPassword"] = null;
            if (aForgetPassword.LoginId != null)
            {
                ForgetPassword userForgetPassword = aHomeManager.GetForgetPasswordUser(aForgetPassword);
                if (userForgetPassword !=null)
                {
                    userForgetPassword.LoginId = aForgetPassword.LoginId;
                    Session["UserForgetPassword"] = userForgetPassword;
                    return RedirectToAction("GetVerification", "Register");
                }
                else
                {
                    TempData["ErrorMessage"] = "Not found anyone with this Login ID.";
                    return RedirectToAction("ForgetPassword", "Register");
                }
            }
            if (aForgetPassword.ContactPersonMobileNo != null)
            {
                ForgetPassword hospitalForgetPassword = aHomeManager.GetForgetPasswordHospital(aForgetPassword);
                if (hospitalForgetPassword != null)
                {
                    hospitalForgetPassword.ContactPersonMobileNo = aForgetPassword.ContactPersonMobileNo;
                    Session["HospitalForgetPassword"] = hospitalForgetPassword;
                    return RedirectToAction("GetVerification", "Register");
                }
                else
                {
                    TempData["ErrorMessage"] = "Not found anyone with this Login ID.";
                    return RedirectToAction("ForgetPassword", "Register");
                }
            }
            return View();
        }
        public ActionResult GetVerification()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            ViewBag.UserForgetPassword = Session["UserForgetPassword"];
            ViewBag.HospitalForgetPassword = Session["HospitalForgetPassword"];
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetVerification(ForgetPassword aForgetPassword)
        {
            Session["ValidUser"] = null;
            if (aForgetPassword.LoginId != null)
            {
                if (aHomeManager.IsUserVerificationValid(aForgetPassword))
                {
                    Session["ValidUser"] = aForgetPassword;
                    return RedirectToAction("ResetPassword", "Register");
                }
            }
            if (aForgetPassword.ContactPersonMobileNo != null)
            {
                if (aHomeManager.IsMedicalVerificationValid(aForgetPassword))
                {
                    Session["ValidUser"] = aForgetPassword;
                    return RedirectToAction("ResetPassword", "Register");
                }
            }
            ViewBag.ErrorMessage = "Your given verification code is incorrect. Please try again!";
            return View();
        }
        public ActionResult SendVerificationCode(string id)
        {
            if (id != null)
            {
                string cantactPersonMobileNo = "";
                string mobileNo = id;
                Random rand = new Random();
                int verificationCode = rand.Next(0,999999);
                string message = aHomeManager.SaveVerificationCode(mobileNo, cantactPersonMobileNo, verificationCode);
                if (message == "Success")
                {
                    SendVerificationMessage(mobileNo, verificationCode);
                    TempData["Message"] = "A verification code has been sent to your mobile number.";
                }
                else
                {
                    TempData["ErrorMessage"] = message;
                }
            }
            return RedirectToAction("GetVerification", "Register");
        }
        public ActionResult SendVerificationCodeHospital(string id)
        {
            if (id != null)
            {
                string mobileNo = "";
                string cantactPersonMobileNo = id;
                Random rand = new Random();
                int verificationCode = rand.Next(0, 999999);
                string message = aHomeManager.SaveVerificationCode(mobileNo, cantactPersonMobileNo, verificationCode);
                if (message == "Success")
                {
                    SendVerificationMessage(cantactPersonMobileNo, verificationCode);
                    TempData["Message"] = "A verification code has been sent to your mobile number.";
                }
                else
                {
                    TempData["ErrorMessage"] = message;
                }
            }
            return RedirectToAction("GetVerification", "Register");
        }
        private void SendVerificationMessage(string mobileNo, int verificationCode)
        {
            string sendMessage = "You have got a verification message from HSSMI. Verification code : " + verificationCode;
            SendSms sendSms = new SendSms();
            sendSms.SendMessage(mobileNo, sendMessage);
        }

        public ActionResult ResetPassword()
        {
            if (Session["ValidUser"] != null)
            {
                ViewBag.GetUserForgetPassword = Session["ValidUser"];
                return View();
            }
            return RedirectToAction("ForgetPassword", "Register");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ForgetPassword aForgetPassword)
        {
            if (Session["ValidUser"] != null)
            {
                if (aForgetPassword.LoginId != null && aForgetPassword.Role == "User")
                {
                    aForgetPassword.NewPassword = Crypto.Hash(aForgetPassword.NewPassword);
                    string message = aHomeManager.ChangeUserPassword(aForgetPassword);
                    if (message == "Success")
                    {
                        Session["ValidUser"] = null;
                        TempData["Message"] = "Hi! Your password has changed successfully. Please Login now!";
                        return RedirectToAction("Login", "Register");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = message;
                    }
                }
                if (aForgetPassword.LoginId != null && aForgetPassword.Role == "Doctor")
                {
                    aForgetPassword.NewPassword = Crypto.Hash(aForgetPassword.NewPassword);
                    string message = aHomeManager.ChangeDoctorPassword(aForgetPassword);
                    if (message == "Success")
                    {
                        Session["ValidUser"] = null;
                        TempData["Message"] = "Hello Doctor! Your password has changed successfully. Please Login now!";
                        return RedirectToAction("Login", "Register");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = message;
                    }
                }
                if (aForgetPassword.ContactPersonMobileNo != null)
                {
                    aForgetPassword.NewPassword = Crypto.Hash(aForgetPassword.NewPassword);
                    string message = aHomeManager.ChangeMedicalPassword(aForgetPassword);
                    if (message == "Success")
                    {
                        Session["ValidUser"] = null;
                        TempData["Message"] = "Your password has changed successfully. Please Login now!";
                        return RedirectToAction("Login", "Register");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = message;
                    }
                }
                ViewBag.ErrorMessage = "Something went wrong. Please try again!";
                return View();
            }
            return RedirectToAction("ForgetPassword", "Register");
        }

        [Authorize(Roles = "User")]
        public ActionResult UserLogout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Register");
        }
        [Authorize(Roles = "Medical")]
        public ActionResult MedicalLogout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Register");
        }
        [Authorize(Roles = "Doctor")]
        public ActionResult DoctorLogout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Register");
        }

        //public ActionResult MainAdminRegister()
        //{
        //    if (User.IsInRole("Medical"))
        //    {
        //        return RedirectToAction("Index", "Medical");
        //    }
        //    if (User.IsInRole("User"))
        //    {
        //        return RedirectToAction("Index", "User");
        //    }
        //    if (User.IsInRole("Doctor"))
        //    {
        //        return RedirectToAction("Index", "Doctor");
        //    }
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult MainAdminRegister(MainAdmin mainAdminRegister)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        mainAdminRegister.Password = Crypto.Hash(mainAdminRegister.Password);
        //        string password = mainAdminRegister.MainAdminPassword;
        //        mainAdminRegister.MainAdminPassword = Crypto.Hash(mainAdminRegister.MainAdminPassword);
        //        mainAdminRegister.Role = "MainAdmin";
        //        string message = aHomeManager.SaveAdmin(mainAdminRegister, password);
        //        if (message == "Success")
        //        {
        //            TempData["Message"] = "Admin account created successfully done! Please Login now.";
        //            return RedirectToAction("MainAdminLogin", "Register");
        //        }
        //        else
        //        {
        //            ViewBag.ErrorMessage = message;
        //        }
        //    }
        //    return View();
        //}
        //public ActionResult MainAdminLogin()
        //{
        //    ViewBag.Message = TempData["Message"];
        //    if (User.IsInRole("Medical"))
        //    {
        //        return RedirectToAction("Index", "Medical");
        //    }
        //    if (User.IsInRole("User"))
        //    {
        //        return RedirectToAction("Index", "User");
        //    }
        //    if (User.IsInRole("Doctor"))
        //    {
        //        return RedirectToAction("Index", "Doctor");
        //    }
            
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult MainAdminLogin(MainAdmin mainAdminLogin, string returnUrl = "")
        //{
        //    ModelState.Remove("Name");
        //    ModelState.Remove("MobileNo");
        //    ModelState.Remove("ConfirmPassword");
        //    ModelState.Remove("MainAdminPassword");
        //    if (ModelState.IsValid)
        //    {
        //        mainAdminLogin.Password = Crypto.Hash(mainAdminLogin.Password);
        //        //Check User Login
        //        if (aMainAdminManager.IsAdminLoginValid(mainAdminLogin.Username, mainAdminLogin.Password))
        //        {
        //            int timeout = mainAdminLogin.RememberMe ? 525600 : 60; // 525600 min = 1year
        //            var ticket = new FormsAuthenticationTicket(mainAdminLogin.Username, mainAdminLogin.RememberMe, timeout);
        //            string encrypted = FormsAuthentication.Encrypt(ticket);
        //            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
        //            cookie.Expires = DateTime.Now.AddMinutes(timeout);
        //            cookie.HttpOnly = true;
        //            Response.Cookies.Add(cookie);

        //            if (Url.IsLocalUrl(returnUrl))
        //            {
        //                return Redirect(returnUrl);
        //            }
        //            else
        //            {
        //                return RedirectToAction("Index", "MainAdmin");
        //            }
        //        }
        //        else
        //        {
        //            ViewBag.ErrorMessage = "Your Username or Password is incorrect! Please try again.";
        //        }
        //    }
        //    return View();
        //}

        //public ActionResult MainAdminLogout()
        //{
        //    FormsAuthentication.SignOut();
        //    return RedirectToAction("Index", "Home");
        //}
    }
}