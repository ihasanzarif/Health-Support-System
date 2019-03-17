using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HealthSupportApp.Models;
using HealthSupportApp.Models.MedicalModel;

namespace HealthSupportApp.Gateway.MedicalGateway
{
    public class MedicalGateway : ParentGateway
    {
        public int Save(MedicalAccount aMedicalAccount)
        {
            Query = "INSERT INTO MedicalAccounts VALUES(@role, @medicalType, @medicalName, @contactPersonName, @contactPersonPosition, @contactPersonPhoneNo, @medicalEmail, @medicalContactNo1, @medicalContactNo2, @address, @area, @city, @ambulanceService, @ambulanceContact, @password, @isEmailVerified, @activationCode, @status, @accountCreatedDate)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("role", aMedicalAccount.Role);
            Command.Parameters.AddWithValue("medicalType", aMedicalAccount.MedicalType);
            Command.Parameters.AddWithValue("medicalName", aMedicalAccount.MedicalName);
            Command.Parameters.AddWithValue("contactPersonName", aMedicalAccount.ContactPersonName);
            Command.Parameters.AddWithValue("contactPersonPosition", aMedicalAccount.ContactPersonPosition);
            Command.Parameters.AddWithValue("contactPersonPhoneNo", aMedicalAccount.ContactPersonPhoneNo);
            Command.Parameters.AddWithValue("medicalEmail", aMedicalAccount.MedicalEmail);
            Command.Parameters.AddWithValue("medicalContactNo1", aMedicalAccount.MedicalContactNo1);
            Command.Parameters.AddWithValue("medicalContactNo2", aMedicalAccount.MedicalContactNo2);
            Command.Parameters.AddWithValue("address", aMedicalAccount.Address);
            Command.Parameters.AddWithValue("area", aMedicalAccount.Area);
            Command.Parameters.AddWithValue("city", aMedicalAccount.City);
            Command.Parameters.AddWithValue("ambulanceService", aMedicalAccount.AmbulanceService);
            Command.Parameters.AddWithValue("ambulanceContact", aMedicalAccount.AmbulanceContact);
            Command.Parameters.AddWithValue("password", aMedicalAccount.Password);
            Command.Parameters.AddWithValue("isEmailVerified", aMedicalAccount.IsEmailVerified);
            Command.Parameters.AddWithValue("activationCode", aMedicalAccount.ActivationCode);
            Command.Parameters.AddWithValue("status", aMedicalAccount.Status);
            Command.Parameters.AddWithValue("accountCreatedDate", aMedicalAccount.AccountCreatedDate);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public bool IsEmailExists(string medicalEmail)
        {
            Query = "SELECT * FROM MedicalAccounts WHERE MedicalEmail= @medicalEmail";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("medicalEmail", medicalEmail);
            Command.Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Command.Connection.Close();
            return hasRow;
        }

        public bool IsMobileNoExists(string medicalContact)
        {
            Query = "SELECT * FROM MedicalAccounts WHERE MedicalContact1= @medicalContact";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("medicalContact", medicalContact);
            Command.Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;
        }

        public bool IsValid(string medicalEmail, string password)
        {
            Query = "SELECT * FROM MedicalAccounts WHERE MedicalEmail= @medicalEmail AND Password=@password";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("medicalEmail", medicalEmail);
            Command.Parameters.AddWithValue("password", password);
            Command.Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRows = Reader.HasRows;
            Reader.Close();
            Command.Connection.Close();
            return hasRows;
        }

        public MedicalAccount GetMedicalData(string username)
        {
            Query = "SELECT * FROM MedicalAccounts WHERE MedicalEmail= @username";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("username", username);
            Command.Connection.Open();
            Reader = Command.ExecuteReader();

            MedicalAccount aMedicalAccount = null;
            if (Reader.HasRows)
            {
                Reader.Read();
                aMedicalAccount = new MedicalAccount();
                aMedicalAccount.MedicalId = Convert.ToInt32(Reader["MedicalId"]);
                aMedicalAccount.MedicalType = Reader["MedicalType"].ToString();
                aMedicalAccount.MedicalName = Reader["MedicalName"].ToString();
                aMedicalAccount.ContactPersonName = Reader["ContactPersonName"].ToString();
                aMedicalAccount.ContactPersonPosition = Reader["ContactPersonPosition"].ToString();
                aMedicalAccount.ContactPersonPhoneNo = Reader["ContactPersonPhoneNo"].ToString();
                aMedicalAccount.MedicalEmail = Reader["MedicalEmail"].ToString();
                aMedicalAccount.MedicalContactNo1 = Reader["MedicalContact1"].ToString();
                aMedicalAccount.MedicalContactNo2 = Reader["MedicalContact2"].ToString();
                aMedicalAccount.Address = Reader["Address"].ToString();
                aMedicalAccount.Area = Reader["Area"].ToString();
                aMedicalAccount.City = Reader["City"].ToString();
                aMedicalAccount.AmbulanceService = Reader["AmbulanceService"].ToString();
                aMedicalAccount.AmbulanceContact = Reader["AmbulanceContact"].ToString();
            }
            Reader.Close();
            Command.Connection.Close();
            return aMedicalAccount;
        }

        public bool IsActivationCodeValid(string id)
        {
            bool hasRow = false;
            try
            {
                Query = "SELECT * FROM MedicalAccounts WHERE ActivationCode = @id";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("id", id);
                Connection.Open();
                Reader = Command.ExecuteReader();
                hasRow = Reader.HasRows;
                Reader.Close();
                Connection.Close();
                return hasRow;
            }
            catch (Exception ex)
            {
                Connection.Close();
                return hasRow;
            }
        }

        public bool UpdateEmailVerification(bool isEmailVerified, string id)
        {
            Query = "UPDATE MedicalAccounts SET IsEmailVerified = @isEmailVerified WHERE ActivationCode= @id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("isEmailVerified", isEmailVerified);
            Command.Parameters.AddWithValue("id", id);
            Connection.Open();
            bool rowAffected = Convert.ToBoolean(Command.ExecuteNonQuery());
            Connection.Close();
            return rowAffected;
        }

        public MedicalAccount IsMedicalLoginVerified(string aLoginEmail)
        {
            Query = "SELECT IsEmailVerified, Status FROM MedicalAccounts WHERE MedicalEmail = @aLoginEmail";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("aLoginEmail", aLoginEmail);
            Connection.Open();
            Reader = Command.ExecuteReader();
            MedicalAccount medicalAccount = null;
            while (Reader.Read())
            {
                medicalAccount = new MedicalAccount();
                medicalAccount.IsEmailVerified = Convert.ToBoolean(Reader["IsEmailVerified"]);
                medicalAccount.Status = Reader["Status"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return medicalAccount;
        }

        public int UpdateMedicalProfile(MedicalAccount aMedicalAccount)
        {
            Query = "UPDATE MedicalAccounts SET MedicalType = @medicalType, MedicalName = @medicalName, ContactPersonName = @contactPersonName, ContactPersonPosition = @contactPersonPosition, ContactPersonPhoneNo = @contactPersonPhoneNo, MedicalContact1 = @medicalContactNo1, MedicalContact2 = @medicalContactNo2, Address = @address, Area = @area, City = @city, AmbulanceService = @ambulanceService, AmbulanceContact= @ambulanceContact WHERE MedicalEmail = @medicalEmail";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("medicalType", aMedicalAccount.MedicalType);
            Command.Parameters.AddWithValue("medicalName", aMedicalAccount.MedicalName);
            Command.Parameters.AddWithValue("contactPersonName", aMedicalAccount.ContactPersonName);
            Command.Parameters.AddWithValue("contactPersonPosition", aMedicalAccount.ContactPersonPosition);
            Command.Parameters.AddWithValue("contactPersonPhoneNo", aMedicalAccount.ContactPersonPhoneNo);
            Command.Parameters.AddWithValue("medicalContactNo1", aMedicalAccount.MedicalContactNo1);
            Command.Parameters.AddWithValue("medicalContactNo2", aMedicalAccount.MedicalContactNo2);
            Command.Parameters.AddWithValue("address", aMedicalAccount.Address);
            Command.Parameters.AddWithValue("area", aMedicalAccount.Area);
            Command.Parameters.AddWithValue("city", aMedicalAccount.City);
            Command.Parameters.AddWithValue("ambulanceService", aMedicalAccount.AmbulanceService);
            Command.Parameters.AddWithValue("ambulanceContact", aMedicalAccount.AmbulanceContact);
            Command.Parameters.AddWithValue("medicalEmail", aMedicalAccount.MedicalEmail);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public bool SaveHospitalService(string serviceName, int medicalId)
        {
            Query = "INSERT INTO HospitalServices VALUES(@serviceName, @medicalId)";
            Command = new SqlCommand(Query,Connection);
            Command.Parameters.AddWithValue("serviceName", serviceName);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            bool rowAffected = Convert.ToBoolean(Command.ExecuteNonQuery());
            Connection.Close();
            return rowAffected;
        }

        public bool SaveConsultant(string consultantConsultantName, int medicalId)
        {
            Query = "INSERT INTO MedicalConsultants VALUES(@consultantConsultantName, @medicalId)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("consultantConsultantName", consultantConsultantName);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            bool rowAffected = Convert.ToBoolean(Command.ExecuteNonQuery());
            Connection.Close();
            return rowAffected;
        }

        public bool SaveMedicalFacility(string medicalFacilityFacilityName, int medicalId)
        {
            Query = "INSERT INTO MedicalFacilities VALUES(@medicalFacilityFacilityName, @medicalId)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("medicalFacilityFacilityName", medicalFacilityFacilityName);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            bool rowAffected = Convert.ToBoolean(Command.ExecuteNonQuery());
            Connection.Close();
            return rowAffected;
        }

        public bool SaveOtherService(string otherServiceServiceName, int medicalId)
        {
            Query = "INSERT INTO OtherMedicalServices VALUES(@otherServiceServiceName, @medicalId)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("otherServiceServiceName", otherServiceServiceName);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            bool rowAffected = Convert.ToBoolean(Command.ExecuteNonQuery());
            Connection.Close();
            return rowAffected;
        }

        public bool SaveDiagnosticService(string diagnosticServiceServiceName, int medicalId)
        {
            Query = "INSERT INTO DiagnosticServices VALUES(@diagnosticServiceServiceName, @medicalId)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("diagnosticServiceServiceName", diagnosticServiceServiceName);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            bool rowAffected = Convert.ToBoolean(Command.ExecuteNonQuery());
            Connection.Close();
            return rowAffected;
        }

        public List<HospitalServices> GetHospitalServices(int hospitalServiceMedicalId)
        {
            Query = "SELECT Id, ServiceName FROM HospitalServices WHERE MedicalId = @medicalId ORDER BY ServiceName";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("medicalId", hospitalServiceMedicalId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<HospitalServices> hospitalServices = new List<HospitalServices>();
            while (Reader.Read())
            {
                HospitalServices hospitalService = new HospitalServices();
                hospitalService.Id = Convert.ToInt32(Reader["Id"]);
                hospitalService.ServiceName = Reader["ServiceName"].ToString();
                hospitalServices.Add(hospitalService);
            }
            Reader.Close();
            Connection.Close();
            return hospitalServices;
        }
        public List<DiagnosticServices> GetDiagnosticServices(int medicalId)
        {
            Query = "SELECT Id, ServiceName FROM DiagnosticServices WHERE MedicalId = @medicalId ORDER BY ServiceName";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<DiagnosticServices> diagnosticServices = new List<DiagnosticServices>();
            while (Reader.Read())
            {
                DiagnosticServices diagnosticService = new DiagnosticServices();
                diagnosticService.Id = Convert.ToInt32(Reader["Id"]);
                diagnosticService.ServiceName = Reader["ServiceName"].ToString();
                diagnosticServices.Add(diagnosticService);
            }
            Reader.Close();
            Connection.Close();
            return diagnosticServices;
        }
        public List<OtherServices> GetOtherServices(int medicalId)
        {
            Query = "SELECT Id, ServiceName FROM OtherMedicalServices WHERE MedicalId = @medicalId ORDER BY ServiceName";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<OtherServices> otherServices = new List<OtherServices>();
            while (Reader.Read())
            {
                OtherServices otherService = new OtherServices();
                otherService.Id = Convert.ToInt32(Reader["Id"]);
                otherService.ServiceName = Reader["ServiceName"].ToString();
                otherServices.Add(otherService);
            }
            Reader.Close();
            Connection.Close();
            return otherServices;
        }
        public List<MedicalFacilities> GetMedicalFacilities(int medicalId)
        {
            Query = "SELECT Id, FacilityName FROM MedicalFacilities WHERE MedicalId = @medicalId ORDER BY FacilityName";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<MedicalFacilities> facilities = new List<MedicalFacilities>();
            while (Reader.Read())
            {
                MedicalFacilities facility = new MedicalFacilities();
                facility.Id = Convert.ToInt32(Reader["Id"]);
                facility.FacilityName = Reader["FacilityName"].ToString();
                facilities.Add(facility);
            }
            Reader.Close();
            Connection.Close();
            return facilities;
        }
        public List<Consultants> GetMedicalConsultants(int medicalId)
        {
            Query = "SELECT Id, ConsultantName FROM MedicalConsultants WHERE MedicalId = @medicalId ORDER BY ConsultantName";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Consultants> consultants = new List<Consultants>();
            while (Reader.Read())
            {
                Consultants consultant = new Consultants();
                consultant.Id = Convert.ToInt32(Reader["Id"]);
                consultant.ConsultantName = Reader["ConsultantName"].ToString();
                consultants.Add(consultant);
            }
            Reader.Close();
            Connection.Close();
            return consultants;
        }

        public bool SaveEmergencyDetails(MedicalService aMedicalService)
        {
            Query = "INSERT INTO MedicalEmergency VALUES(@twentyFourService, @emergencyService, @ambulanceService, @seatCapacity, @emergencyPhoneNo, @emergencyPhoneNo2, @ambulancePhoneNo, @ambulancePhoneNo2, @medicalId)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("twentyFourService", aMedicalService.TwentyFourService);
            Command.Parameters.AddWithValue("emergencyService", aMedicalService.EmergencyService);
            Command.Parameters.AddWithValue("ambulanceService", aMedicalService.AmbulanceService);
            Command.Parameters.AddWithValue("seatCapacity", aMedicalService.SeatCapacity);
            Command.Parameters.AddWithValue("emergencyPhoneNo", aMedicalService.EmergencyPhoneNo);
            Command.Parameters.AddWithValue("emergencyPhoneNo2", aMedicalService.EmergencyPhoneNo2);
            Command.Parameters.AddWithValue("ambulancePhoneNo", aMedicalService.AmbulancePhoneNo);
            Command.Parameters.AddWithValue("ambulancePhoneNo2", aMedicalService.AmbulancePhoneNo2);
            Command.Parameters.AddWithValue("medicalId", aMedicalService.MedicalId);
            Connection.Open();
            bool rowAffected = Convert.ToBoolean(Command.ExecuteNonQuery());
            Connection.Close();
            return rowAffected;
        }

        public MedicalService GetEmergencyDetails(int medicalId)
        {
            Query = "SELECT * FROM MedicalEmergency WHERE MedicalId = @medicalId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            MedicalService aMedicalService = null;
            while (Reader.Read())
            {
                aMedicalService = new MedicalService();
                aMedicalService.Id = Convert.ToInt32(Reader["Id"]);
                aMedicalService.TwentyFourService = Reader["TwentyFourService"].ToString();
                aMedicalService.EmergencyService = Reader["EmergencyService"].ToString();
                aMedicalService.AmbulanceService = Reader["AmbulanceService"].ToString();
                aMedicalService.SeatCapacity = Convert.ToInt32(Reader["SeatCapacity"]);
                aMedicalService.EmergencyPhoneNo = Reader["EmergencyPhoneNo"].ToString();
                aMedicalService.EmergencyPhoneNo2 = Reader["EmergencyPhoneNo2"].ToString();
                aMedicalService.AmbulancePhoneNo = Reader["AmbulancePhoneNo"].ToString();
                aMedicalService.AmbulancePhoneNo2 = Reader["AmbulancePhoneNo2"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return aMedicalService;
        }

        public bool DeleteHospitalService(int id)
        {
            Query = "DELETE FROM HospitalServices WHERE Id =@id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("id", id);
            Connection.Open();
            bool rowAffected = Convert.ToBoolean(Command.ExecuteNonQuery());
            Connection.Close();
            return rowAffected;
        }
        public bool DeleteDiagnosticService(int id)
        {
            Query = "DELETE FROM DiagnosticServices WHERE Id =@id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("id", id);
            Connection.Open();
            bool rowAffected = Convert.ToBoolean(Command.ExecuteNonQuery());
            Connection.Close();
            return rowAffected;
        }
        public bool DeleteOtherService(int id)
        {
            Query = "DELETE FROM OtherMedicalServices WHERE Id =@id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("id", id);
            Connection.Open();
            bool rowAffected = Convert.ToBoolean(Command.ExecuteNonQuery());
            Connection.Close();
            return rowAffected;
        }
        public bool DeleteFacility(int id)
        {
            Query = "DELETE FROM MedicalFacilities WHERE Id =@id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("id", id);
            Connection.Open();
            bool rowAffected = Convert.ToBoolean(Command.ExecuteNonQuery());
            Connection.Close();
            return rowAffected;
        }
        public bool DeleteConsultant(int id)
        {
            Query = "DELETE FROM MedicalConsultants WHERE Id =@id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("id", id);
            Connection.Open();
            bool rowAffected = Convert.ToBoolean(Command.ExecuteNonQuery());
            Connection.Close();
            return rowAffected;
        }

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

        public bool SaveDoctor(Doctors aDoctors)
        {
            bool rowAffected = false;
            Query =
                "INSERT INTO Doctors VALUES(@role, @name, @gender, @title, @speciality, @email, @mobileNo, @password, @phoneNo, @bmdcReg, @otherSpecification, @imagePath, @passwordVerified, @status, @accountCreatedDate)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("role", aDoctors.Role);
            Command.Parameters.AddWithValue("name", aDoctors.Name);
            Command.Parameters.AddWithValue("gender", aDoctors.Gender);
            Command.Parameters.AddWithValue("title", aDoctors.Title);
            Command.Parameters.AddWithValue("speciality", aDoctors.Speciality);
            Command.Parameters.AddWithValue("email", aDoctors.Email);
            Command.Parameters.AddWithValue("mobileNo", aDoctors.MobileNo);
            Command.Parameters.AddWithValue("password", aDoctors.Password);
            Command.Parameters.AddWithValue("phoneNo", aDoctors.PhoneNo);
            Command.Parameters.AddWithValue("bmdcReg", aDoctors.BmdcReg);
            Command.Parameters.AddWithValue("otherSpecification", aDoctors.OtherSpecification);
            Command.Parameters.AddWithValue("imagePath", aDoctors.ImagePath);
            Command.Parameters.AddWithValue("passwordVerified", aDoctors.PasswordVerified);
            Command.Parameters.AddWithValue("status", aDoctors.Status);
            Command.Parameters.AddWithValue("accountCreatedDate", aDoctors.AccountCreatedDate);
            Connection.Open();
            rowAffected = Convert.ToBoolean(Command.ExecuteNonQuery());
            Connection.Close();
            if (rowAffected)
            {
                int id = GetDoctorId(aDoctors.MobileNo, aDoctors.BmdcReg);
                aDoctors.AssignedDoctors.DoctorId = id;
                bool scheduleRowAffected = SaveAssignedDoctor(aDoctors.AssignedDoctors);
            }
            return rowAffected;
        }

        private int GetDoctorId(string mobileNo, string bmdcReg)
        {
            Query = "SELECT Id FROM Doctors WHERE MobileNo = @mobileNo AND BmdcReg = @bmdcReg";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("mobileNo", mobileNo);
            Command.Parameters.AddWithValue("bmdcReg", bmdcReg);
            Connection.Open();
            Reader = Command.ExecuteReader();
            int id = 0;
            while (Reader.Read())
            {
                id = Convert.ToInt32(Reader["Id"]);
            }
            Reader.Close();
            Connection.Close();
            return id;
        }

        public bool SaveAssignedDoctor(AssignedDoctors assignedDoctors)
        {
            Query = "INSERT INTO AssignedDoctors VALUES(@medicalId, @doctorId, @doctorFee, @returningFee, @sat, @sun, @mon, @tue, @wed, @thu, @fri, @satTime, @sunTime, @monTime, @tueTime,@wedTime,@thuTime,@friTime)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("medicalId", assignedDoctors.MedicalId);
            Command.Parameters.AddWithValue("doctorId", assignedDoctors.DoctorId);
            Command.Parameters.AddWithValue("doctorFee", assignedDoctors.DoctorFee);
            Command.Parameters.AddWithValue("returningFee", assignedDoctors.DoctorReturningFee);
            Command.Parameters.AddWithValue("sat", assignedDoctors.Sat);
            Command.Parameters.AddWithValue("sun", assignedDoctors.Sun);
            Command.Parameters.AddWithValue("mon", assignedDoctors.Mon);
            Command.Parameters.AddWithValue("tue", assignedDoctors.Tue);
            Command.Parameters.AddWithValue("wed", assignedDoctors.Wed);
            Command.Parameters.AddWithValue("thu", assignedDoctors.Thu);
            Command.Parameters.AddWithValue("fri", assignedDoctors.Fri);
            Command.Parameters.AddWithValue("satTime", assignedDoctors.SatFromTime);
            Command.Parameters.AddWithValue("sunTime", assignedDoctors.SunFromTime);
            Command.Parameters.AddWithValue("monTime", assignedDoctors.MonFromTime);
            Command.Parameters.AddWithValue("tueTime", assignedDoctors.TueFromTime);
            Command.Parameters.AddWithValue("wedTime", assignedDoctors.WedFromTime);
            Command.Parameters.AddWithValue("thuTime", assignedDoctors.ThuFromTime);
            Command.Parameters.AddWithValue("friTime", assignedDoctors.FriFromTime);
            Connection.Open();
            bool rowAffected = Convert.ToBoolean(Command.ExecuteNonQuery());
            Connection.Close();
            return rowAffected;
        }

        public bool IsDoctorExistInCurrentMedical(string mobileNo, string bmdcReg, int medicalId)
        {
            Query = "SELECT * FROM ViewDoctors WHERE (MobileNo = @mobileNo OR BmdcReg = @bmdcReg) AND MedicalId IN (@medicalId)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("mobileNo", mobileNo);
            Command.Parameters.AddWithValue("bmdcReg", bmdcReg);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;
        }
        public bool IsDoctorExistInOtherMedical(string mobileNo, string bmdcReg, int medicalId)
        {
            Query = "SELECT * FROM ViewDoctors WHERE (MobileNo = @mobileNo OR  BmdcReg = @bmdcReg) AND NOT MedicalId = @medicalId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("mobileNo", mobileNo);
            Command.Parameters.AddWithValue("bmdcReg", bmdcReg);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;
        }

        public List<ViewDoctors> GetAllDoctors(int medicalId)
        {
            Query = "SELECT * FROM ViewDoctors WHERE MedicalId = @medicalId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<ViewDoctors> viewDoctors = new List<ViewDoctors>();
            while (Reader.Read())
            {
                ViewDoctors doctors = new ViewDoctors();
                doctors.Id = Convert.ToInt32(Reader["Id"]);
                doctors.ImagePath = Reader["ImagePath"].ToString();
                doctors.Name = Reader["Name"].ToString();
                doctors.Gender = Reader["Gender"].ToString();
                doctors.Title = Reader["Title"].ToString();
                doctors.Speciality = Reader["Speciality"].ToString();
                doctors.Email = Reader["Email"].ToString();
                doctors.MobileNo = Reader["MobileNo"].ToString();
                doctors.PhoneNo = Reader["PhoneNo"].ToString();
                doctors.BmdcReg = Reader["BmdcReg"].ToString();
                doctors.OtherSpecification = Reader["OtherSpecification"].ToString();
                doctors.Fee = Convert.ToInt32(Reader["DoctorFee"]);
                doctors.ReturningFee = Convert.ToInt32(Reader["DoctorReturningFee"]);
                doctors.Sat = Convert.ToBoolean(Reader["Sat"]);
                doctors.Sun = Convert.ToBoolean(Reader["Sun"]);
                doctors.Mon = Convert.ToBoolean(Reader["Mon"]);
                doctors.Tue = Convert.ToBoolean(Reader["Tue"]);
                doctors.Wed = Convert.ToBoolean(Reader["Wed"]);
                doctors.Thu = Convert.ToBoolean(Reader["Thu"]);
                doctors.Fri = Convert.ToBoolean(Reader["Fri"]);
                doctors.SatTime = Reader["SatTime"].ToString();
                doctors.SunTime = Reader["SunTime"].ToString();
                doctors.MonTime = Reader["MonTime"].ToString();
                doctors.TueTime = Reader["TueTime"].ToString();
                doctors.WedTime = Reader["WedTime"].ToString();
                doctors.ThuTime = Reader["ThuTime"].ToString();
                doctors.FriTime = Reader["FriTime"].ToString();
                viewDoctors.Add(doctors);
            }
            Reader.Close();
            Connection.Close();
            return viewDoctors;
        }

        public ViewDoctors GetDoctor(int id, int medicalId)
        {
            Query = "SELECT * FROM ViewDoctors WHERE Id = @id AND MedicalId = @medicalId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("id", id);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            ViewDoctors doctors = null;
            while (Reader.Read())
            {
                doctors = new ViewDoctors();
                doctors.Id = Convert.ToInt32(Reader["Id"]);
                doctors.ImagePath = Reader["ImagePath"].ToString();
                doctors.Name = Reader["Name"].ToString();
                doctors.Gender = Reader["Gender"].ToString();
                doctors.Title = Reader["Title"].ToString();
                doctors.Speciality = Reader["Speciality"].ToString();
                doctors.Email = Reader["Email"].ToString();
                doctors.MobileNo = Reader["MobileNo"].ToString();
                doctors.PhoneNo = Reader["PhoneNo"].ToString();
                doctors.BmdcReg = Reader["BmdcReg"].ToString();
                doctors.OtherSpecification = Reader["OtherSpecification"].ToString();
                doctors.MedicalId = Convert.ToInt32(Reader["MedicalId"]);
                doctors.Fee = Convert.ToInt32(Reader["DoctorFee"]);
                doctors.ReturningFee = Convert.ToInt32(Reader["DoctorReturningFee"]);
                doctors.Sat = Convert.ToBoolean(Reader["Sat"]);
                doctors.Sun = Convert.ToBoolean(Reader["Sun"]);
                doctors.Mon = Convert.ToBoolean(Reader["Mon"]);
                doctors.Tue = Convert.ToBoolean(Reader["Tue"]);
                doctors.Wed = Convert.ToBoolean(Reader["Wed"]);
                doctors.Thu = Convert.ToBoolean(Reader["Thu"]);
                doctors.Fri = Convert.ToBoolean(Reader["Fri"]);
                doctors.SatTime = Reader["SatTime"].ToString();
                doctors.SunTime = Reader["SunTime"].ToString();
                doctors.MonTime = Reader["MonTime"].ToString();
                doctors.TueTime = Reader["TueTime"].ToString();
                doctors.WedTime = Reader["WedTime"].ToString();
                doctors.ThuTime = Reader["ThuTime"].ToString();
                doctors.FriTime = Reader["FriTime"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return doctors;
        }

        public bool EditDoctor(Doctors doctors)
        {
            Query = "UPDATE ViewDoctors SET ImagePath = @imagePath, Name = @name, Gender = @gender, Title =@title, Speciality = @speciality, Email = @email, PhoneNo = @phoneNo, OtherSpecification = @other WHERE Id = @id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("imagePath", doctors.ImagePath);
            Command.Parameters.AddWithValue("name", doctors.Name);
            Command.Parameters.AddWithValue("gender", doctors.Gender);
            Command.Parameters.AddWithValue("title", doctors.Title);
            Command.Parameters.AddWithValue("speciality", doctors.Speciality);
            Command.Parameters.AddWithValue("email", doctors.Email);
            Command.Parameters.AddWithValue("phoneNo", doctors.PhoneNo);
            Command.Parameters.AddWithValue("other", doctors.OtherSpecification);
            Command.Parameters.AddWithValue("id", doctors.Id);
            Connection.Open();
            bool rowAffected = Convert.ToBoolean(Command.ExecuteNonQuery());
            Connection.Close();
            if (doctors.AssignedDoctors.DoctorFee >= 0 || doctors.AssignedDoctors.DoctorReturningFee >= 0)
            {
                Query = "UPDATE ViewDoctors SET DoctorFee = @fee, DoctorReturningFee = @returningFee WHERE Id = @id AND MedicalId = @medicalId";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("fee", doctors.AssignedDoctors.DoctorFee);
                Command.Parameters.AddWithValue("returningFee", doctors.AssignedDoctors.DoctorReturningFee);
                Command.Parameters.AddWithValue("id", doctors.Id);
                Command.Parameters.AddWithValue("medicalId", doctors.MedicalId);
                Connection.Open();
                rowAffected = Convert.ToBoolean(Command.ExecuteNonQuery());
                Connection.Close();
            }
            return rowAffected;
        }

        public string DeleteDoctor(int id, int medicalId)
        {
            bool rowAffected = DeleteManageAppoint(id, medicalId);
            string message = null;
            if (rowAffected)
            {
                Query = "DELETE FROM AssignedDoctors WHERE MedicalId = @medicalId AND DoctorId =@id";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("medicalId", medicalId);
                Command.Parameters.AddWithValue("id", id);
                Connection.Open();
                bool rowAffected2 = Convert.ToBoolean(Command.ExecuteNonQuery());
                Connection.Close();
                if (rowAffected2)
                {
                    message = "Doctor deleted successfully.";
                }
                else
                {
                    message = "Doctor deleting failed.";
                }
            }
            else
            {
                message = "Doctor deleting failed.";
            }
            return message;
        }

        private bool DeleteManageAppoint(int id, int medicalId)
        {
            bool rowAffected2 = false;
            Query = "DELETE FROM ManageAppointment WHERE DoctorId =@id AND MedicalId = @medicalId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("id", id);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            if (rowAffected == 0)
            {
                rowAffected2 = true;
            }
            return rowAffected2;
        }

        public bool EditEmergencyDetails(MedicalService aMedicalService)
        {
            Query = "UPDATE MedicalEmergency SET TwentyFourService = @24service, EmergencyService = @emergency, AmbulanceService = @ambulance, SeatCapacity = @seat, EmergencyPhoneNo = @emerPhoneNo, EmergencyPhoneNo2 = @emerPhoneNo2, AmbulancePhoneNo = @ambuPhoneNo, AmbulancePhoneNo2 = @ambuPhoneNo2 WHERE MedicalId =@medicalId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("24service", aMedicalService.TwentyFourService);
            Command.Parameters.AddWithValue("emergency", aMedicalService.EmergencyService);
            Command.Parameters.AddWithValue("ambulance", aMedicalService.AmbulanceService);
            Command.Parameters.AddWithValue("seat", aMedicalService.SeatCapacity);
            Command.Parameters.AddWithValue("emerPhoneNo", aMedicalService.EmergencyPhoneNo);
            Command.Parameters.AddWithValue("emerPhoneNo2", aMedicalService.EmergencyPhoneNo2);
            Command.Parameters.AddWithValue("ambuPhoneNo", aMedicalService.AmbulancePhoneNo);
            Command.Parameters.AddWithValue("ambuPhoneNo2", aMedicalService.AmbulancePhoneNo2);
            Command.Parameters.AddWithValue("medicalId", aMedicalService.MedicalId);
            Connection.Open();
            bool rowAffected = Convert.ToBoolean(Command.ExecuteNonQuery());
            Connection.Close();
            return rowAffected;
        }


        public int GetExistDoctor(string mobileNo, string bmdcReg)
        {
            Query = "SELECT Id FROM Doctors WHERE MobileNo = @mobileNo OR BmdcReg = @bmdcReg";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("mobileNo", mobileNo);
            Command.Parameters.AddWithValue("bmdcReg", bmdcReg);
            Connection.Open();
            Reader = Command.ExecuteReader();
            int doctorId = 0;
            if (Reader.Read())
            {
                doctorId = Convert.ToInt32(Reader["Id"]);
            }
            Reader.Close();
            Connection.Close();
            return doctorId;
        }

        public bool EditDoctorSchedule(AssignedDoctors assignedDoctors)
        {
            Query = "UPDATE AssignedDoctors SET Sat = @sat, Sun = @sun, Mon = @mon, Tue = @tue, Wed = @wed, Thu = @thu, Fri = @fri, SatTime = @satTime, SunTime = @sunTime, MonTime = @monTime, TueTime = @tueTime, WedTime = @wedTime, ThuTime = @thuTime, FriTime = @friTime WHERE MedicalId = @medicalId AND DoctorId = @doctorId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("sat", assignedDoctors.Sat);
            Command.Parameters.AddWithValue("sun", assignedDoctors.Sun);
            Command.Parameters.AddWithValue("mon", assignedDoctors.Mon);
            Command.Parameters.AddWithValue("tue", assignedDoctors.Tue);
            Command.Parameters.AddWithValue("wed", assignedDoctors.Wed);
            Command.Parameters.AddWithValue("thu", assignedDoctors.Thu);
            Command.Parameters.AddWithValue("fri", assignedDoctors.Fri);
            Command.Parameters.AddWithValue("satTime", assignedDoctors.SatFromTime);
            Command.Parameters.AddWithValue("sunTime", assignedDoctors.SunFromTime);
            Command.Parameters.AddWithValue("monTime", assignedDoctors.MonFromTime);
            Command.Parameters.AddWithValue("tueTime", assignedDoctors.TueFromTime);
            Command.Parameters.AddWithValue("wedTime", assignedDoctors.WedFromTime);
            Command.Parameters.AddWithValue("thuTime", assignedDoctors.ThuFromTime);
            Command.Parameters.AddWithValue("friTime", assignedDoctors.FriFromTime);
            Command.Parameters.AddWithValue("medicalId", assignedDoctors.MedicalId);
            Command.Parameters.AddWithValue("doctorId", assignedDoctors.DoctorId);
            Connection.Open();
            bool rowAffected = Convert.ToBoolean(Command.ExecuteNonQuery());
            Connection.Close();
            return rowAffected;
        }

        public bool IsMedicalOldPasswordValid(int medicalId, string oldPassword)
        {
            Query = "SELECT * FROM MedicalAccounts WHERE MedicalId = @medicalId AND Password = @oldPassword";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Command.Parameters.AddWithValue("oldPassword", oldPassword);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;
        }

        public int ChangeMedicalPassword(ChangeMedicalPassword changeMedicalPassword)
        {
            Query = "UPDATE MedicalAccounts SET Password = @newPassword WHERE MedicalId = @medicalId AND Password = @oldPassword";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("newPassword", changeMedicalPassword.NewPassword);
            Command.Parameters.AddWithValue("medicalId", changeMedicalPassword.MedicalId);
            Command.Parameters.AddWithValue("oldPassword", changeMedicalPassword.OldPassword);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
        
    }
}