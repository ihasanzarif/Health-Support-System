using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HealthSupportApp.Models.DoctorModel;
using HealthSupportApp.Models.HomeModel;
using HealthSupportApp.Models.MainAdminModel;
using HealthSupportApp.Models.MedicalModel;
using HealthSupportApp.Models.UserModel;

namespace HealthSupportApp.Gateway
{
    public class MainAdminGateway : ParentGateway
    {

        public List<DoctorsSpecialities> GetSpecialities()
        {
            Query = "SELECT * FROM DoctorsSpecialities ORDER BY Speciality";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<DoctorsSpecialities> doctorsSpecialities = new List<DoctorsSpecialities>();
            while (Reader.Read())
            {
                DoctorsSpecialities specialities = new DoctorsSpecialities();
                specialities.Id = Convert.ToInt32(Reader["Id"]);
                specialities.Speciality = Reader["Speciality"].ToString();
                doctorsSpecialities.Add(specialities);
            }

            Reader.Close();
            Connection.Close();
            return doctorsSpecialities;
        }

        public int SaveDoctorSpeciality(DoctorsSpecialities aDoctorSpeciality)
        {
            Query = "INSERT INTO DoctorsSpecialities VALUES(@speciality)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("speciality", aDoctorSpeciality.Speciality);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public List<BookAppointment> GetAllAppointments()
        {
            Query = "SELECT * FROM BookAppointment WHERE NOT Status = 'Canceled' ORDER BY Status DESC";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<BookAppointment> bookAppointments = new List<BookAppointment>();
            while (Reader.Read())
            {
                BookAppointment bookAppointment = new BookAppointment();
                bookAppointment.BookAppointId = Convert.ToInt32(Reader["Id"]);
                bookAppointment.DoctorId = Convert.ToInt32(Reader["DoctorId"]);
                bookAppointment.ChamberId = Convert.ToInt32(Reader["ChamberId"]);
                bookAppointment.UserId = Convert.ToInt32(Reader["UserId"]);
                bookAppointment.UserName = Reader["UserName"].ToString();
                bookAppointment.MobileNo = Reader["MobileNo"].ToString();
                bookAppointment.DoctorName = Reader["DoctorName"].ToString();
                bookAppointment.ChamberName = Reader["ChamberName"].ToString();
                bookAppointment.Schedule = Reader["Schedule"].ToString();
                bookAppointment.AppointDate = Reader["AppointDate"].ToString();
                bookAppointment.PaymentMethod = Reader["PaymentMethod"].ToString();
                bookAppointment.TrxId = Reader["TrxId"].ToString();
                bookAppointment.SerialNo = Convert.ToInt32(Reader["SerialNo"]);
                bookAppointment.Status = Reader["Status"].ToString();
                bookAppointments.Add(bookAppointment);
            }
            Reader.Close();
            Connection.Close();
            return bookAppointments;
        }

        public int ConfirmAppointment(BookAppointment bookAppoint)
        {
            try
            {
                Query = "UPDATE BookAppointment SET Status = @status, SerialNo = @serialNo WHERE Id = @id AND UserId = @userdId AND DoctorId = @doctorId AND ChamberId = @chamberId";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("status", bookAppoint.Status);
                Command.Parameters.AddWithValue("serialNo", bookAppoint.SerialNo);
                Command.Parameters.AddWithValue("id", bookAppoint.BookAppointId);
                Command.Parameters.AddWithValue("userdId", bookAppoint.UserId);
                Command.Parameters.AddWithValue("doctorId", bookAppoint.DoctorId);
                Command.Parameters.AddWithValue("chamberId", bookAppoint.ChamberId);
                Connection.Open();
                int rowAffected = Command.ExecuteNonQuery();
                Connection.Close();
                return rowAffected;
            }
            catch (Exception ex)
            {
                Connection.Close();
                int rowAffected = 0;
                return rowAffected;
            }
        }

        public ManageAppointment GetDoctorAppointInfo(BookAppointment bookAppoint)
        {
            Query = "SELECT * FROM ManageAppointVM WHERE DoctorId = @doctorId AND MedicalId = @chamberId AND Expired = 'False'";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("doctorId", bookAppoint.DoctorId);
            Command.Parameters.AddWithValue("chamberId", bookAppoint.ChamberId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            ManageAppointment manageAppointment = null;
            while (Reader.Read())
            {
                manageAppointment = new ManageAppointment();
                manageAppointment.Id = Convert.ToInt32(Reader["Id"]);
                manageAppointment.SatAvailableAppoint = Convert.ToInt32(Reader["SatAvailableAppoint"]);
                manageAppointment.SunAvailableAppoint = Convert.ToInt32(Reader["SunAvailableAppoint"]);
                manageAppointment.MonAvailableAppoint = Convert.ToInt32(Reader["MonAvailableAppoint"]);
                manageAppointment.TueAvailableAppoint = Convert.ToInt32(Reader["TueAvailableAppoint"]);
                manageAppointment.WedAvailableAppoint = Convert.ToInt32(Reader["WedAvailableAppoint"]);
                manageAppointment.ThuAvailableAppoint = Convert.ToInt32(Reader["ThuAvailableAppoint"]);
                manageAppointment.FriAvailableAppoint = Convert.ToInt32(Reader["FriAvailableAppoint"]);
                manageAppointment.TotalAvailableAppoint = Convert.ToInt32(Reader["TotalAvailableAppoint"]);
                manageAppointment.RemainingAppoint = Convert.ToInt32(Reader["RemainingAppoint"]);
                manageAppointment.TotalAppoint = Convert.ToInt32(Reader["TotalAppoint"]);
                manageAppointment.OrderedAppoint = Convert.ToInt32(Reader["AppointAmount"]);
                manageAppointment.DoctorId = Convert.ToInt32(Reader["DoctorId"]);
                manageAppointment.MedicalId = Convert.ToInt32(Reader["MedicalId"]);
            }
            Reader.Close();
            Connection.Close();
            if (manageAppointment == null)
            {
                Query = "SELECT * FROM ManageAppointVM WHERE DoctorId = @doctorId AND DoctorChamberId = @chamberId AND Expired = 'False'";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("doctorId", bookAppoint.DoctorId);
                Command.Parameters.AddWithValue("chamberId", bookAppoint.ChamberId);
                Connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    manageAppointment = new ManageAppointment();
                    manageAppointment.Id = Convert.ToInt32(Reader["Id"]);
                    manageAppointment.SatAvailableAppoint = Convert.ToInt32(Reader["SatAvailableAppoint"]);
                    manageAppointment.SunAvailableAppoint = Convert.ToInt32(Reader["SunAvailableAppoint"]);
                    manageAppointment.MonAvailableAppoint = Convert.ToInt32(Reader["MonAvailableAppoint"]);
                    manageAppointment.TueAvailableAppoint = Convert.ToInt32(Reader["TueAvailableAppoint"]);
                    manageAppointment.WedAvailableAppoint = Convert.ToInt32(Reader["WedAvailableAppoint"]);
                    manageAppointment.ThuAvailableAppoint = Convert.ToInt32(Reader["ThuAvailableAppoint"]);
                    manageAppointment.FriAvailableAppoint = Convert.ToInt32(Reader["FriAvailableAppoint"]);
                    manageAppointment.TotalAvailableAppoint = Convert.ToInt32(Reader["TotalAvailableAppoint"]);
                    manageAppointment.RemainingAppoint = Convert.ToInt32(Reader["RemainingAppoint"]);
                    manageAppointment.TotalAppoint = Convert.ToInt32(Reader["TotalAppoint"]);
                    manageAppointment.OrderedAppoint = Convert.ToInt32(Reader["AppointAmount"]);
                    manageAppointment.DoctorId = Convert.ToInt32(Reader["DoctorId"]);
                    manageAppointment.DoctorChamberId = Convert.ToInt32(Reader["DoctorChamberId"]);
                }
                Reader.Close();
                Connection.Close();
            }
            return manageAppointment;
        }

        public int UpdateDoctorAppointInfo(ManageAppointment manageAppointment)
        {
            int rowAffected = 0;
            try
            {
                if (manageAppointment.MedicalId > 0)
                {
                    Query =
                        "UPDATE ManageAppointment SET SatAvailableAppoint = @satAppoint, SunAvailableAppoint = @sunAppoint, MonAvailableAppoint = @monAppoint, TueAvailableAppoint = @tueAppoint, WedAvailableAppoint = @wedAppoint, ThuAvailableAppoint = @ThuAppoint, FriAvailableAppoint = @friAppoint, TotalAvailableAppoint = @totalAppoint WHERE Id = @id AND DoctorId = @doctorId AND MedicalId = @medicalId";
                    Command = new SqlCommand(Query, Connection);
                    Command.Parameters.AddWithValue("satAppoint", manageAppointment.SatAvailableAppoint);
                    Command.Parameters.AddWithValue("sunAppoint", manageAppointment.SunAvailableAppoint);
                    Command.Parameters.AddWithValue("monAppoint", manageAppointment.MonAvailableAppoint);
                    Command.Parameters.AddWithValue("tueAppoint", manageAppointment.TueAvailableAppoint);
                    Command.Parameters.AddWithValue("wedAppoint", manageAppointment.WedAvailableAppoint);
                    Command.Parameters.AddWithValue("thuAppoint", manageAppointment.ThuAvailableAppoint);
                    Command.Parameters.AddWithValue("friAppoint", manageAppointment.FriAvailableAppoint);
                    Command.Parameters.AddWithValue("totalAppoint", manageAppointment.TotalAvailableAppoint);
                    Command.Parameters.AddWithValue("id", manageAppointment.Id);
                    Command.Parameters.AddWithValue("doctorId", manageAppointment.DoctorId);
                    Command.Parameters.AddWithValue("medicalId", manageAppointment.MedicalId);
                    Connection.Open();
                    rowAffected = Command.ExecuteNonQuery();
                    Connection.Close();
                }
                if (manageAppointment.DoctorChamberId > 0)
                {
                    Query =
                        "UPDATE ManageAppointment SET SatAvailableAppoint = @satAppoint, SunAvailableAppoint = @sunAppoint, MonAvailableAppoint = @monAppoint, TueAvailableAppoint = @tueAppoint, WedAvailableAppoint = @wedAppoint, ThuAvailableAppoint = @ThuAppoint, FriAvailableAppoint = @friAppoint, TotalAvailableAppoint = @totalAppoint WHERE Id = @id AND DoctorId = @doctorId AND DoctorChamberId = @doctorChamberId";
                    Command = new SqlCommand(Query, Connection);
                    Command.Parameters.AddWithValue("satAppoint", manageAppointment.SatAvailableAppoint);
                    Command.Parameters.AddWithValue("sunAppoint", manageAppointment.SunAvailableAppoint);
                    Command.Parameters.AddWithValue("monAppoint", manageAppointment.MonAvailableAppoint);
                    Command.Parameters.AddWithValue("tueAppoint", manageAppointment.TueAvailableAppoint);
                    Command.Parameters.AddWithValue("wedAppoint", manageAppointment.WedAvailableAppoint);
                    Command.Parameters.AddWithValue("thuAppoint", manageAppointment.ThuAvailableAppoint);
                    Command.Parameters.AddWithValue("friAppoint", manageAppointment.FriAvailableAppoint);
                    Command.Parameters.AddWithValue("totalAppoint", manageAppointment.TotalAvailableAppoint);
                    Command.Parameters.AddWithValue("id", manageAppointment.Id);
                    Command.Parameters.AddWithValue("doctorId", manageAppointment.DoctorId);
                    Command.Parameters.AddWithValue("doctorChamberId", manageAppointment.DoctorChamberId);
                    Connection.Open();
                    rowAffected = Command.ExecuteNonQuery();
                    Connection.Close();
                }
                UpdateDoctorAppointOrder(manageAppointment.Id, manageAppointment.RemainingAppoint);
                return rowAffected;
            }
            catch (Exception ex)
            {
                Connection.Close();
                return rowAffected;
            }
        }

        private void UpdateDoctorAppointOrder(int manageAppointmentId, int remainingAppoint)
        {
            Query = "UPDATE DoctorAppointOrder SET RemainingAppoint = @remainingAppoint WHERE ManageAppointId = @id AND Expired = 'False'";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("remainingAppoint", remainingAppoint);
            Command.Parameters.AddWithValue("id", manageAppointmentId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
        }
        public List<DoctorAppointOrderVM> GetAllDoctorsAppointOrders()
        {
            Query = "SELECT * FROM DoctorChamberOrder ORDER BY Status DESC";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<DoctorAppointOrderVM> doctorsOrders = new List<DoctorAppointOrderVM>();
            while (Reader.Read())
            {
                DoctorAppointOrderVM doctorOrder = new DoctorAppointOrderVM();
                doctorOrder.OrderId = Convert.ToInt32(Reader["OrderId"]);
                doctorOrder.ManageAppointId = Convert.ToInt32(Reader["Id"]);
                doctorOrder.DoctorId = Convert.ToInt32(Reader["DoctorId"]);
                doctorOrder.MedicalId = Convert.ToInt32(Reader["MedicalId"]);
                doctorOrder.DoctorChamberId = Convert.ToInt32(Reader["DoctorChamberId"]);
                doctorOrder.DoctorName = Reader["Name"].ToString();
                doctorOrder.ChamberName = Reader["MedicalName"].ToString();
                if (doctorOrder.ChamberName == "")
                {
                    doctorOrder.ChamberName = Reader["ChamberName"].ToString();
                }
                doctorOrder.AppointAmount = Convert.ToInt32(Reader["AppointAmount"]);
                doctorOrder.TotalPrice = Convert.ToInt32(Reader["TotalPrice"]);
                doctorOrder.PaymentMethod = Reader["PaymentMethod"].ToString();
                doctorOrder.TrnxId = Reader["TrxId"].ToString();
                doctorOrder.OrderDate = Reader["OrderDate"].ToString();
                doctorOrder.ExpireDate = Reader["ExpireDate"].ToString();
                doctorOrder.Status = Reader["Status"].ToString();
                doctorsOrders.Add(doctorOrder);
            }
            Reader.Close();
            Connection.Close();
            return doctorsOrders;
        }

        public int ConfirmDoctorOrder(int manageAppointId, int doctorId)
        {
            Query = "UPDATE DoctorChamberOrder SET Status = 'Active' WHERE Id = @manageAppointId AND DoctorId = @doctorId AND Status = 'Pending'";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("manageAppointId", manageAppointId);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public bool IsAdminLoginValid(string username, string password)
        {
            Query = "SELECT Username, Password FROM MainAdmin WHERE Username = @username AND Password = @password";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("username", username);
            Command.Parameters.AddWithValue("password", password);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;
        }

        public List<Doctors> GetAllDoctors()
        {
            Query = "SELECT * FROM Doctors ORDER BY Status DESC";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Doctors> doctors = new List<Doctors>();
            while (Reader.Read())
            {
                Doctors doctor = new Doctors();
                doctor.Id = Convert.ToInt32(Reader["Id"]);
                doctor.Name = Reader["Name"].ToString();
                doctor.Title = Reader["Title"].ToString();
                doctor.Speciality = Reader["Speciality"].ToString();
                doctor.Email = Reader["Email"].ToString();
                doctor.MobileNo = Reader["MobileNo"].ToString();
                doctor.BmdcReg = Reader["BmdcReg"].ToString();
                doctor.Status = Reader["Status"].ToString();
                doctor.AccountCreatedDate = Reader["AccountCreatedDate"].ToString();
                DateTime newDate = Convert.ToDateTime(doctor.AccountCreatedDate);
                doctor.AccountCreatedDate = newDate.ToString("dd/MM/yyyy");
                doctors.Add(doctor);
            }
            Reader.Close();
            Connection.Close();
            return doctors;
        }
        public List<MedicalAccount> GetAllMedicals()
        {
            Query = "SELECT * FROM MedicalAccounts ORDER BY Status DESC";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<MedicalAccount> medicalAccounts = new List<MedicalAccount>();
            while (Reader.Read())
            {
                MedicalAccount medicalAccount = new MedicalAccount();
                medicalAccount.MedicalId = Convert.ToInt32(Reader["MedicalId"]);
                medicalAccount.MedicalType = Reader["MedicalType"].ToString();
                medicalAccount.MedicalName = Reader["MedicalName"].ToString();
                medicalAccount.ContactPersonName = Reader["ContactPersonName"].ToString();
                medicalAccount.ContactPersonPhoneNo = Reader["ContactPersonPhoneNo"].ToString();
                medicalAccount.MedicalEmail = Reader["MedicalEmail"].ToString();
                medicalAccount.Address = Reader["Address"].ToString();
                medicalAccount.City = Reader["City"].ToString();
                medicalAccount.Status = Reader["Status"].ToString();
                medicalAccount.AccountCreatedDate = Reader["AccountCreatedDate"].ToString();
                DateTime newDate = Convert.ToDateTime(medicalAccount.AccountCreatedDate);
                medicalAccount.AccountCreatedDate = newDate.ToString("dd/MM/yyyy");
                medicalAccounts.Add(medicalAccount);
            }
            Reader.Close();
            Connection.Close();
            return medicalAccounts;
        }

        public int ActiveDoctor(int doctorId)
        {
            Query = "UPDATE Doctors SET Status = 'Active' WHERE Id = @doctorId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
        public int SuspendDoctor(int doctorId)
        {
            Query = "UPDATE Doctors SET Status = 'Suspend' WHERE Id = @doctorId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
        public int DeleteDoctor(int doctorId)
        {
            int deleteAssignedDoctor = DeleteAssignedDoctor(doctorId);
            int deleteChamber = DeleteDoctorChamber(doctorId);
            Query = "DELETE FROM Doctors WHERE Id = @doctorId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        private int DeleteAssignedDoctor(int doctorId)
        {
            Query = "DELETE FROM AssignedDoctors WHERE DoctorId = @doctorId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
        private int DeleteDoctorChamber(int doctorId)
        {
            Query = "DELETE FROM DoctorsChambers WHERE DoctorId = @doctorId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public int ActiveMedical(int medicalId)
        {
            Query = "UPDATE MedicalAccounts SET Status = 'Active' WHERE MedicalId = @medicalId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
        public int SuspendMedical(int medicalId)
        {
            Query = "UPDATE MedicalAccounts SET Status = 'Suspend' WHERE MedicalId = @medicalId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
        public int DeleteMedical(int medicalId)
        {
            Query = "DELETE FROM MedicalAccounts WHERE MedicalId = @medicalId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public BookAppointment GetBookAppointInfo(BookAppointment bookAppointment)
        {
            Query = "SELECT * FROM BookAppointment WHERE Id = @id AND DoctorId = @doctorId AND ChamberId = @chamberId AND UserId = @userId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("id", bookAppointment.BookAppointId);
            Command.Parameters.AddWithValue("doctorId", bookAppointment.DoctorId);
            Command.Parameters.AddWithValue("chamberId", bookAppointment.ChamberId);
            Command.Parameters.AddWithValue("userId", bookAppointment.UserId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            BookAppointment bookAppointInfo = null;
            if (Reader.Read())
            {
                bookAppointInfo = new BookAppointment();
                bookAppointInfo.DoctorName = Reader["DoctorName"].ToString();
                bookAppointInfo.AppointDate = Reader["AppointDate"].ToString();
                bookAppointInfo.Schedule = Reader["Schedule"].ToString();
                bookAppointInfo.SerialNo = Convert.ToInt32(Reader["SerialNo"]);
            }
            Reader.Close();
            Connection.Close();
            return bookAppointInfo;
        }

        public int CancelAppointment(int id, int doctorId, int chamberId, int userId)
        {
            Query =
                "UPDATE BookAppointment SET Status = 'Canceled' WHERE Id = @id AND DoctorId = @doctorId AND ChamberId = @chamberId AND UserId = @userId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("id",id);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Command.Parameters.AddWithValue("chamberId", chamberId);
            Command.Parameters.AddWithValue("userId", userId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public int DeleteAppointment(int id, int doctorId, int chamberId, int userId)
        {
            Query =
                "DELETE FROM BookAppointment WHERE Id = @id AND DoctorId = @doctorId AND ChamberId = @chamberId AND UserId = @userId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("id", id);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Command.Parameters.AddWithValue("chamberId", chamberId);
            Command.Parameters.AddWithValue("userId", userId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public int DeleteDoctorOrder(int orderId, int manageAppointId, string trxId)
        {
            Query =
                "DELETE FROM DoctorAppointOrder WHERE OrderId = @orderId AND ManageAppointId = @manageAppointId AND TrxId = @trxId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("orderId", orderId);
            Command.Parameters.AddWithValue("manageAppointId", manageAppointId);
            Command.Parameters.AddWithValue("trxId", trxId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public MainAdmin GetAdminData(string userName)
        {
            Query = "SELECT * FROM MainAdmin WHERE Username = @userName";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("userName", userName);
            Connection.Open();
            Reader = Command.ExecuteReader();
            MainAdmin mainAdmin = null;
            if (Reader.Read())
            {
                mainAdmin = new MainAdmin();
                mainAdmin.MainAdminId = Convert.ToInt32(Reader["AdminId"]); 
                mainAdmin.Name = Reader["Name"].ToString();
                mainAdmin.Username = Reader["Username"].ToString();
                mainAdmin.MobileNo = Reader["MobileNo"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return mainAdmin;
        }

        public bool IsAdminOldPasswordValid(int adminId, string oldPassword)
        {
            Query = "SELECT * FROM MainAdmin WHERE AdminId = @adminId AND Password = @oldPassword";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("adminId", adminId);
            Command.Parameters.AddWithValue("oldPassword", oldPassword);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;
        }

        public int ChangePassword(ChangeAdminPassword changePassword)
        {
            Query = "UPDATE MainAdmin SET Password = @newPassword WHERE AdminId = @adminId AND Password = @oldPassword";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("newPassword", changePassword.NewPassword);
            Command.Parameters.AddWithValue("adminId", changePassword.AdminId);
            Command.Parameters.AddWithValue("oldPassword", changePassword.OldPassword);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
    }
}