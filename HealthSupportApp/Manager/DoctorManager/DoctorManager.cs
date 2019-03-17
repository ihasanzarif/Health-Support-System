using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HealthSupportApp.Gateway.DoctorGateway;
using HealthSupportApp.Gateway.UserGateway;
using HealthSupportApp.Models.DoctorModel;
using HealthSupportApp.Models.HomeModel;
using HealthSupportApp.Models.MedicalModel;
using HealthSupportApp.Models.UserModel;

namespace HealthSupportApp.Manager.DoctorManager
{
    public class DoctorManager
    {
        DoctorGateway aDoctorGateway = new DoctorGateway();
        UserGateway aUserGateway = new UserGateway();
        public List<DoctorsSpecialities> GetSpecialities()
        {
            return aDoctorGateway.GetSpecialities();
        }

        public bool IsValid(string username, string password)
        {
            return aDoctorGateway.IsValid(username, password);
        }

        public Doctors GetDoctorData(string username)
        {
            return aDoctorGateway.GetDoctorData(username);
        }

        public Doctors GetDoctorInfo(int id)
        {
            return aDoctorGateway.GetDoctorInfo(id);
        }

        public string EditDoctorProfile(Doctors aDoctor)
        {
            int rowAffected = aDoctorGateway.EditDoctorProfile(aDoctor);
            if(rowAffected > 0)
            {
                return "Success";
            }
            return "Doctor info updating failed";
        }

        public List<ViewDoctors> GetDoctorChambers(int id)
        {
            return aDoctorGateway.GetDoctorChambers(id);
        }

        public ViewDoctors GetChamberInfo(int medicalId, int doctorId)
        {
            return aDoctorGateway.GetChamberInfo(medicalId, doctorId);
        }

        public string EditDoctorSchedule(AssignedDoctors aAssignedDoctor)
        {
            bool rowAffected = aDoctorGateway.EditDoctorSchedule(aAssignedDoctor);
            if (rowAffected)
            {
                return "Success";
            }
            else
            {
                return "Doctor schedule editing failed.";
            }
        }

        public string EditDoctorFees(AssignedDoctors aAssignedDoctor)
        {
            bool rowAffected = aDoctorGateway.EditDoctorFees(aAssignedDoctor);
            if (rowAffected)
            {
                return "Success";
            }
            else
            {
                return "Doctor fees editing failed.";
            }
        }

        public string ChangeDoctorPassword(ChangePassword changePassword)
        {
            bool validOldPassword = aDoctorGateway.IsOldPasswordValid(changePassword.DoctorId, changePassword.OldPassword);
            if (validOldPassword)
            {
                int rowAffected = aDoctorGateway.ChangeDoctorPassword(changePassword);
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
                return "Your old password is wrong. Try again!";
            }
        }

        public string AddNewChamber(DoctorChamber aDoctorChamber)
        {
            int rowAffected = aDoctorGateway.AddNewChamber(aDoctorChamber);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Doctor's new chamber adding failed.";
            }
        }

        public List<DoctorChamber> GetDoctorOwnChamber(int id)
        {
            return aDoctorGateway.GetDoctorOwnChamber(id);
        }

        public DoctorChamber GetDoctorChamberInfo(int id, int doctorId)
        {
            return aDoctorGateway.GetDoctorChamberInfo(id, doctorId);
        }

