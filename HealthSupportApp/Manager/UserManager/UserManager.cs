using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HealthSupportApp.Gateway;
using HealthSupportApp.Gateway.DoctorGateway;
using HealthSupportApp.Gateway.UserGateway;
using HealthSupportApp.Models;
using HealthSupportApp.Models.HomeModel;
using HealthSupportApp.Models.MedicalModel;
using HealthSupportApp.Models.UserModel;

namespace HealthSupportApp.Manager.UserManager
{
    public class UserManager
    {
        UserGateway aUserGateway = new UserGateway();
        DoctorGateway aDoctorGateway = new DoctorGateway();
        public string Save(UserAccount aUserAccount)
        {
            if (aUserGateway.IsEmailExists(aUserAccount.Email) == false)
            {
                if (aUserGateway.IsMobileNoExists(aUserAccount.MobileNo) == false && aDoctorGateway.IsDoctorMobileNoExists(aUserAccount.MobileNo) == false)
                {
                    int rowAffected = aUserGateway.Save(aUserAccount);
                    if (rowAffected > 0)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "Your Registration is not Successfull!";
                    }
                }
                else
                {
                    return "The mobile number is already exist. Please give valid mobile number.";
                }

            }
            else
            {
                return "The Email is already exists. Please, Give another Email.";
            }
        }

        public bool IsValid(string mobileNo, string password)
        {
            return aUserGateway.IsValid(mobileNo, password);
        }

        public UserAccount GetUserData(string mobileNo)
        {
            return aUserGateway.GetUserData(mobileNo);
        }

        public string EditUserProfile(UserAccount aUserAccount)
        {
            int rowAffected = aUserGateway.EditUserProfile(aUserAccount);
            if (rowAffected > 0)
            {
                return "Success";
            }

            return "Your profile updating failed";
        }
        public List<MedicalAccount> GetHospitalSearchResult(SearchModel aSearchModel)
        {
            string keyword = " ";
            if (aSearchModel.SearchString == null || aSearchModel.SearchString.Contains("medi") || aSearchModel.SearchString.Contains("Medi") || aSearchModel.SearchString.Contains("MEDI") || aSearchModel.SearchString.Contains("ambul") || aSearchModel.SearchString.Contains("Ambul") || aSearchModel.SearchString.Contains("AMBUL"))
            {
                keyword = "Medical";
            }
            return aUserGateway.GetMedicalSearchResult(aSearchModel, keyword);
        }

        public List<Doctors> GetDoctorSearchResult(SearchModel aSearchModel)
        {
            List<int> doctorsId = new List<int>();
            List<Doctors> doctors = new List<Doctors>();
            if (aSearchModel.SearchString == null || aSearchModel.SearchString.Contains("doct") || aSearchModel.SearchString.Contains("Doct") || aSearchModel.SearchString.Contains("DOCT") || aSearchModel.SearchString.Contains("dact") || aSearchModel.SearchString.Contains("Dact") || aSearchModel.SearchString.Contains("all") || aSearchModel.SearchString.Contains("All"))
            {
                doctorsId = aUserGateway.GetAllDoctorSearchResult(aSearchModel.Location);
                doctors = aUserGateway.GetDoctorsSearchResult(doctorsId);
                return doctors;
            }
            doctorsId = aUserGateway.GetDoctorsIdSearchResult(aSearchModel);
            doctors = aUserGateway.GetDoctorsSearchResult(doctorsId);
            return doctors;
        }

        public string ChangeUserPassword(ChangeUserPassword changeUserPassword)
        {
            bool validOldPassword = aUserGateway.IsUserOldPasswordValid(changeUserPassword.UserId, changeUserPassword.OldPassword);
            if (validOldPassword)
            {
                int rowAffected = aUserGateway.ChangeUserPassword(changeUserPassword);
                if (rowAffected > 0)
                {
                    return "Success";
                }
                else
                {
                    return "Your password changing failed.";
                }
            }
            else
            {
                return "Your given old password is wrong. Please try again!";
            }
        }

        public Doctors GetDoctorDetailsSearchResult(int id)
        {
            return aUserGateway.GetDoctorDetailsSearchResult(id);
        }

        public BookAppointment GetMedicalChamberForAppoint(int doctorId, int? medicalId)
        {
            return aUserGateway.GetMedicalChamberForAppoint(doctorId, medicalId);
        }

        public BookAppointment GetDoctorChamberForAppoint(int doctorId, int? doctorChmaberId)
        {
            return aUserGateway.GetDoctorChamberForAppoint(doctorId, doctorChmaberId);
        }

        public string ConfirmBookAppointment(BookAppointment bookAppointment)
        {
            int rowAffected = aUserGateway.ConfirmBookAppointment(bookAppointment);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Your appoint booking confirmation failed.";
            }
        }

        public List<BookAppointment> GetUserAppointments(int userId)
        {
            return aUserGateway.GetUserAppointments(userId);
        }

        public List<BookAppointment> GetUserAllAppointments(int userId)
        {
            return aUserGateway.GetUserAllAppointments(userId);
        }

        public string SavePost(ForumPost forumPost, int countTopicCategory)
        {
            int rowAffected = aUserGateway.SavePost(forumPost, countTopicCategory);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Your post publishing failed. Try again!";
            }
        }

        public string SaveComment(Comments comment)
        {
            int rowAffected = aUserGateway.SaveComment(comment);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Your comment publishing failed. Try again!";
            }
        }

        public UserAccount GetBloodDonateStatus(int userId)
        {
            return aUserGateway.GetBloodDonateStatus(userId);
        }

        public string UpdateBloodDonateStatus(UserAccount userAccount)
        {
            int rowAffected = aUserGateway.UpdateBloodDonateStatus(userAccount);
            if (rowAffected > 0)
            {
                return "Success";
            }
            return "Blood donate status updating failed. Try again!";
        }
    }
}