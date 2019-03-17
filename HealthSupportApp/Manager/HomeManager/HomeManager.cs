using System;
using System.Collections.Generic;
using HealthSupportApp.Gateway;
using HealthSupportApp.Gateway.MedicalGateway;
using HealthSupportApp.Models.HomeModel;
using HealthSupportApp.Models.MedicalModel;
using HealthSupportApp.Models.UserModel;

namespace HealthSupportApp.Manager.HomeManager
{
    public class HomeManager
    {
        HomeGateway aHomeGateway = new HomeGateway();
        MedicalGateway aMedicalGateway = new MedicalGateway();
        public string GetRole(string username)
        {
            string role = aHomeGateway.GetMedicalRole(username);
            if (role == "")
            {
                role = aHomeGateway.GetUserRole(username);
                if (role == "")
                {
                    role = aHomeGateway.GetDoctorRole(username);
                    if (role == "")
                    {
                        role = aHomeGateway.GetAdminRole(username);
                        return role;
                    }
                    return role;
                }
                return role;
            }
            return role;
        }

        public List<MedicalAccount> GetHospitalSearchResult(SearchModel aSearchModel)
        {
            string keyword = "";
            if (aSearchModel.SearchString == null || aSearchModel.SearchString.Contains("medi") || aSearchModel.SearchString.Contains("Medi") || aSearchModel.SearchString.Contains("MEDI") || aSearchModel.SearchString.Contains("ambul") || aSearchModel.SearchString.Contains("Ambul") || aSearchModel.SearchString.Contains("AMBUL"))
            {
                keyword = "Medical";
            }
            return aHomeGateway.GetMedicalSearchResult(aSearchModel, keyword);
        }

        public List<Doctors> GetDoctorSearchResult(SearchModel aSearchModel)
        {
            List<int> doctorsId = new List<int>();
            List<Doctors> doctors = new List<Doctors>();
            if (aSearchModel.SearchString == null || aSearchModel.SearchString.Contains("doct") || aSearchModel.SearchString.Contains("Doct") || aSearchModel.SearchString.Contains("DOCT") || aSearchModel.SearchString.Contains("dact") || aSearchModel.SearchString.Contains("Dact"))
            {
                doctorsId = aHomeGateway.GetAllDoctorSearchResult(aSearchModel.Location);
                doctors = aHomeGateway.GetDoctorsSearchResult(doctorsId);
                return doctors;
            }
            doctorsId = aHomeGateway.GetDoctorsIdSearchResult(aSearchModel);
            doctors = aHomeGateway.GetDoctorsSearchResult(doctorsId);
            return doctors;
        }

        public MedicalAccount GetMedicalSearchViewInfo(int medicalId)
        {
            MedicalAccount aMedicalAccount = aHomeGateway.GetMedicalSearchViewInfo(medicalId);
            aMedicalAccount.ViewDoctors = aMedicalGateway.GetAllDoctors(medicalId);
            aMedicalAccount.HospitalSerives = aMedicalGateway.GetHospitalServices(medicalId);
            aMedicalAccount.DiagnosticServices = aMedicalGateway.GetDiagnosticServices(medicalId);
            aMedicalAccount.OtherServices = aMedicalGateway.GetOtherServices(medicalId);
            aMedicalAccount.MedicalFacilities = aMedicalGateway.GetMedicalFacilities(medicalId);
            aMedicalAccount.Consultants = aMedicalGateway.GetMedicalConsultants(medicalId);
            aMedicalAccount.MedicalService = aMedicalGateway.GetEmergencyDetails(medicalId);
            return aMedicalAccount;
        }

        public ForgetPassword GetForgetPasswordUser(ForgetPassword aForgetPassword)
        {
            return aHomeGateway.GetForgetPasswordUser(aForgetPassword);
        }

        public ForgetPassword GetForgetPasswordHospital(ForgetPassword aForgetPassword)
        {
            return aHomeGateway.GetForgetPasswordHospital(aForgetPassword);
        }

        public string SaveVerificationCode(string mobileNo, string cantactPersonMobileNo, int verificationCode)
        {
            int rowAffected = aHomeGateway.SaveVerificationCode(mobileNo, cantactPersonMobileNo, verificationCode);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Verification process failed. Try again!";
            }
        }
        
        public bool IsUserVerificationValid(ForgetPassword aForgetPassword)
        {
            return aHomeGateway.IsUserVerificationValid(aForgetPassword);
        }

        public bool IsMedicalVerificationValid(ForgetPassword aForgetPassword)
        {
            return aHomeGateway.IsMedicalVerificationValid(aForgetPassword);
        }
       
        public string ChangeUserPassword(ForgetPassword aForgetPassword)
        {
            int rowAffected = aHomeGateway.ChangeUserPassword(aForgetPassword);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Your passowrd changing failed. Try again!";
            }
        }

        public string ChangeDoctorPassword(ForgetPassword aForgetPassword)
        {
            int rowAffected = aHomeGateway.ChangeDoctorPassword(aForgetPassword);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Your passowrd changing failed. Try again!";
            }
        }

        public string ChangeMedicalPassword(ForgetPassword aForgetPassword)
        {
            int rowAffected = aHomeGateway.ChangeMedicalPassword(aForgetPassword);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Your passowrd changing failed. Try again!";
            }
        }
        public Doctors GetDoctorDetailsSearchResult(int id)
        {
            return aHomeGateway.GetDoctorDetailsSearchResult(id);
        }

        public string SaveAdmin(MainAdmin mainAdminRegister, string password)
        {
            if (aHomeGateway.IsMainAdminPasswordValid(mainAdminRegister.MainAdminPassword, password))
            {
                if (aHomeGateway.IsUsernameExist(mainAdminRegister.Username) == false)
                {
                    int rowAffected = aHomeGateway.SavAdmin(mainAdminRegister);
                    if (rowAffected > 0)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "Main Admin account creating failed! Please try again.";
                    }
                }
                else
                {
                    return "Username is already exist! Please try with another.";
                }
            }
            else
            {
                return "Main Admin password is wrong! Please try again";
            }
        }

        public List<ForumPost> GetAllForumPosts()
        {
            return aHomeGateway.GetAllForumPosts();
        }

        public List<UserAccount> GetBloodDonorUsers()
        {
            return aHomeGateway.GetBloodDonorUsers();
        }

        public List<TopicCategory> GetTopicCategories()
        {
            return aHomeGateway.GetTopicCategories();
        }

        public List<ForumPost> GetForumPosts()
        {
            return aHomeGateway.GetForumPosts();
        }

        public int GetCountCategory(int topicCategoryId)
        {
            return aHomeGateway.GetCountCategory(topicCategoryId);
        }

        public List<ForumPost> GetFormTopicPost(int topicCategoryId)
        {
            return aHomeGateway.GetFormTopicPost(topicCategoryId);
        }
        
    }
}