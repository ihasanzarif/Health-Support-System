using System.Collections.Generic;
using HealthSupportApp.Gateway.DoctorGateway;
using HealthSupportApp.Gateway.MedicalGateway;
using HealthSupportApp.Gateway.UserGateway;
using HealthSupportApp.Models;
using HealthSupportApp.Models.HomeModel;
using HealthSupportApp.Models.MedicalModel;

namespace HealthSupportApp.Manager.MedicalManager
{
    public class MedicalManager
    {
        MedicalGateway aMedicalGateway = new MedicalGateway();
        UserGateway aUserGateway = new UserGateway();
        DoctorGateway aDoctorGateway = new DoctorGateway();
        public string Save(MedicalAccount aMedicalAccount)
        {
            if (aMedicalGateway.IsEmailExists(aMedicalAccount.MedicalEmail) == false)
            {
                if (aMedicalGateway.IsMobileNoExists(aMedicalAccount.MedicalContactNo1) == false)
                {
                    int rowAffected = aMedicalGateway.Save(aMedicalAccount);
                    if (rowAffected > 0)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "Medical registration is not successful!";
                    }
                }
                else
                {
                    return "Medical contact number is already used. Try another!";
                }
            }
            else
            {
                return "The email is already exists. Please, Try to give another Email.";
            }
        }

        public bool IsValid(string medicalEmail, string password)
        {
            return aMedicalGateway.IsValid(medicalEmail, password);
        }

        public MedicalAccount GetMedicalData(string username)
        {
            return aMedicalGateway.GetMedicalData(username);
        }

        public bool IsActivationCodeValid(string id)
        {
            return aMedicalGateway.IsActivationCodeValid(id);
        }

        public bool UpdateEmailVerification(bool isEmailVerified, string id)
        {
            return aMedicalGateway.UpdateEmailVerification(isEmailVerified, id);
        }

        public MedicalAccount IsMedicalLoginVerified(string aLoginEmail)
        {
            return aMedicalGateway.IsMedicalLoginVerified(aLoginEmail);
        }

        public string UpdateMedicalProfile(MedicalAccount aMedicalAccount)
        {
            int rowAffected = aMedicalGateway.UpdateMedicalProfile(aMedicalAccount);
            if (rowAffected > 0)
            {
                return "Medical profile updated successfully.";
            }
            else
            {
                return "Medical profile updating failed.";
            }
        }

        public List<DoctorsSpecialities> GetSpecialities()
        {
            return aMedicalGateway.GetSpecialities();
        }

        public string SaveDoctor(Doctors aDoctors)
        {
            bool isDoctorExistInSameMedical = aMedicalGateway.IsDoctorExistInCurrentMedical(aDoctors.MobileNo, aDoctors.BmdcReg, aDoctors.MedicalId);
            if (isDoctorExistInSameMedical)
            {
                return "This doctor already exists in your medical.";
            }
            else
            {
                bool isDoctorExistInOtherMedical = aMedicalGateway.IsDoctorExistInOtherMedical(aDoctors.MobileNo, aDoctors.BmdcReg, aDoctors.MedicalId);
                if (isDoctorExistInOtherMedical)
                {
                    return "Exist";
                }
                else
                {
                    if (aUserGateway.IsMobileNoExists(aDoctors.MobileNo) == false && aDoctorGateway.IsDoctorExists(aDoctors.MobileNo, aDoctors.BmdcReg) == false)
                    {
                        bool rowAffected = aMedicalGateway.SaveDoctor(aDoctors);
                        if (rowAffected)
                        {
                            return "Success";
                        }
                        else
                        {
                            return "Doctor assigning failed.";
                        }
                    }
                    else
                    {
                        return "Exist";
                    }
                }
            }
        }

        public List<ViewDoctors> GetAllDoctors(int medicalId)
        {
            return aMedicalGateway.GetAllDoctors(medicalId);
        }

        public ViewDoctors GetDoctor(int id, int medicalId)
        {
            return aMedicalGateway.GetDoctor(id, medicalId);
        }

        public string EditDoctor(Doctors doctors)
        {
            bool rowAffected = aMedicalGateway.EditDoctor(doctors);
            if (rowAffected)
            {
                return "Success";
            }
            else
            {
                return "Doctor info editing failed.";
            }
        }

        public int GetExistDoctor(string mobileNo, string bmdcReg)
        {
            return aMedicalGateway.GetExistDoctor(mobileNo, bmdcReg);
        }

        public string AddDoctorFromOtherMedical(AssignedDoctors aAssignedDoctor)
        {
            bool rowAffected = aMedicalGateway.SaveAssignedDoctor(aAssignedDoctor);
            if (rowAffected)
            {
                return "Success";
            }
            else
            {
                return "Doctor assigning failed.";
            }
        }

        public string EditDoctorSchedule(AssignedDoctors assignedDoctors)
        {
            bool rowAffected = aMedicalGateway.EditDoctorSchedule(assignedDoctors);
            if (rowAffected)
            {
                return "Success";
            }
            else
            {
                return "Doctor schedule editing failed.";
            }
        }

        public AssignedDoctors GetDoctorSchedule(int id)
        {
            throw new System.NotImplementedException();
        }

        public string ChangeMedicalPassword(ChangeMedicalPassword changeMedicalPassword)
        {
            bool validOldPassword = aMedicalGateway.IsMedicalOldPasswordValid(changeMedicalPassword.MedicalId, changeMedicalPassword.OldPassword);
            if (validOldPassword)
            {
                int rowAffected = aMedicalGateway.ChangeMedicalPassword(changeMedicalPassword);
                if (rowAffected > 0)
                {
                    return "Success";
                }
                else
                {
                    return "Password changing failed.";
                }
            }
            else
            {
                return "Old password is wrong. Please try again!";
            }
        }
        public string SaveComment(Comments aComment)
        {
            int rowAffected = aUserGateway.SaveComment(aComment);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Your comment publishing failed. Try again!";
            }
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
    }
}