        public string EditDoctorChamberFees(DoctorChamber doctorChamber)
        {
            int rowAffected = aDoctorGateway.EditDoctorChamberFees(doctorChamber);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Doctor fees editing failed.";
            }
        }

        public string EditDoctorChamberSchedule(DoctorChamber doctorChamber)
        {
            int rowAffected = aDoctorGateway.EditDoctorChamberSchedule(doctorChamber);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Doctor fees editing failed.";
            }
        }

        public bool CheckManageAppointment(int doctorId, int? medicalId)
        {
            return aDoctorGateway.CheckManageAppointment(doctorId, medicalId);
        }

        public string ExistAppointmentAvailability(bool availability, int doctorId, int? medicalId)
        {
            int rowAffected = aDoctorGateway.ExistAppointmentAvailability(availability, doctorId, medicalId);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Manage Appointment availability changing failed.";
            }
        }

        public string AppointmentAvailability(ManageAppointment manageAppointment)
        {
            int rowAffected = aDoctorGateway.AppointmentAvailability(manageAppointment);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Manage Appointment availability changing failed.";
            }
        }

        public bool CheckChamberManageAppointment(int doctorId, int? doctorChamberId)
        {
            return aDoctorGateway.CheckChamberManageAppointment(doctorId, doctorChamberId);
        }

        public string ExistChamberAppointmentAvailability(bool availability, int doctorId, int? doctorChamberId)
        {
            int rowAffected = aDoctorGateway.ExistChamberAppointmentAvailability(availability, doctorId, doctorChamberId);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Manage Appointment availability changing failed.";
            }
        }

        public string ChamberAppointmentAvailability(ManageAppointment manageAppointment)
        {
            int rowAffected = aDoctorGateway.ChamberAppointmentAvailability(manageAppointment);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Manage Appointment availability changing failed.";
            }
        }

        public string UpdateManageAppointment(ManageAppointment manageAppointment)
        {
            if (manageAppointment.MedicalId == null)
            {
                manageAppointment.MedicalId = 0;
            }
            if (manageAppointment.DoctorChamberId == null)
            {
                manageAppointment.DoctorChamberId = 0;
            }
            int rowAffected = aDoctorGateway.UpdateManageAppointment(manageAppointment);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Manage Appointment updating failed.";
            }
        }

        public Doctors IsLoginVerified(string aLoginId)
        {
            return aDoctorGateway.IsLoginVerified(aLoginId);
        }

        public string ChangeTemporaryPassword(ChangePassword changePassword)
        {
            int rowAffected = aDoctorGateway.ChangeTemporaryPassword(changePassword);
            if(rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Your temporary password changing failed";
            }
        }

        public string DeleteDoctorChamber(int chamberId, int doctorId)
        {
            if (aDoctorGateway.DeleteDoctorChamber(chamberId, doctorId))
            {
                return "Success";
            }
            else
            {
                return "Chamber deleting failed";
            }
        }
        public string DeleteMedicalChamber(int medicalId, int doctorId)
        {
            if (aDoctorGateway.DeleteMedicalChamber(medicalId, doctorId))
            {
                return "Success";
            }
            else
            {
                return "Chamber deleting failed";
            }
        }

        public string SaveDoctor(Doctors aDoctor)
        {
            if (aDoctorGateway.IsDoctorExistInMedical(aDoctor.MobileNo, aDoctor.BmdcReg) == false)
            {
                if (aUserGateway.IsMobileNoExists(aDoctor.MobileNo) == false && aDoctorGateway.IsDoctorExists(aDoctor.MobileNo, aDoctor.BmdcReg) == false)
                {
                    int rowAffected = aDoctorGateway.SaveDoctor(aDoctor);
                    if (rowAffected > 0)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "Your temporary password changing failed";
                    }
                }
                else
                {
                    return "The mobile number is already exist. Please give valid mobile number.";
                }
            }
            else
            {
                return "Warning";
            }
            
        }

        public int GetManageId(int doctorId, int? medicalId, int? doctorChamberId)
        {
            return aDoctorGateway.GetManageId(doctorId, medicalId, doctorChamberId);
        }

        public int CreateOrderForChamber(DoctorAppointOrder doctorAppointOrder)
        {
            return aDoctorGateway.CreateOrderForChamber(doctorAppointOrder);
        }

        public List<ChambersForOrder> GetChambersForOrder(int doctorId)
        {
            return aDoctorGateway.GetChambersForOrder(doctorId);
        }

        public int GetRemainingAppoint(int manageAppointId)
        {
            return aDoctorGateway.GetRemainingAppoint(manageAppointId);
        }

        public string SaveOrderAppointment(DoctorAppointOrder doctorAppointOrder)
        {
            int rowAffected = aDoctorGateway.SaveOrderAppointment(doctorAppointOrder);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Your Appointment order failed. Please try again";
            }
        }

        public List<OrderStatus> GetOrderStatus(int doctorId)
        {
            return aDoctorGateway.GetOrderStatus(doctorId);
        }

        public ManageAppointment GetDoctorChamberManageInfo(int doctorId, int? medicalId, int? doctorChamberId)
        {
            return aDoctorGateway.GetDoctorChamberManageInfo(doctorId, medicalId, doctorChamberId);
        }

        public List<Appointments> GetUserAppointments(int doctorId)
        {
            return aDoctorGateway.GetUserAppointments(doctorId);
        }

        public List<BookAppointment> GetAllUserAppointments(int doctorId)
        {
            return aDoctorGateway.GetAllUserAppointments(doctorId);
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

        public string CancelUserAppointment(int id, int doctorId, int userId, string mobileNo)
        {
            int rowAffected = aDoctorGateway.CancelUserAppointment(id,doctorId, userId,mobileNo);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Appointment canceling failed. Try again!";
            }
        }
    }
}