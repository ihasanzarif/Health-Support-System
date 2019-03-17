using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HealthSupportApp.Gateway;
using HealthSupportApp.Models.DoctorModel;
using HealthSupportApp.Models.HomeModel;
using HealthSupportApp.Models.MainAdminModel;
using HealthSupportApp.Models.MedicalModel;
using HealthSupportApp.Models.UserModel;

namespace HealthSupportApp.Manager
{
    public class MainAdminManager
    {
        MainAdminGateway aMainAdminGateway = new MainAdminGateway();
        public List<DoctorsSpecialities> GetSpecialities()
        {
            return aMainAdminGateway.GetSpecialities();
        }

        public string SaveDoctorSpeciality(DoctorsSpecialities aDoctorSpeciality)
        {
            int rowAffected = aMainAdminGateway.SaveDoctorSpeciality(aDoctorSpeciality);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Doctor speciality saving failed.";
            }
        }

        public List<BookAppointment> GetAllAppointments()
        {
            return aMainAdminGateway.GetAllAppointments();
        }

        public string ConfirmAppointment(BookAppointment bookAppoint)
        {
            int rowAffected = aMainAdminGateway.ConfirmAppointment(bookAppoint);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Appointment confirming failed.";
            }
        }

        public ManageAppointment GetDoctorAppointInfo(BookAppointment bookAppoint)
        {
            return aMainAdminGateway.GetDoctorAppointInfo(bookAppoint);
        }

        public string UpdateDoctorAppointInfo(ManageAppointment manageAppointment)
        {
            int rowAffected = aMainAdminGateway.UpdateDoctorAppointInfo(manageAppointment);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Appoint confirmation processing failed";
            }
        }

        public List<DoctorAppointOrderVM> GetAllDoctorsAppointOrders()
        {
            return aMainAdminGateway.GetAllDoctorsAppointOrders();
        }

        public string ConfirmDoctorOrder(int manageAppointId, int doctorId)
        {
            int rowAffected = aMainAdminGateway.ConfirmDoctorOrder(manageAppointId, doctorId);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Order confirmation processing failed.";
            }
        }

        public bool IsAdminLoginValid(string username, string password)
        {
            return aMainAdminGateway.IsAdminLoginValid(username, password);
        }

        public List<Doctors> GetAllDoctors()
        {
            return aMainAdminGateway.GetAllDoctors();
        }
        public List<MedicalAccount> GetAllMedicals()
        {
            return aMainAdminGateway.GetAllMedicals();
        }

        public string ActiveDoctor(int doctorId)
        {
            int rowAffected = aMainAdminGateway.ActiveDoctor(doctorId);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Doctor account activating failed. Try again!";
            }
        }
        public string SuspendDoctor(int doctorId)
        {
            int rowAffected = aMainAdminGateway.SuspendDoctor(doctorId);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Doctor account suspending failed. Try again!";
            }
        }
        public string DeleteDoctor(int doctorId)
        {
            int rowAffected = aMainAdminGateway.DeleteDoctor(doctorId);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Doctor account deleting failed. Try again!";
            }
        }
        public string ActiveMedical(int medicalId)
        {
            int rowAffected = aMainAdminGateway.ActiveMedical(medicalId);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Medical account activating failed. Try again!";
            }
        }
        public string SuspendMedical(int medicalId)
        {
            int rowAffected = aMainAdminGateway.SuspendMedical(medicalId);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Medical account suspending failed. Try again!";
            }
        }
        public string DeleteMedical(int medicalId)
        {
            int rowAffected = aMainAdminGateway.DeleteMedical(medicalId);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Medical account deleting failed. Try again!";
            }
        }

        public BookAppointment GetBookAppointInfo(BookAppointment bookAppointment)
        {
            return aMainAdminGateway.GetBookAppointInfo(bookAppointment);
        }

        public string CancelAppointment(int id, int doctorId, int chamberId, int userId)
        {
            int rowAffected = aMainAdminGateway.CancelAppointment(id, doctorId, chamberId, userId);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "User appointment canceling failed";
            }
        }

        public string DeleteAppointment(int id, int doctorId, int chamberId, int userId)
        {
            int rowAffected = aMainAdminGateway.DeleteAppointment(id, doctorId, chamberId, userId);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "User appointment deleting failed";
            }
        }

        public string DeleteDoctorOrder(int orderId, int manageAppointId, string trxId)
        {
            int rowAffected = aMainAdminGateway.DeleteDoctorOrder(orderId, manageAppointId, trxId);
            if (rowAffected > 0)
            {
                return "Success";
            }
            else
            {
                return "Doctor's order deleting failed";
            }
        }

        public MainAdmin GetAdminData(string mobileNo)
        {
            return aMainAdminGateway.GetAdminData(mobileNo);
        }
        public string ChangePassword(ChangeAdminPassword changePassword)
        {
            bool validOldPassword = aMainAdminGateway.IsAdminOldPasswordValid(changePassword.AdminId, changePassword.OldPassword);
            if (validOldPassword)
            {
                int rowAffected = aMainAdminGateway.ChangePassword(changePassword);
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
                return "Given old password is wrong. Please try again!";
            }
        }
    }
}