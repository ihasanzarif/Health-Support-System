using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HealthSupportApp.Models.DoctorModel;
using HealthSupportApp.Models.MedicalModel;
using HealthSupportApp.Models.UserModel;

namespace HealthSupportApp.Gateway.DoctorGateway
{
    public class DoctorGateway : ParentGateway
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

        public bool IsValid(string username, string password)
        {
            Query = "SELECT * FROM Doctors WHERE MobileNo = @username AND Password = @password";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("username", username);
            Command.Parameters.AddWithValue("password", password);
            Command.Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRows = Reader.HasRows;
            Reader.Close();
            Command.Connection.Close();
            return hasRows;
        }

        public Doctors GetDoctorData(string username)
        {
            Query = "SELECT Id, Name, ImagePath FROM Doctors WHERE MobileNo = @username";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("username", username);
            Command.Connection.Open();
            Reader = Command.ExecuteReader();
            Doctors aDoctor = null;
            if (Reader.HasRows)
            {
                Reader.Read();
                aDoctor = new Doctors();
                aDoctor.Id = Convert.ToInt32(Reader["Id"]);
                aDoctor.Name = Reader["Name"].ToString();
                aDoctor.ImagePath = Reader["ImagePath"].ToString();
            }
            Reader.Close();
            Command.Connection.Close();
            return aDoctor;
        }

        public Doctors GetDoctorInfo(int id)
        {
            Query = "SELECT * FROM Doctors WHERE Id = @id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("id", id);
            Command.Connection.Open();
            Reader = Command.ExecuteReader();
            Doctors aDoctor = null;
            if (Reader.HasRows)
            {
                Reader.Read();
                aDoctor = new Doctors();
                aDoctor.Id = Convert.ToInt32(Reader["Id"]);
                aDoctor.Name = Reader["Name"].ToString();
                aDoctor.Gender = Reader["Gender"].ToString();
                aDoctor.Title = Reader["Title"].ToString();
                aDoctor.Speciality = Reader["Speciality"].ToString();
                aDoctor.Email = Reader["Email"].ToString();
                aDoctor.MobileNo = Reader["MobileNo"].ToString();
                aDoctor.PhoneNo = Reader["PhoneNo"].ToString();
                aDoctor.BmdcReg = Reader["BmdcReg"].ToString();
                aDoctor.OtherSpecification = Reader["OtherSpecification"].ToString();
                aDoctor.ImagePath = Reader["ImagePath"].ToString();
            }
            Reader.Close();
            Command.Connection.Close();
            return aDoctor;
        }

        public int EditDoctorProfile(Doctors aDoctor)
        {
            try
            {
                Query =
                    "UPDATE Doctors SET ImagePath = @imagePath, Name = @name, Gender = @gender, Title =@title, Speciality = @speciality, Email = @email, PhoneNo = @phoneNo, OtherSpecification = @other WHERE Id = @id";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("imagePath", aDoctor.ImagePath);
                Command.Parameters.AddWithValue("name", aDoctor.Name);
                Command.Parameters.AddWithValue("gender", aDoctor.Gender);
                Command.Parameters.AddWithValue("title", aDoctor.Title);
                Command.Parameters.AddWithValue("speciality", aDoctor.Speciality);
                Command.Parameters.AddWithValue("email", aDoctor.Email);
                Command.Parameters.AddWithValue("phoneNo", aDoctor.PhoneNo);
                Command.Parameters.AddWithValue("other", aDoctor.OtherSpecification);
                Command.Parameters.AddWithValue("id", aDoctor.Id);
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

        public List<ViewDoctors> GetDoctorChambers(int id)
        {
            Query =
                "SELECT * FROM ViewDoctors WHERE Id = @id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("id", id);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<ViewDoctors> viewDoctors = new List<ViewDoctors>();
            while (Reader.Read())
            {
                ViewDoctors viewDoctor = new ViewDoctors();
                viewDoctor.MedicalId = Convert.ToInt32(Reader["MedicalId"]);
                viewDoctor.MedicalName = Reader["MedicalName"].ToString();
                viewDoctor.MedicalAddress = Reader["Address"].ToString();
                viewDoctor.MedicalArea = Reader["Area"].ToString();
                viewDoctor.MedicalCity = Reader["City"].ToString();
                viewDoctor.Fee = Convert.ToInt32(Reader["DoctorFee"]);
                viewDoctor.ReturningFee = Convert.ToInt32(Reader["DoctorReturningFee"]);
                viewDoctor.Sat = Convert.ToBoolean(Reader["Sat"]);
                viewDoctor.Sun = Convert.ToBoolean(Reader["Sun"]);
                viewDoctor.Mon = Convert.ToBoolean(Reader["Mon"]);
                viewDoctor.Tue = Convert.ToBoolean(Reader["Tue"]);
                viewDoctor.Wed = Convert.ToBoolean(Reader["Wed"]);
                viewDoctor.Thu = Convert.ToBoolean(Reader["Thu"]);
                viewDoctor.Fri = Convert.ToBoolean(Reader["Fri"]);
                viewDoctor.SatTime = Reader["SatTime"].ToString();
                viewDoctor.SunTime = Reader["SunTime"].ToString();
                viewDoctor.MonTime = Reader["MonTime"].ToString();
                viewDoctor.TueTime = Reader["TueTime"].ToString();
                viewDoctor.WedTime = Reader["WedTime"].ToString();
                viewDoctor.ThuTime = Reader["ThuTime"].ToString();
                viewDoctor.FriTime = Reader["FriTime"].ToString();
                viewDoctors.Add(viewDoctor);
            }
            if (viewDoctors.Count > 0)
            {
                foreach (ViewDoctors aViewDoctor in viewDoctors)
                {
                    aViewDoctor.ManageAppointment = GetManageAppointmentInfo(id, aViewDoctor.MedicalId);
                }
            }
            Reader.Close();
            Connection.Close();
            return viewDoctors;
        }

        private ManageAppointment GetManageAppointmentInfo(int doctorId, int medicalId)
        {
            Connection.Close();
            Query = "SELECT * FROM ManageAppointVM WHERE DoctorId = @doctorId AND MedicalId = @medicalId AND Expired = 'False'";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            ManageAppointment manageAppointment = new ManageAppointment();
            while (Reader.Read())
            {
                manageAppointment.Id = Convert.ToInt32(Reader["Id"]);
                manageAppointment.SatCapacity = Convert.ToInt32(Reader["SatCapacity"]);
                manageAppointment.SunCapacity = Convert.ToInt32(Reader["SunCapacity"]);
                manageAppointment.MonCapacity = Convert.ToInt32(Reader["MonCapacity"]);
                manageAppointment.TueCapacity = Convert.ToInt32(Reader["TueCapacity"]);
                manageAppointment.WedCapacity = Convert.ToInt32(Reader["WedCapacity"]);
                manageAppointment.ThuCapacity = Convert.ToInt32(Reader["ThuCapacity"]);
                manageAppointment.FriCapacity = Convert.ToInt32(Reader["FriCapacity"]);
                manageAppointment.SatAvailableAppoint = Convert.ToInt32(Reader["SatAvailableAppoint"]);
                manageAppointment.SunAvailableAppoint = Convert.ToInt32(Reader["SunAvailableAppoint"]);
                manageAppointment.MonAvailableAppoint = Convert.ToInt32(Reader["MonAvailableAppoint"]);
                manageAppointment.TueAvailableAppoint = Convert.ToInt32(Reader["TueAvailableAppoint"]);
                manageAppointment.WedAvailableAppoint = Convert.ToInt32(Reader["WedAvailableAppoint"]);
                manageAppointment.ThuAvailableAppoint = Convert.ToInt32(Reader["ThuAvailableAppoint"]);
                manageAppointment.FriAvailableAppoint = Convert.ToInt32(Reader["FriAvailableAppoint"]);
                manageAppointment.OnlineAppointment = Convert.ToBoolean(Reader["OnlineAppoint"]);
                manageAppointment.UsedAppoint = Convert.ToInt32(Reader["UsedAppoint"]);
                manageAppointment.TotalAvailableAppoint = Convert.ToInt32(Reader["TotalAvailableAppoint"]);
                manageAppointment.DoctorId = Convert.ToInt32(Reader["DoctorId"]);
                manageAppointment.MedicalId = Convert.ToInt32(Reader["MedicalId"]);
                manageAppointment.OrderedAppoint = Convert.ToInt32(Reader["AppointAmount"]);
                manageAppointment.RemainingAppoint = Convert.ToInt32(Reader["RemainingAppoint"]);
                manageAppointment.OrderExpireDate = Convert.ToDateTime(Reader["ExpireDate"]);
                manageAppointment.Status = Reader["Status"].ToString();
            }
            return manageAppointment;
        }

        public ViewDoctors GetChamberInfo(int medicalId, int doctorId)
        {
            Query =
                "SELECT * FROM ViewDoctors WHERE Id = @doctorId AND MedicalId = @medicalId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            ViewDoctors viewChamber = new ViewDoctors();
            if (Reader.Read())
            {
                viewChamber.MedicalId = Convert.ToInt32(Reader["MedicalId"]);
                viewChamber.MedicalName = Reader["MedicalName"].ToString();
                viewChamber.MedicalAddress = Reader["Address"].ToString();
                viewChamber.MedicalArea = Reader["Area"].ToString();
                viewChamber.MedicalCity = Reader["City"].ToString();
                viewChamber.Fee = Convert.ToInt32(Reader["DoctorFee"]);
                viewChamber.ReturningFee = Convert.ToInt32(Reader["DoctorReturningFee"]);
                viewChamber.Sat = Convert.ToBoolean(Reader["Sat"]);
                viewChamber.Sun = Convert.ToBoolean(Reader["Sun"]);
                viewChamber.Mon = Convert.ToBoolean(Reader["Mon"]);
                viewChamber.Tue = Convert.ToBoolean(Reader["Tue"]);
                viewChamber.Wed = Convert.ToBoolean(Reader["Wed"]);
                viewChamber.Thu = Convert.ToBoolean(Reader["Thu"]);
                viewChamber.Fri = Convert.ToBoolean(Reader["Fri"]);
                viewChamber.SatTime = Reader["SatTime"].ToString();
                viewChamber.SunTime = Reader["SunTime"].ToString();
                viewChamber.MonTime = Reader["MonTime"].ToString();
                viewChamber.TueTime = Reader["TueTime"].ToString();
                viewChamber.WedTime = Reader["WedTime"].ToString();
                viewChamber.ThuTime = Reader["ThuTime"].ToString();
                viewChamber.FriTime = Reader["FriTime"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return viewChamber;
        }

        public bool EditDoctorSchedule(AssignedDoctors aAssignedDoctor)
        {
            try
            {
                Query = "UPDATE AssignedDoctors SET Sat = @sat, Sun = @sun, Mon = @mon, Tue = @tue, Wed = @wed, Thu = @thu, Fri = @fri, SatTime = @satTime, SunTime = @sunTime, MonTime = @monTime, TueTime = @tueTime, WedTime = @wedTime, ThuTime = @thuTime, FriTime = @friTime WHERE MedicalId = @medicalId AND DoctorId = @doctorId";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("sat", aAssignedDoctor.Sat);
                Command.Parameters.AddWithValue("sun", aAssignedDoctor.Sun);
                Command.Parameters.AddWithValue("mon", aAssignedDoctor.Mon);
                Command.Parameters.AddWithValue("tue", aAssignedDoctor.Tue);
                Command.Parameters.AddWithValue("wed", aAssignedDoctor.Wed);
                Command.Parameters.AddWithValue("thu", aAssignedDoctor.Thu);
                Command.Parameters.AddWithValue("fri", aAssignedDoctor.Fri);
                Command.Parameters.AddWithValue("satTime", aAssignedDoctor.SatFromTime);
                Command.Parameters.AddWithValue("sunTime", aAssignedDoctor.SunFromTime);
                Command.Parameters.AddWithValue("monTime", aAssignedDoctor.MonFromTime);
                Command.Parameters.AddWithValue("tueTime", aAssignedDoctor.TueFromTime);
                Command.Parameters.AddWithValue("wedTime", aAssignedDoctor.WedFromTime);
                Command.Parameters.AddWithValue("thuTime", aAssignedDoctor.ThuFromTime);
                Command.Parameters.AddWithValue("friTime", aAssignedDoctor.FriFromTime);
                Command.Parameters.AddWithValue("medicalId", aAssignedDoctor.MedicalId);
                Command.Parameters.AddWithValue("doctorId", aAssignedDoctor.DoctorId);
                Connection.Open();
                bool rowAffected = Convert.ToBoolean(Command.ExecuteNonQuery());
                Connection.Close();
                return rowAffected;
            }
             catch (Exception ex)
            {
                Connection.Close();
                bool rowAffected = false;
                return rowAffected;
            }
        }

        public bool EditDoctorFees(AssignedDoctors aAssignedDoctor)
        {
            try
            {
                Query = "UPDATE AssignedDoctors SET DoctorFee = @fee, DoctorReturningFee = @returningFee WHERE MedicalId = @medicalId AND DoctorId = @doctorId";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("fee", aAssignedDoctor.DoctorFee);
                Command.Parameters.AddWithValue("returningFee", aAssignedDoctor.DoctorReturningFee);
                Command.Parameters.AddWithValue("medicalId", aAssignedDoctor.MedicalId);
                Command.Parameters.AddWithValue("doctorId", aAssignedDoctor.DoctorId);
                Connection.Open();
                bool rowAffected = Convert.ToBoolean(Command.ExecuteNonQuery());
                Connection.Close();
                return rowAffected;
            }
            catch (Exception ex)
            {
                Connection.Close();
                bool rowAffected = false;
                return rowAffected;
            }
        }

        public bool IsOldPasswordValid(int doctorId, string oldPassword)
        {
            Query = "SELECT * FROM Doctors WHERE Id = @doctorId AND Password = @oldPassword";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Command.Parameters.AddWithValue("oldPassword", oldPassword);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;
        }

        public int ChangeDoctorPassword(ChangePassword changePassword)
        {
            Query = "UPDATE Doctors SET Password = @newPassword WHERE Id = @doctorId AND Password = @oldPassword";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("newPassword", changePassword.NewPassword);
            Command.Parameters.AddWithValue("doctorId", changePassword.DoctorId);
            Command.Parameters.AddWithValue("oldPassword", changePassword.OldPassword);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public int AddNewChamber(DoctorChamber aDoctorChamber)
        {
            try
            {
                Query =
                    "INSERT INTO DoctorsChambers VALUES(@doctorId, @chamberName, @address, @area, @city, @mobileNo, @phoneNo, @fee, @returningFee, @sat, @sun, @mon, @tue, @wed, @thu, @fri, @satTime, @sunTime, @monTime, @tueTime,@wedTime,@thuTime,@friTime)";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("doctorId", aDoctorChamber.DoctorId);
                Command.Parameters.AddWithValue("chamberName", aDoctorChamber.ChamberName);
                Command.Parameters.AddWithValue("address", aDoctorChamber.Address);
                Command.Parameters.AddWithValue("area", aDoctorChamber.Area);
                Command.Parameters.AddWithValue("city", aDoctorChamber.City);
                Command.Parameters.AddWithValue("mobileNo", aDoctorChamber.ContactNo);
                Command.Parameters.AddWithValue("phoneNo", aDoctorChamber.PhoneNo);
                Command.Parameters.AddWithValue("fee", aDoctorChamber.Fee);
                Command.Parameters.AddWithValue("returningFee", aDoctorChamber.ReturningFee);
                Command.Parameters.AddWithValue("sat", aDoctorChamber.Sat);
                Command.Parameters.AddWithValue("sun", aDoctorChamber.Sun);
                Command.Parameters.AddWithValue("mon", aDoctorChamber.Mon);
                Command.Parameters.AddWithValue("tue", aDoctorChamber.Tue);
                Command.Parameters.AddWithValue("wed", aDoctorChamber.Wed);
                Command.Parameters.AddWithValue("thu", aDoctorChamber.Thu);
                Command.Parameters.AddWithValue("fri", aDoctorChamber.Fri);
                Command.Parameters.AddWithValue("satTime", aDoctorChamber.SatFromTime);
                Command.Parameters.AddWithValue("sunTime", aDoctorChamber.SunFromTime);
                Command.Parameters.AddWithValue("monTime", aDoctorChamber.MonFromTime);
                Command.Parameters.AddWithValue("tueTime", aDoctorChamber.TueFromTime);
                Command.Parameters.AddWithValue("wedTime", aDoctorChamber.WedFromTime);
                Command.Parameters.AddWithValue("thuTime", aDoctorChamber.ThuFromTime);
                Command.Parameters.AddWithValue("friTime", aDoctorChamber.FriFromTime);
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

        public List<DoctorChamber> GetDoctorOwnChamber(int id)
        {
            Query =
                "SELECT * FROM DoctorsChambers WHERE DoctorId = @id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("id", id);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<DoctorChamber> doctorChambers = new List<DoctorChamber>();
            while (Reader.Read())
            {
                DoctorChamber aChamber = new DoctorChamber();
                aChamber.Id = Convert.ToInt32(Reader["Id"]);
                aChamber.ChamberName = Reader["ChamberName"].ToString();
                aChamber.Address = Reader["Address"].ToString();
                aChamber.Area = Reader["Area"].ToString();
                aChamber.City = Reader["City"].ToString();
                aChamber.ContactNo = Reader["ContactNo"].ToString();
                aChamber.PhoneNo = Reader["PhoneNo"].ToString();
                aChamber.Fee = Convert.ToInt32(Reader["Fee"]);
                aChamber.ReturningFee = Convert.ToInt32(Reader["ReturningFee"]);
                aChamber.Sat = Convert.ToBoolean(Reader["Sat"]);
                aChamber.Sun = Convert.ToBoolean(Reader["Sun"]);
                aChamber.Mon = Convert.ToBoolean(Reader["Mon"]);
                aChamber.Tue = Convert.ToBoolean(Reader["Tue"]);
                aChamber.Wed = Convert.ToBoolean(Reader["Wed"]);
                aChamber.Thu = Convert.ToBoolean(Reader["Thu"]);
                aChamber.Fri = Convert.ToBoolean(Reader["Fri"]);
                aChamber.SatFromTime = Reader["SatTime"].ToString();
                aChamber.SunFromTime = Reader["SunTime"].ToString();
                aChamber.MonFromTime = Reader["MonTime"].ToString();
                aChamber.TueFromTime = Reader["TueTime"].ToString();
                aChamber.WedFromTime = Reader["WedTime"].ToString();
                aChamber.ThuFromTime = Reader["ThuTime"].ToString();
                aChamber.FriFromTime = Reader["FriTime"].ToString();
                doctorChambers.Add(aChamber);
            }

            if (doctorChambers.Count > 0)
            {
                foreach (DoctorChamber doctorChamber in doctorChambers)
                {
                    doctorChamber.ManageAppointment = GetChamberManageAppointmentInfo(id, doctorChamber.Id);
                }
            }
            Reader.Close();
            Connection.Close();
            return doctorChambers;
        }

        private ManageAppointment GetChamberManageAppointmentInfo(int doctorId, int doctorChamberId)
        {
            Connection.Close();
            Query = "SELECT * FROM ManageAppointVM WHERE DoctorId = @doctorId AND DoctorChamberId = @doctorChamberId AND Expired = 'False'";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Command.Parameters.AddWithValue("doctorChamberId", doctorChamberId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            ManageAppointment manageAppointment = new ManageAppointment();
            while (Reader.Read())
            {
                manageAppointment.Id = Convert.ToInt32(Reader["Id"]);
                manageAppointment.SatCapacity = Convert.ToInt32(Reader["SatCapacity"]);
                manageAppointment.SunCapacity = Convert.ToInt32(Reader["SunCapacity"]);
                manageAppointment.MonCapacity = Convert.ToInt32(Reader["MonCapacity"]);
                manageAppointment.TueCapacity = Convert.ToInt32(Reader["TueCapacity"]);
                manageAppointment.WedCapacity = Convert.ToInt32(Reader["WedCapacity"]);
                manageAppointment.ThuCapacity = Convert.ToInt32(Reader["ThuCapacity"]);
                manageAppointment.FriCapacity = Convert.ToInt32(Reader["FriCapacity"]);
                manageAppointment.SatAvailableAppoint = Convert.ToInt32(Reader["SatAvailableAppoint"]);
                manageAppointment.SunAvailableAppoint = Convert.ToInt32(Reader["SunAvailableAppoint"]);
                manageAppointment.MonAvailableAppoint = Convert.ToInt32(Reader["MonAvailableAppoint"]);
                manageAppointment.TueAvailableAppoint = Convert.ToInt32(Reader["TueAvailableAppoint"]);
                manageAppointment.WedAvailableAppoint = Convert.ToInt32(Reader["WedAvailableAppoint"]);
                manageAppointment.ThuAvailableAppoint = Convert.ToInt32(Reader["ThuAvailableAppoint"]);
                manageAppointment.FriAvailableAppoint = Convert.ToInt32(Reader["FriAvailableAppoint"]);
                manageAppointment.OnlineAppointment = Convert.ToBoolean(Reader["OnlineAppoint"]);
                manageAppointment.UsedAppoint = Convert.ToInt32(Reader["UsedAppoint"]);
                manageAppointment.TotalAvailableAppoint = Convert.ToInt32(Reader["TotalAvailableAppoint"]);
                manageAppointment.DoctorId = Convert.ToInt32(Reader["DoctorId"]);
                manageAppointment.MedicalId = Convert.ToInt32(Reader["MedicalId"]);
                manageAppointment.OrderedAppoint = Convert.ToInt32(Reader["AppointAmount"]);
                manageAppointment.RemainingAppoint = Convert.ToInt32(Reader["RemainingAppoint"]);
                manageAppointment.TotalAppoint = Convert.ToInt32(Reader["TotalAppoint"]);
                manageAppointment.OrderExpireDate = Convert.ToDateTime(Reader["ExpireDate"]);
                manageAppointment.Status = Reader["Status"].ToString();
            }
            return manageAppointment;
        }

        public DoctorChamber GetDoctorChamberInfo(int id, int doctorId)
        {
            Query =
                "SELECT * FROM DoctorsChambers WHERE Id = @id AND DoctorId = @doctorId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("id", id);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            DoctorChamber aChamber = new DoctorChamber();
            if (Reader.Read())
            {
                aChamber.Id = Convert.ToInt32(Reader["Id"]);
                aChamber.ChamberName = Reader["ChamberName"].ToString();
                aChamber.Address = Reader["Address"].ToString();
                aChamber.Area = Reader["Area"].ToString();
                aChamber.City = Reader["City"].ToString();
                aChamber.ContactNo = Reader["ContactNo"].ToString();
                aChamber.PhoneNo = Reader["PhoneNo"].ToString();
                aChamber.Fee = Convert.ToInt32(Reader["Fee"]);
                aChamber.ReturningFee = Convert.ToInt32(Reader["ReturningFee"]);
                aChamber.Sat = Convert.ToBoolean(Reader["Sat"]);
                aChamber.Sun = Convert.ToBoolean(Reader["Sun"]);
                aChamber.Mon = Convert.ToBoolean(Reader["Mon"]);
                aChamber.Tue = Convert.ToBoolean(Reader["Tue"]);
                aChamber.Wed = Convert.ToBoolean(Reader["Wed"]);
                aChamber.Thu = Convert.ToBoolean(Reader["Thu"]);
                aChamber.Fri = Convert.ToBoolean(Reader["Fri"]);
                aChamber.SatFromTime = Reader["SatTime"].ToString();
                aChamber.SunFromTime = Reader["SunTime"].ToString();
                aChamber.MonFromTime = Reader["MonTime"].ToString();
                aChamber.TueFromTime = Reader["TueTime"].ToString();
                aChamber.WedFromTime = Reader["WedTime"].ToString();
                aChamber.ThuFromTime = Reader["ThuTime"].ToString();
                aChamber.FriFromTime = Reader["FriTime"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return aChamber;
        }

        public int EditDoctorChamberFees(DoctorChamber doctorChamber)
        {
            try
            {
                Query =
                    "UPDATE DoctorsChambers SET ChamberName = @name, Address = @address, Area = @area, City = @city, ContactNo = @contactNo, PhoneNo = @phoneNo, Fee = @fee, ReturningFee = @returningFee WHERE Id = @id AND DoctorId = @doctorId";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("name", doctorChamber.ChamberName);
                Command.Parameters.AddWithValue("address", doctorChamber.Address);
                Command.Parameters.AddWithValue("area", doctorChamber.Area);
                Command.Parameters.AddWithValue("city", doctorChamber.City);
                Command.Parameters.AddWithValue("contactNo", doctorChamber.ContactNo);
                Command.Parameters.AddWithValue("phoneNo", doctorChamber.PhoneNo);
                Command.Parameters.AddWithValue("fee", doctorChamber.Fee);
                Command.Parameters.AddWithValue("returningFee", doctorChamber.ReturningFee);
                Command.Parameters.AddWithValue("id", doctorChamber.Id);
                Command.Parameters.AddWithValue("doctorId", doctorChamber.DoctorId);
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

        public int EditDoctorChamberSchedule(DoctorChamber doctorChamber)
        {
            try
            {
                Query = "UPDATE DoctorsChambers SET Sat = @sat, Sun = @sun, Mon = @mon, Tue = @tue, Wed = @wed, Thu = @thu, Fri = @fri, SatTime = @satTime, SunTime = @sunTime, MonTime = @monTime, TueTime = @tueTime, WedTime = @wedTime, ThuTime = @thuTime, FriTime = @friTime WHERE Id = @id AND DoctorId = @doctorId";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("sat", doctorChamber.Sat);
                Command.Parameters.AddWithValue("sun", doctorChamber.Sun);
                Command.Parameters.AddWithValue("mon", doctorChamber.Mon);
                Command.Parameters.AddWithValue("tue", doctorChamber.Tue);
                Command.Parameters.AddWithValue("wed", doctorChamber.Wed);
                Command.Parameters.AddWithValue("thu", doctorChamber.Thu);
                Command.Parameters.AddWithValue("fri", doctorChamber.Fri);
                Command.Parameters.AddWithValue("satTime", doctorChamber.SatFromTime);
                Command.Parameters.AddWithValue("sunTime", doctorChamber.SunFromTime);
                Command.Parameters.AddWithValue("monTime", doctorChamber.MonFromTime);
                Command.Parameters.AddWithValue("tueTime", doctorChamber.TueFromTime);
                Command.Parameters.AddWithValue("wedTime", doctorChamber.WedFromTime);
                Command.Parameters.AddWithValue("thuTime", doctorChamber.ThuFromTime);
                Command.Parameters.AddWithValue("friTime", doctorChamber.FriFromTime);
                Command.Parameters.AddWithValue("id", doctorChamber.Id);
                Command.Parameters.AddWithValue("doctorId", doctorChamber.DoctorId);
                Connection.Open();
                int rowAffected =Command.ExecuteNonQuery();
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

        public bool CheckManageAppointment(int doctorId, int? medicalId)
        {
            Query = "SELECT * FROM ManageAppointment WHERE DoctorId = @doctorId AND MedicalId = @medicalId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;
        }

        public int ExistAppointmentAvailability(bool availability,int doctorId, int? medicalId)
        {
            Query = "UPDATE ManageAppointment SET OnlineAppoint = @onlineAppoint WHERE DoctorId = @doctorId AND MedicalId = @medicalId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("onlineAppoint", availability);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public int AppointmentAvailability(ManageAppointment manageAppointment)
        {
            try
            {
                Query =
                    "INSERT INTO ManageAppointment VALUES(@satCapacity, @sunCapacity, @monCapacity, @tueCapacity, @wedCapacity, @thuCapacity, @friCapacity, @satAvailableAppoint, @sunAvailableAppoint, @monAvailableAppoint, @tueAvailableAppoint, @wedAvailableAppoint, @thuAvailableAppoint, @friAvailableAppoint, @onlineAppoint, @usedAppoint, @totalAvailable, @doctorId, @medicalId, @doctorChamberId)";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("satCapacity", manageAppointment.SatCapacity);
                Command.Parameters.AddWithValue("sunCapacity", manageAppointment.SunCapacity);
                Command.Parameters.AddWithValue("monCapacity", manageAppointment.MonCapacity);
                Command.Parameters.AddWithValue("tueCapacity", manageAppointment.TueCapacity);
                Command.Parameters.AddWithValue("wedCapacity", manageAppointment.WedCapacity);
                Command.Parameters.AddWithValue("thuCapacity", manageAppointment.ThuCapacity);
                Command.Parameters.AddWithValue("friCapacity", manageAppointment.FriCapacity);
                Command.Parameters.AddWithValue("satAvailableAppoint", manageAppointment.SatAvailableAppoint);
                Command.Parameters.AddWithValue("sunAvailableAppoint", manageAppointment.SunAvailableAppoint);
                Command.Parameters.AddWithValue("monAvailableAppoint", manageAppointment.MonAvailableAppoint);
                Command.Parameters.AddWithValue("tueAvailableAppoint", manageAppointment.TueAvailableAppoint);
                Command.Parameters.AddWithValue("wedAvailableAppoint", manageAppointment.WedAvailableAppoint);
                Command.Parameters.AddWithValue("thuAvailableAppoint", manageAppointment.ThuAvailableAppoint);
                Command.Parameters.AddWithValue("friAvailableAppoint", manageAppointment.FriAvailableAppoint);
                Command.Parameters.AddWithValue("onlineAppoint", manageAppointment.OnlineAppointment);
                Command.Parameters.AddWithValue("usedAppoint", manageAppointment.UsedAppoint);
                Command.Parameters.AddWithValue("totalAvailable", manageAppointment.TotalAvailableAppoint);
                Command.Parameters.AddWithValue("doctorId", manageAppointment.DoctorId);
                Command.Parameters.AddWithValue("medicalId", manageAppointment.MedicalId);
                Command.Parameters.AddWithValue("doctorChamberId", manageAppointment.DoctorChamberId);
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

        public int GetManageId(int doctorId, int? medicalId, int? doctorChamberId)
        {
            int id = 0;
            if (doctorId > 0 && medicalId > 0)
            {
                Query = "SELECT Id FROM ManageAppointment WHERE DoctorId = @doctorId AND MedicalId = @medicalId";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("doctorId", doctorId);
                Command.Parameters.AddWithValue("medicalId", medicalId);
            }
            if (doctorId > 0 && doctorChamberId > 0)
            {
                Query = "SELECT Id FROM ManageAppointment WHERE DoctorId = @doctorId AND DoctorChamberId = @doctorChamberId";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("doctorId", doctorId);
                Command.Parameters.AddWithValue("doctorChamberId", doctorChamberId);
            }
            Connection.Open();
            Reader = Command.ExecuteReader();
            if (Reader.Read())
            {
                id = Convert.ToInt32(Reader["Id"]);
            }
            Reader.Close();
            Connection.Close();
            return id;
        }

        public bool CheckChamberManageAppointment(int doctorId, int? doctorChamberId)
        {
            Query = "SELECT * FROM ManageAppointment WHERE DoctorId = @doctorId AND DoctorChamberId = @doctorChamberId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Command.Parameters.AddWithValue("doctorChamberId", doctorChamberId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;
        }

        public int ExistChamberAppointmentAvailability(bool availability, int doctorId, int? doctorChamberId)
        {
            Query = "UPDATE ManageAppointment SET OnlineAppoint = @availability WHERE DoctorId = @doctorId AND DoctorChamberId = @doctorChamberId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("availability", availability);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Command.Parameters.AddWithValue("doctorChamberId", doctorChamberId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public int ChamberAppointmentAvailability(ManageAppointment manageAppointment)
        {
            try
            {
                Query =
                    "INSERT INTO ManageAppointment VALUES(@satCapacity, @sunCapacity, @monCapacity, @tueCapacity, @wedCapacity, @thuCapacity, @friCapacity, @satAvailableAppoint, @sunAvailableAppoint, @monAvailableAppoint, @tueAvailableAppoint, @wedAvailableAppoint, @thuAvailableAppoint, @friAvailableAppoint, @onlineAppoint, @usedAppoint, @totalAvailable, @doctorId, @medicalId, @doctorChamberId)";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("satCapacity", manageAppointment.SatCapacity);
                Command.Parameters.AddWithValue("sunCapacity", manageAppointment.SunCapacity);
                Command.Parameters.AddWithValue("monCapacity", manageAppointment.MonCapacity);
                Command.Parameters.AddWithValue("tueCapacity", manageAppointment.TueCapacity);
                Command.Parameters.AddWithValue("wedCapacity", manageAppointment.WedCapacity);
                Command.Parameters.AddWithValue("thuCapacity", manageAppointment.ThuCapacity);
                Command.Parameters.AddWithValue("friCapacity", manageAppointment.FriCapacity);
                Command.Parameters.AddWithValue("satAvailableAppoint", manageAppointment.SatAvailableAppoint);
                Command.Parameters.AddWithValue("sunAvailableAppoint", manageAppointment.SunAvailableAppoint);
                Command.Parameters.AddWithValue("monAvailableAppoint", manageAppointment.MonAvailableAppoint);
                Command.Parameters.AddWithValue("tueAvailableAppoint", manageAppointment.TueAvailableAppoint);
                Command.Parameters.AddWithValue("wedAvailableAppoint", manageAppointment.WedAvailableAppoint);
                Command.Parameters.AddWithValue("thuAvailableAppoint", manageAppointment.ThuAvailableAppoint);
                Command.Parameters.AddWithValue("friAvailableAppoint", manageAppointment.FriAvailableAppoint);
                Command.Parameters.AddWithValue("onlineAppoint", manageAppointment.OnlineAppointment);
                Command.Parameters.AddWithValue("usedAppoint", manageAppointment.UsedAppoint);
                Command.Parameters.AddWithValue("totalAvailable", manageAppointment.TotalAvailableAppoint);
                Command.Parameters.AddWithValue("doctorId", manageAppointment.DoctorId);
                Command.Parameters.AddWithValue("medicalId", manageAppointment.MedicalId);
                Command.Parameters.AddWithValue("doctorChamberId", manageAppointment.DoctorChamberId);
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

        public int UpdateManageAppointment(ManageAppointment manageAppointment)
        {
            int rowAffected = 0;
            try
            {
                if (manageAppointment.DoctorChamberId > 0)
                {
                    Query = "UPDATE ManageAppointment SET SatCapacity = @satCapacity, SunCapacity = @sunCapacity, MonCapacity = @monCapacity, TueCapacity = @tueCapacity, WedCapacity = @wedCapacity, ThuCapacity = @thuCapacity, FriCapacity = @friCapacity," +
                        "SatAvailableAppoint = @satAvailable, SunAvailableAppoint = @sunAvailable, MonAvailableAppoint = @monAvailable, TueAvailableAppoint = @tueAvailable, WedAvailableAppoint = @wedAvailable, ThuAvailableAppoint = @thuAvailable, FriAvailableAppoint = @friAvailable," +
                        " OnlineAppoint = @onlineAppoint, UsedAppoint = @usedAppoint, TotalAvailableAppoint = @totalAvailable WHERE DoctorId = @doctorId AND DoctorChamberId = @doctorChamberId";
                    Command = new SqlCommand(Query, Connection);
                    Command.Parameters.AddWithValue("satCapacity", manageAppointment.SatCapacity);
                    Command.Parameters.AddWithValue("sunCapacity", manageAppointment.SunCapacity);
                    Command.Parameters.AddWithValue("monCapacity", manageAppointment.MonCapacity);
                    Command.Parameters.AddWithValue("tueCapacity", manageAppointment.TueCapacity);
                    Command.Parameters.AddWithValue("wedCapacity", manageAppointment.WedCapacity);
                    Command.Parameters.AddWithValue("thuCapacity", manageAppointment.ThuCapacity);
                    Command.Parameters.AddWithValue("friCapacity", manageAppointment.FriCapacity);
                    Command.Parameters.AddWithValue("satAvailable", manageAppointment.SatAvailableAppoint);
                    Command.Parameters.AddWithValue("sunAvailable", manageAppointment.SunAvailableAppoint);
                    Command.Parameters.AddWithValue("monAvailable", manageAppointment.MonAvailableAppoint);
                    Command.Parameters.AddWithValue("tueAvailable", manageAppointment.TueAvailableAppoint);
                    Command.Parameters.AddWithValue("wedAvailable", manageAppointment.WedAvailableAppoint);
                    Command.Parameters.AddWithValue("thuAvailable", manageAppointment.ThuAvailableAppoint);
                    Command.Parameters.AddWithValue("friAvailable", manageAppointment.FriAvailableAppoint);
                    Command.Parameters.AddWithValue("onlineAppoint", manageAppointment.OnlineAppointment);
                    Command.Parameters.AddWithValue("usedAppoint", manageAppointment.UsedAppoint);
                    Command.Parameters.AddWithValue("totalAvailable", manageAppointment.TotalAvailableAppoint);
                    Command.Parameters.AddWithValue("doctorId", manageAppointment.DoctorId);
                    Command.Parameters.AddWithValue("doctorChamberId", manageAppointment.DoctorChamberId);
                    Connection.Open();
                    rowAffected = Command.ExecuteNonQuery();
                    Connection.Close();
                    return rowAffected;
                }
                if(manageAppointment.MedicalId > 0)
                {
                    Query = "UPDATE ManageAppointment SET SatCapacity = @satCapacity, SunCapacity = @sunCapacity, MonCapacity = @monCapacity, TueCapacity = @tueCapacity, WedCapacity = @wedCapacity, ThuCapacity = @thuCapacity, FriCapacity = @friCapacity," +
                        "SatAvailableAppoint = @satAvailable, SunAvailableAppoint = @sunAvailable, MonAvailableAppoint = @monAvailable, TueAvailableAppoint = @tueAvailable, WedAvailableAppoint = @wedAvailable, ThuAvailableAppoint = @thuAvailable, FriAvailableAppoint = @friAvailable," +
                        " OnlineAppoint = @onlineAppoint, UsedAppoint = @usedAppoint, TotalAvailableAppoint = @totalAvailable WHERE DoctorId = @doctorId AND MedicalId = @medicalId";
                    Command = new SqlCommand(Query, Connection);
                    Command.Parameters.AddWithValue("satCapacity", manageAppointment.SatCapacity);
                    Command.Parameters.AddWithValue("sunCapacity", manageAppointment.SunCapacity);
                    Command.Parameters.AddWithValue("monCapacity", manageAppointment.MonCapacity);
                    Command.Parameters.AddWithValue("tueCapacity", manageAppointment.TueCapacity);
                    Command.Parameters.AddWithValue("wedCapacity", manageAppointment.WedCapacity);
                    Command.Parameters.AddWithValue("thuCapacity", manageAppointment.ThuCapacity);
                    Command.Parameters.AddWithValue("friCapacity", manageAppointment.FriCapacity);
                    Command.Parameters.AddWithValue("satAvailable", manageAppointment.SatAvailableAppoint);
                    Command.Parameters.AddWithValue("sunAvailable", manageAppointment.SunAvailableAppoint);
                    Command.Parameters.AddWithValue("monAvailable", manageAppointment.MonAvailableAppoint);
                    Command.Parameters.AddWithValue("tueAvailable", manageAppointment.TueAvailableAppoint);
                    Command.Parameters.AddWithValue("wedAvailable", manageAppointment.WedAvailableAppoint);
                    Command.Parameters.AddWithValue("thuAvailable", manageAppointment.ThuAvailableAppoint);
                    Command.Parameters.AddWithValue("friAvailable", manageAppointment.FriAvailableAppoint);
                    Command.Parameters.AddWithValue("onlineAppoint", manageAppointment.OnlineAppointment);
                    Command.Parameters.AddWithValue("usedAppoint", manageAppointment.UsedAppoint);
                    Command.Parameters.AddWithValue("totalAvailable", manageAppointment.TotalAvailableAppoint);
                    Command.Parameters.AddWithValue("doctorId", manageAppointment.DoctorId);
                    Command.Parameters.AddWithValue("medicalId", manageAppointment.MedicalId);
                    Connection.Open();
                    rowAffected = Command.ExecuteNonQuery();
                    Connection.Close();
                    return rowAffected;
                }
                return rowAffected;
            }
            catch (Exception ex)
            {
                return rowAffected;
            }
        }
        public bool IsDoctorMobileNoExists(string mobileNo)
        {
            Query = "SELECT * FROM Doctors WHERE MobileNo = @mobileNo";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("mobileNo", mobileNo);
            Command.Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRows = Reader.HasRows;
            Reader.Close();
            Command.Connection.Close();
            return hasRows;
        }

        public Doctors IsLoginVerified(string aLoginLoginId)
        {
            Query = "SELECT PasswordVerified, Status FROM Doctors WHERE MobileNo = @aLoginLoginId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("aLoginLoginId", aLoginLoginId);
            Command.Connection.Open();
            Reader = Command.ExecuteReader();
            Doctors doctor  = null;
            if (Reader.Read())
            {
                doctor = new Doctors();
                doctor.PasswordVerified = Convert.ToBoolean(Reader["PasswordVerified"]);
                doctor.Status = Reader["Status"].ToString();
            }
            Reader.Close();
            Command.Connection.Close();
            return doctor;
        }

        public int ChangeTemporaryPassword(ChangePassword changePassword)
        {
            Query = "UPDATE Doctors SET Password = @newPassword, PasswordVerified = @passwordVerified WHERE MobileNo = @doctorLoginId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("newPassword", changePassword.NewPassword);
            Command.Parameters.AddWithValue("passwordVerified", changePassword.PasswordVerified);
            Command.Parameters.AddWithValue("doctorLoginId", changePassword.DoctorLoginId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public bool DeleteDoctorChamber(int chamberId, int doctorId)
        {
            Query = "DELETE FROM DoctorsChambers WHERE Id = @chamberId AND DoctorId = @doctorId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("chamberId", chamberId);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Connection.Open();
            bool rowAffected = Convert.ToBoolean(Command.ExecuteNonQuery());
            Connection.Close();
            return rowAffected;
        }

        public bool DeleteMedicalChamber(int medicalId, int doctorId)
        {
            Query = "DELETE FROM AssignedDoctors WHERE MedicalId = @medicalId AND DoctorId = @doctorId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Connection.Open();
            bool rowAffected = Convert.ToBoolean(Command.ExecuteNonQuery());
            Connection.Close();
            return rowAffected;
        }

        public bool IsDoctorExistInMedical(string doctorMobileNo, string doctorBmdcReg)
        {
            Query = "SELECT * FROM ViewDoctors WHERE MobileNo = @doctorMobileNo OR BmdcReg = @doctorBmdcReg";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("doctorMobileNo", doctorMobileNo);
            Command.Parameters.AddWithValue("doctorBmdcReg", doctorBmdcReg);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;
        }

        public int SaveDoctor(Doctors aDoctor)
        {
            Query =
                "INSERT INTO Doctors VALUES(@role, @name, @gender, @title, @speciality, @email, @mobileNo, @password, @phoneNo, @bmdcReg, @otherSpecification, @imagePath, @passwordVerified, @status, @accountCreatedDate)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("role", aDoctor.Role);
            Command.Parameters.AddWithValue("name", aDoctor.Name);
            Command.Parameters.AddWithValue("gender", aDoctor.Gender);
            Command.Parameters.AddWithValue("title", aDoctor.Title);
            Command.Parameters.AddWithValue("speciality", aDoctor.Speciality);
            Command.Parameters.AddWithValue("email", aDoctor.Email);
            Command.Parameters.AddWithValue("mobileNo", aDoctor.MobileNo);
            Command.Parameters.AddWithValue("password", aDoctor.Password);
            Command.Parameters.AddWithValue("phoneNo", aDoctor.PhoneNo);
            Command.Parameters.AddWithValue("bmdcReg", aDoctor.BmdcReg);
            Command.Parameters.AddWithValue("otherSpecification", aDoctor.OtherSpecification);
            Command.Parameters.AddWithValue("imagePath", aDoctor.ImagePath);
            Command.Parameters.AddWithValue("passwordVerified", aDoctor.PasswordVerified);
            Command.Parameters.AddWithValue("status", aDoctor.Status);
            Command.Parameters.AddWithValue("accountCreatedDate", aDoctor.AccountCreatedDate);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public bool IsDoctorExists(string mobileNo, string bmdcReg)
        {
            Query = "SELECT * FROM Doctors WHERE MobileNo = @mobileNo OR BmdcReg = @bmdcReg";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("mobileNo", mobileNo);
            Command.Parameters.AddWithValue("bmdcReg", bmdcReg);
            Command.Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRows = Reader.HasRows;
            Reader.Close();
            Command.Connection.Close();
            return hasRows;
        }

        public int CreateOrderForChamber(DoctorAppointOrder doctorAppointOrder)
        {
            Query =
                "INSERT INTO DoctorAppointOrder VALUES(@manageAppointId, @appointAmount, @totalPrice, @paymentMethod, @trxnId, @orderDate, @expireDate, @remainingAppoint, @totalAppoint, @expired, @status)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("manageAppointId", doctorAppointOrder.ManageAppointId);
            Command.Parameters.AddWithValue("appointAmount", doctorAppointOrder.AppointAmount);
            Command.Parameters.AddWithValue("totalPrice", doctorAppointOrder.TotalPrice);
            Command.Parameters.AddWithValue("paymentMethod", doctorAppointOrder.PaymentMethod);
            Command.Parameters.AddWithValue("trxnId", doctorAppointOrder.TrnxId);
            Command.Parameters.AddWithValue("orderDate", doctorAppointOrder.OrderDate);
            Command.Parameters.AddWithValue("expireDate", doctorAppointOrder.ExpireDate);
            Command.Parameters.AddWithValue("remainingAppoint", doctorAppointOrder.RemainingAppoint);
            Command.Parameters.AddWithValue("totalAppoint", doctorAppointOrder.TotalAppoint);
            Command.Parameters.AddWithValue("expired", doctorAppointOrder.Expired);
            Command.Parameters.AddWithValue("status", doctorAppointOrder.Status);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public List<ChambersForOrder> GetChambersForOrder(int doctorId)
        {
            List<ChambersForOrder> chambersForOrders = new List<ChambersForOrder>();
            Query = "SELECT * FROM ChambersAppointOrder WHERE DoctorId = @doctorId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                ChambersForOrder aChambersForOrder = new ChambersForOrder();
                aChambersForOrder.ManageId = Convert.ToInt32(Reader["Id"]);
                aChambersForOrder.ChamberName = Reader["MedicalName"].ToString();
                chambersForOrders.Add(aChambersForOrder);
            }
            Reader.Close();
            Connection.Close();
            if (chambersForOrders != null)
            {
                Query = "SELECT * FROM DoctorChambersAppointOrder WHERE DoctorId = @doctorId";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("doctorId", doctorId);
                Connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    ChambersForOrder aChambersForOrder = new ChambersForOrder();
                    aChambersForOrder.ManageId = Convert.ToInt32(Reader["Id"]);
                    aChambersForOrder.ChamberName = Reader["ChamberName"].ToString();
                    chambersForOrders.Add(aChambersForOrder);
                }
                Reader.Close();
                Connection.Close();
            }
            return chambersForOrders;
        }

        public int GetRemainingAppoint(int manageAppointId)
        {
            int remainingAppoints = 0;
            Query = "SELECT RemainingAppoint FROM DoctorAppointOrder WHERE ManageAppointId = @manageId AND Expired = 'False'";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("manageId", manageAppointId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            if (Reader.Read())
            {
                remainingAppoints = Convert.ToInt32(Reader["RemainingAppoint"]);
            }
            Reader.Close();
            Connection.Close();
            return remainingAppoints;
        }

        public int SaveOrderAppointment(DoctorAppointOrder doctorAppointOrder)
        {
            int disabled = DisableOldOrder(doctorAppointOrder.ManageAppointId);
            int rowAffected = 0;
            if (disabled > 0)
            {
                Query =
                    "INSERT INTO DoctorAppointOrder VALUES(@manageAppointId, @appointAmount, @totalPrice, @paymentMethod, @trxnId, @orderDate, @expireDate, @remainingAppoint, @totalAppoint, @expired, @status)";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("manageAppointId", doctorAppointOrder.ManageAppointId);
                Command.Parameters.AddWithValue("appointAmount", doctorAppointOrder.AppointAmount);
                Command.Parameters.AddWithValue("totalPrice", doctorAppointOrder.TotalPrice);
                Command.Parameters.AddWithValue("paymentMethod", doctorAppointOrder.PaymentMethod);
                Command.Parameters.AddWithValue("trxnId", doctorAppointOrder.TrnxId);
                Command.Parameters.AddWithValue("orderDate", doctorAppointOrder.OrderDate);
                Command.Parameters.AddWithValue("expireDate", doctorAppointOrder.ExpireDate);
                Command.Parameters.AddWithValue("remainingAppoint", doctorAppointOrder.RemainingAppoint);
                Command.Parameters.AddWithValue("totalAppoint", doctorAppointOrder.TotalAppoint);
                Command.Parameters.AddWithValue("expired", doctorAppointOrder.Expired);
                Command.Parameters.AddWithValue("status", doctorAppointOrder.Status);
                Connection.Open();
                rowAffected = Command.ExecuteNonQuery();
                Connection.Close();
                return rowAffected;
            }
            return rowAffected;
        }

        private int DisableOldOrder(int manageAppointId)
        {
            Query = "UPDATE DoctorAppointOrder SET Expired = 'True' WHERE ManageAppointId = @manageId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("manageId", manageAppointId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public List<OrderStatus> GetOrderStatus(int doctorId)
        {
            Query = "SELECT * FROM DoctorChamberOrder WHERE DoctorId = @doctorId ORDER BY Status DESC";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<OrderStatus> orderStatuses = new List<OrderStatus>();
            while (Reader.Read())
            {
                OrderStatus orderStatus = new OrderStatus();
                orderStatus.ChamberName = Reader["MedicalName"].ToString();
                if (orderStatus.ChamberName == "")
                {
                    orderStatus.ChamberName = Reader["ChamberName"].ToString();
                }
                orderStatus.AppointAmount = Convert.ToInt32(Reader["AppointAmount"]);
                orderStatus.TotalPrice = Convert.ToInt32(Reader["TotalPrice"]);
                orderStatus.TrxId = Reader["TrxId"].ToString();
                orderStatus.Status = Reader["Status"].ToString();
                orderStatuses.Add(orderStatus);
            }
            Reader.Close();
            Connection.Close();
            return orderStatuses;
        }

        public ManageAppointment GetDoctorChamberManageInfo(int doctorId, int? medicalId, int? doctorChamberId)
        {
            if (medicalId > 0)
            {
                Query = "SELECT * FROM ManageAppointment WHERE DoctorId = @doctorId AND MedicalId =@medicalId";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("doctorId", doctorId);
                Command.Parameters.AddWithValue("medicalId", medicalId);
                Connection.Open();
            }
            if (doctorChamberId > 0)
            {
                Query = "SELECT * FROM ManageAppointment WHERE DoctorId = @doctorId AND DoctorChamberId =@doctorChamberId";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("doctorId", doctorId);
                Command.Parameters.AddWithValue("doctorChamberId", doctorChamberId);
                Connection.Open();
            }
            Reader = Command.ExecuteReader();
            ManageAppointment manageAppointment = null;
            while (Reader.Read())
            {
                manageAppointment = new ManageAppointment();
                manageAppointment.Id = Convert.ToInt32(Reader["Id"]);
                manageAppointment.SatCapacity = Convert.ToInt32(Reader["SatCapacity"]);
                manageAppointment.SunCapacity = Convert.ToInt32(Reader["SunCapacity"]);
                manageAppointment.MonCapacity = Convert.ToInt32(Reader["MonCapacity"]);
                manageAppointment.TueCapacity = Convert.ToInt32(Reader["TueCapacity"]);
                manageAppointment.WedCapacity = Convert.ToInt32(Reader["WedCapacity"]);
                manageAppointment.ThuCapacity = Convert.ToInt32(Reader["ThuCapacity"]);
                manageAppointment.FriCapacity = Convert.ToInt32(Reader["FriCapacity"]);
                manageAppointment.SatAvailableAppoint = Convert.ToInt32(Reader["SatAvailableAppoint"]);
                manageAppointment.SunAvailableAppoint = Convert.ToInt32(Reader["SunAvailableAppoint"]);
                manageAppointment.MonAvailableAppoint = Convert.ToInt32(Reader["MonAvailableAppoint"]);
                manageAppointment.TueAvailableAppoint = Convert.ToInt32(Reader["TueAvailableAppoint"]);
                manageAppointment.WedAvailableAppoint = Convert.ToInt32(Reader["WedAvailableAppoint"]);
                manageAppointment.ThuAvailableAppoint = Convert.ToInt32(Reader["ThuAvailableAppoint"]);
                manageAppointment.FriAvailableAppoint = Convert.ToInt32(Reader["FriAvailableAppoint"]);
                manageAppointment.OnlineAppointment = Convert.ToBoolean(Reader["OnlineAppoint"]);
                manageAppointment.UsedAppoint = Convert.ToInt32(Reader["UsedAppoint"]);
                manageAppointment.TotalAvailableAppoint = Convert.ToInt32(Reader["TotalAvailableAppoint"]);
                manageAppointment.DoctorId = Convert.ToInt32(Reader["DoctorId"]);
                manageAppointment.MedicalId = Convert.ToInt32(Reader["MedicalId"]);
            }
            Reader.Close();
            Connection.Close();
            return manageAppointment;
        }

        public List<Appointments> GetUserAppointments(int doctorId)
        {
            Query = "SELECT DISTINCT ChamberId, ChamberName FROM BookAppointment WHERE DoctorId = @doctorId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Appointments> appointments = new List<Appointments>();
            while (Reader.Read())
            {
                Appointments appointment = new Appointments();
                appointment.ChamberId = Convert.ToInt32(Reader["ChamberId"]);
                appointment.ChamberName = Reader["ChamberName"].ToString();
                appointments.Add(appointment);
            }
            if (appointments.Count > 0)
            {
                foreach (Appointments appointment in appointments)
                {
                    appointment.BookAppointments = GetAvailableAppointments(appointment.ChamberId, doctorId);
                }
            }
            Reader.Close();
            Connection.Close();
            return appointments;
        }

        public List<BookAppointment> GetAvailableAppointments(int chamberId, int doctorId)
        {
            Connection.Close();
            Query = "SELECT * FROM BookAppointment WHERE ChamberId = @chamberId AND DoctorId = @doctorId AND Status = 'Accepted'";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("chamberId", chamberId);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<BookAppointment> bookAppointments = new List<BookAppointment>();
            while (Reader.Read())
            {
                BookAppointment bookAppointment = new BookAppointment();
                bookAppointment.BookAppointId = Convert.ToInt32(Reader["Id"]);
                bookAppointment.ChamberId = Convert.ToInt32(Reader["ChamberId"]);
                bookAppointment.UserId = Convert.ToInt32(Reader["UserId"]);
                bookAppointment.UserName = Reader["UserName"].ToString();
                bookAppointment.MobileNo = Reader["MobileNo"].ToString();
                bookAppointment.Schedule = Reader["Schedule"].ToString();
                bookAppointment.AppointDate = Reader["AppointDate"].ToString();
                bookAppointment.SerialNo = Convert.ToInt32(Reader["SerialNo"]);
                DateTime appointDate = Convert.ToDateTime(bookAppointment.AppointDate);
                if (DateTime.Now.Date <= appointDate)
                {
                    bookAppointments.Add(bookAppointment);
                }
            }
            return bookAppointments;
        }

        public List<BookAppointment> GetAllUserAppointments(int doctorId)
        {
            Query = "SELECT * FROM BookAppointment WHERE DoctorId = @doctorId AND Status = 'Accepted' ORDER BY AppointDate";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<BookAppointment> bookAppointments = new List<BookAppointment>();
            while (Reader.Read())
            {
                BookAppointment bookAppointment = new BookAppointment();
                bookAppointment.UserName = Reader["UserName"].ToString();
                bookAppointment.ChamberName = Reader["ChamberName"].ToString();
                bookAppointment.MobileNo = Reader["MobileNo"].ToString();
                bookAppointment.Schedule = Reader["Schedule"].ToString();
                bookAppointment.AppointDate = Reader["AppointDate"].ToString();
                bookAppointment.SerialNo = Convert.ToInt32(Reader["SerialNo"]);
                bookAppointments.Add(bookAppointment);
            }
            Reader.Close();
            Connection.Close();
            return bookAppointments;
        }
        public int CancelUserAppointment(int id, int doctorId, int userId, string mobileNo)
        {
            Query =
                "UPDATE BookAppointment SET Status = 'Canceled' WHERE Id = @id AND DoctorId = @doctorId AND UserId = @userId AND MobileNo = @mobileNo";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("id", id);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Command.Parameters.AddWithValue("userId", userId);
            Command.Parameters.AddWithValue("mobileNo", mobileNo);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
    }
}