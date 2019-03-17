using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HealthSupportApp.Models;
using HealthSupportApp.Models.DoctorModel;
using HealthSupportApp.Models.HomeModel;
using HealthSupportApp.Models.MedicalModel;
using HealthSupportApp.Models.UserModel;

namespace HealthSupportApp.Gateway.UserGateway
{
    public class UserGateway : ParentGateway
    {
        public int Save(UserAccount aUserAccount)
        {
            int rowAffected = 0;
            try
            {
                Query = "INSERT INTO UserAccounts VALUES(@role, @name, @email, @password, @mobileNo, @gender, @age, @bloodGroup,@wantToGiveBlood, @location)";

                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("role", aUserAccount.Role);
                Command.Parameters.AddWithValue("name", aUserAccount.Name);
                Command.Parameters.AddWithValue("email", aUserAccount.Email);
                Command.Parameters.AddWithValue("password", aUserAccount.Password);
                Command.Parameters.AddWithValue("confirmPassword", aUserAccount.ConfirmPassword);
                Command.Parameters.AddWithValue("mobileNo", aUserAccount.MobileNo);
                Command.Parameters.AddWithValue("gender", aUserAccount.Gender);
                Command.Parameters.AddWithValue("age", aUserAccount.Age);
                Command.Parameters.AddWithValue("bloodGroup", aUserAccount.BloodGroup);
                Command.Parameters.AddWithValue("wantToGiveBlood", aUserAccount.WantToGiveBlood);
                Command.Parameters.AddWithValue("location", aUserAccount.Location);

                Connection.Open();
                rowAffected = Command.ExecuteNonQuery();
                Connection.Close();
                return rowAffected;
            }
            catch (Exception ex)
            {
                string st = ex.Message;
                if (st != null)
                {
                    rowAffected = -1;
                }
                return rowAffected;
            }
        }

        public bool IsEmailExists(string email)
        {
            bool hasRow = false;
            if (email == "")
            {
                return hasRow;
            }
            Query = "SELECT * FROM UserAccounts WHERE Email= @email";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("email", email);
            Command.Connection.Open();
            Reader = Command.ExecuteReader();
            hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;
        }

        public bool IsMobileNoExists(string mobileNo)
        {
            Query = "SELECT * FROM UserAccounts WHERE MobileNo= @mobileNo";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("mobileNo", mobileNo);
            Command.Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;
        }

        public bool IsValid(string mobileNo, string password)
        {
            Query = "SELECT * FROM UserAccounts WHERE MobileNo= @mobileNo AND Password=@password";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("mobileNo", mobileNo);
            Command.Parameters.AddWithValue("password", password);
            Command.Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Command.Connection.Close();
            return hasRow;
        }

        public UserAccount GetUserData(string mobileNo)
        {
            Query = "SELECT * FROM UserAccounts WHERE MobileNo = @mobileNo";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("mobileNo", mobileNo);
            Command.Connection.Open();
            Reader = Command.ExecuteReader();

            UserAccount aUserAccount = null;
            if (Reader.HasRows)
            {
                Reader.Read();
                aUserAccount = new UserAccount();
                aUserAccount.UserId = Convert.ToInt32(Reader["UserId"]);
                aUserAccount.Name = Reader["UserName"].ToString();
                aUserAccount.Email = Reader["Email"].ToString();
                aUserAccount.MobileNo = Reader["MobileNo"].ToString();
                aUserAccount.Gender = Reader["Gender"].ToString();
                aUserAccount.Age = Reader["Age"].ToString();
                aUserAccount.BloodGroup = Reader["BloodGroup"].ToString();
                aUserAccount.WantToGiveBlood = Convert.ToBoolean(Reader["WantToGiveBlood"]);
                aUserAccount.Location = Reader["Location"].ToString();
            }
            Reader.Close();
            Command.Connection.Close();
            return aUserAccount;
        }

        public int EditUserProfile(UserAccount aUserAccount)
        {
            int rowAffected = 0;
            try
            {
                Query = "UPDATE UserAccounts SET UserName = @name, Email = @email, Gender = @gender, Age = @age, BloodGroup = @bloodgroup, WantToGiveBlood = @wantToGiveBlood, Location =@location WHERE MobileNo = @mobileNo";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("name", aUserAccount.Name);
                Command.Parameters.AddWithValue("email", aUserAccount.Email);
                Command.Parameters.AddWithValue("gender", aUserAccount.Gender);
                Command.Parameters.AddWithValue("age", aUserAccount.Age);
                Command.Parameters.AddWithValue("bloodgroup", aUserAccount.BloodGroup);
                Command.Parameters.AddWithValue("wantToGiveBlood", aUserAccount.WantToGiveBlood);
                Command.Parameters.AddWithValue("location", aUserAccount.Location);
                Command.Parameters.AddWithValue("mobileNo", aUserAccount.MobileNo);
                Connection.Open();
                rowAffected = Command.ExecuteNonQuery();
                Connection.Close();
                return rowAffected;
            }
            catch(Exception ex)
            {
                Connection.Close();
                rowAffected = 0;
                return rowAffected;
            }
        }

        public int SaveComment(Comments comment)
        {
            Query = "INSERT INTO ForumComments VALUES(@userId, @doctorId, @medicalId, @forumPostId, @comment)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("userId", comment.UserId);
            Command.Parameters.AddWithValue("doctorId", comment.DoctorId);
            Command.Parameters.AddWithValue("medicalId", comment.MedicalId);
            Command.Parameters.AddWithValue("forumPostId", comment.PostId);
            Command.Parameters.AddWithValue("comment", comment.Comment);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public int SavePost(ForumPost forumPost, int countTopicCategory)
        {
            Query = "INSERT INTO ForumPosts VALUES(@topicCategoryId, @topicName, @description, @imagePath, @views, @userId, @doctorId, @medicalId, @postDate)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("topicCategoryId", forumPost.TopicCategoryId);
            Command.Parameters.AddWithValue("topicName", forumPost.TopicName);
            Command.Parameters.AddWithValue("description", forumPost.Description);
            Command.Parameters.AddWithValue("imagePath", forumPost.ImagePath);
            Command.Parameters.AddWithValue("views", forumPost.Views);
            Command.Parameters.AddWithValue("userId", forumPost.UserId);
            Command.Parameters.AddWithValue("doctorId", forumPost.DoctorId);
            Command.Parameters.AddWithValue("medicalId", forumPost.MedicalId);
            Command.Parameters.AddWithValue("postDate", forumPost.PostDate);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            if (rowAffected > 0)
            {
                int rowUpdated = UpdateCountCategory(forumPost.TopicCategoryId, countTopicCategory);
                if (rowUpdated == 0)
                {
                    rowAffected = 0;
                }
            }
            return rowAffected;
        }

        private int UpdateCountCategory(int topicCategoryId, int countTopicCategory)
        {
            Query = "UPDATE TopicCategories SET CountCategory = @countTopicCategory WHERE TopicCategoryId = @topicCategoryId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("countTopicCategory", countTopicCategory);
            Command.Parameters.AddWithValue("topicCategoryId", topicCategoryId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public List<MedicalAccount> GetMedicalSearchResult(SearchModel aSearchModel, string keyword)
        {
            List<MedicalAccount> medicalAccounts = new List<MedicalAccount>();
            if (keyword == "Medical")
            {
                Query = "SELECT * FROM MedicalAccounts WHERE Address LIKE '%'+@address+'%' OR Area LIKE '%'+@area+'%' OR City LIKE '%'+@city+'%' AND Role = 'Medical'";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("address", aSearchModel.Location);
                Command.Parameters.AddWithValue("area", aSearchModel.Location);
                Command.Parameters.AddWithValue("city", aSearchModel.Location);
                Command.Parameters.AddWithValue("searchKeyword", keyword);
            }
            else
            {
                Query = "SELECT * FROM MedicalAccounts WHERE (Address LIKE '%'+@address+'%' OR Area LIKE '%'+@area+'%' OR City LIKE '%'+@city+'%') AND (MedicalName LIKE '%' + @searchKeyword2 + '%' OR MedicalType LIKE '%'+@searchKeyword3+'%')";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("address", aSearchModel.Location);
                Command.Parameters.AddWithValue("area", aSearchModel.Location);
                Command.Parameters.AddWithValue("city", aSearchModel.Location);
                Command.Parameters.AddWithValue("searchKeyword2", aSearchModel.SearchString);
                Command.Parameters.AddWithValue("searchKeyword3", aSearchModel.SearchString);
            }
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                MedicalAccount aMedicalAccount = new MedicalAccount();
                aMedicalAccount.MedicalId = Convert.ToInt32(Reader["MedicalId"]);
                aMedicalAccount.MedicalName = Reader["MedicalName"].ToString();
                aMedicalAccount.MedicalEmail = Reader["MedicalEmail"].ToString();
                aMedicalAccount.MedicalContactNo1 = Reader["MedicalContact1"].ToString();
                aMedicalAccount.MedicalContactNo2 = Reader["MedicalContact2"].ToString();
                aMedicalAccount.Address = Reader["Address"].ToString();
                aMedicalAccount.Area = Reader["Area"].ToString();
                aMedicalAccount.City = Reader["City"].ToString();
                aMedicalAccount.AmbulanceService = Reader["AmbulanceService"].ToString();
                aMedicalAccount.AmbulanceContact = Reader["AmbulanceContact"].ToString();
                medicalAccounts.Add(aMedicalAccount);
            }
            Reader.Close();
            Connection.Close();
            return medicalAccounts;

        }

        public List<int> GetDoctorsIdSearchResult(SearchModel aSearchModel)
        {
            Query = "SELECT DISTINCT Id FROM ViewDoctors WHERE (Address LIKE '%'+@address+'%' OR Area LIKE '%'+@area+'%' OR City LIKE '%'+@city+'%') AND (Name LIKE '%'+@name+'%' OR Speciality LIKE '%'+@speciality+'%' OR OtherSpecification LIKE '%'+@other+'%')";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("address", aSearchModel.Location);
            Command.Parameters.AddWithValue("area", aSearchModel.Location);
            Command.Parameters.AddWithValue("city", aSearchModel.Location);
            Command.Parameters.AddWithValue("name", aSearchModel.SearchString);
            Command.Parameters.AddWithValue("speciality", aSearchModel.SearchString);
            Command.Parameters.AddWithValue("other", aSearchModel.SearchString);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<int> doctorsId = new List<int>();
            while (Reader.Read())
            {
                int id = Convert.ToInt32(Reader["Id"]);
                doctorsId.Add(id);
            }
            if (doctorsId.Count > 0)
            {
                Connection.Close();
                Query = "SELECT DISTINCT DoctorId FROM ViewDoctorChamber WHERE (Address LIKE '%'+@address+'%' OR Area LIKE '%'+@area+'%' OR City LIKE '%'+@city+'%') AND (Name LIKE '%'+@name+'%' OR Speciality LIKE '%'+@speciality+'%' OR OtherSpecification LIKE '%'+@other+'%')";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("address", aSearchModel.Location);
                Command.Parameters.AddWithValue("area", aSearchModel.Location);
                Command.Parameters.AddWithValue("city", aSearchModel.Location);
                Command.Parameters.AddWithValue("name", aSearchModel.SearchString);
                Command.Parameters.AddWithValue("speciality", aSearchModel.SearchString);
                Command.Parameters.AddWithValue("other", aSearchModel.SearchString);
                Connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    int id = Convert.ToInt32(Reader["DoctorId"]);
                    if (doctorsId.Contains(id) == false)
                    {
                        doctorsId.Add(id);
                    }
                }
            }
            else if (doctorsId.Count == 0)
            {
                Connection.Close();
                Query = "SELECT DISTINCT DoctorId FROM ViewDoctorChamber WHERE (Address LIKE '%'+@address+'%' OR Area LIKE '%'+@area+'%' OR City LIKE '%'+@city+'%') AND (Name LIKE '%'+@name+'%' OR Speciality LIKE '%'+@speciality+'%' OR OtherSpecification LIKE '%'+@other+'%')";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("address", aSearchModel.Location);
                Command.Parameters.AddWithValue("area", aSearchModel.Location);
                Command.Parameters.AddWithValue("city", aSearchModel.Location);
                Command.Parameters.AddWithValue("name", aSearchModel.SearchString);
                Command.Parameters.AddWithValue("speciality", aSearchModel.SearchString);
                Command.Parameters.AddWithValue("other", aSearchModel.SearchString);
                Connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    int id = Convert.ToInt32(Reader["DoctorId"]);
                    doctorsId.Add(id);
                }
            }
            Reader.Close();
            Connection.Close();
            return doctorsId;
        }
        public List<Doctors> GetDoctorsSearchResult(List<int> doctorsId)
        {
            List<Doctors> doctors = new List<Doctors>();
            foreach (int id in doctorsId)
            {
                Query = "SELECT * FROM Doctors WHERE Id = @id";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("id", id);
                Connection.Open();
                Reader = Command.ExecuteReader();
                Doctors doctor = null;
                while (Reader.Read())
                {
                    doctor = new Doctors();
                    doctor.Id = Convert.ToInt32(Reader["Id"]);
                    doctor.ImagePath = Reader["ImagePath"].ToString();
                    doctor.Name = Reader["Name"].ToString();
                    doctor.Gender = Reader["Gender"].ToString();
                    doctor.Title = Reader["Title"].ToString();
                    doctor.Speciality = Reader["Speciality"].ToString();
                    doctor.Email = Reader["Email"].ToString();
                    doctor.MobileNo = Reader["MobileNo"].ToString();
                    doctor.PhoneNo = Reader["PhoneNo"].ToString();
                    doctor.BmdcReg = Reader["BmdcReg"].ToString();
                    doctor.OtherSpecification = Reader["OtherSpecification"].ToString();
                    doctor.ViewDoctors = GetDoctorSearchDetails(doctor.Id);
                    doctor.ViewDoctorChambers = GetDoctorChamberSearchDetails(doctor.Id);
                    doctors.Add(doctor);
                }
                Reader.Close();
                Connection.Close();
            }
            return doctors;
        }
        private List<DoctorChamber> GetDoctorChamberSearchDetails(int doctorId)
        {
            Connection.Close();
            Query = "SELECT * FROM ViewDoctorChamber WHERE DoctorId = @id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("id", doctorId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<DoctorChamber> viewDoctorChambers = new List<DoctorChamber>();
            while (Reader.Read())
            {
                DoctorChamber viewChamber = new DoctorChamber();
                viewChamber.Id = Convert.ToInt32(Reader["Id"]);
                viewChamber.ChamberName = Reader["ChamberName"].ToString();
                viewChamber.Address = Reader["Address"].ToString();
                viewChamber.Area = Reader["Area"].ToString();
                viewChamber.City = Reader["City"].ToString();
                viewChamber.ContactNo = Reader["ContactNo"].ToString();
                viewChamber.PhoneNo = Reader["PhoneNo"].ToString();
                viewChamber.Fee = Convert.ToInt32(Reader["Fee"]);
                viewChamber.ReturningFee = Convert.ToInt32(Reader["ReturningFee"]);
                viewChamber.Sat = Convert.ToBoolean(Reader["Sat"]);
                viewChamber.Sun = Convert.ToBoolean(Reader["Sun"]);
                viewChamber.Mon = Convert.ToBoolean(Reader["Mon"]);
                viewChamber.Tue = Convert.ToBoolean(Reader["Tue"]);
                viewChamber.Wed = Convert.ToBoolean(Reader["Wed"]);
                viewChamber.Thu = Convert.ToBoolean(Reader["Thu"]);
                viewChamber.Fri = Convert.ToBoolean(Reader["Fri"]);
                viewChamber.SatFromTime = Reader["SatTime"].ToString();
                viewChamber.SunFromTime = Reader["SunTime"].ToString();
                viewChamber.MonFromTime = Reader["MonTime"].ToString();
                viewChamber.TueFromTime = Reader["TueTime"].ToString();
                viewChamber.WedFromTime = Reader["WedTime"].ToString();
                viewChamber.ThuFromTime = Reader["ThuTime"].ToString();
                viewChamber.FriFromTime = Reader["FriTime"].ToString();
                viewChamber.ManageAppointment = GetChamberManageAppointmentInfo(doctorId, viewChamber.Id);
                viewDoctorChambers.Add(viewChamber);
            }
            return viewDoctorChambers;
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
                manageAppointment.OrderExpireDate = Convert.ToDateTime(Reader["ExpireDate"]);
            }
            return manageAppointment;
        }
        private List<ViewDoctors> GetDoctorSearchDetails(int doctorId)
        {
            Connection.Close();
            Query = "SELECT * FROM ViewDoctors WHERE Id = @id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("id", doctorId);
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
                viewDoctor.ManageAppointment = GetManageAppointmentInfo(doctorId, viewDoctor.MedicalId);
                viewDoctors.Add(viewDoctor);
            }
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
            }
            return manageAppointment;
        }
        public List<int> GetAllDoctorSearchResult(string location)
        {
            Query = "SELECT DISTINCT Id FROM ViewDoctors WHERE Address LIKE '%'+@address+'%' OR Area LIKE '%'+@area+'%' OR City LIKE '%'+@city+'%'";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("address", location);
            Command.Parameters.AddWithValue("area", location);
            Command.Parameters.AddWithValue("city", location);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<int> doctorsId = new List<int>();
            while (Reader.Read())
            {
                int id = Convert.ToInt32(Reader["Id"]);
                doctorsId.Add(id);
            }

            if (doctorsId.Count > 0)
            {
                Connection.Close();
                Query = "SELECT DISTINCT DoctorId FROM DoctorsChambers WHERE Address LIKE '%'+@address+'%' OR Area LIKE '%'+@area+'%' OR City LIKE '%'+@city+'%'";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("address", location);
                Command.Parameters.AddWithValue("area", location);
                Command.Parameters.AddWithValue("city", location);
                Connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    int id = Convert.ToInt32(Reader["DoctorId"]);
                    if (doctorsId.Contains(id) == false)
                    {
                        doctorsId.Add(id);
                    } 
                }
            }
            else if (doctorsId.Count == 0)
            {
                Connection.Close();
                Query = "SELECT DISTINCT DoctorId FROM DoctorsChambers WHERE Address LIKE '%'+@address+'%' OR Area LIKE '%'+@area+'%' OR City LIKE '%'+@city+'%'";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("address", location);
                Command.Parameters.AddWithValue("area", location);
                Command.Parameters.AddWithValue("city", location);
                Connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    int id = Convert.ToInt32(Reader["DoctorId"]);
                    doctorsId.Add(id);
                }
            }
            Reader.Close();
            Connection.Close();
            return doctorsId;
        }
        public Doctors GetDoctorDetailsSearchResult(int id)
        {
            Query = "SELECT * FROM Doctors WHERE Id = @id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("id", id);
            Connection.Open();
            Reader = Command.ExecuteReader();
            Doctors doctor = null;
            while (Reader.Read())
            {
                doctor = new Doctors();
                doctor.Id = Convert.ToInt32(Reader["Id"]);
                doctor.ImagePath = Reader["ImagePath"].ToString();
                doctor.Name = Reader["Name"].ToString();
                doctor.Gender = Reader["Gender"].ToString();
                doctor.Title = Reader["Title"].ToString();
                doctor.Speciality = Reader["Speciality"].ToString();
                doctor.Email = Reader["Email"].ToString();
                doctor.MobileNo = Reader["MobileNo"].ToString();
                doctor.PhoneNo = Reader["PhoneNo"].ToString();
                doctor.BmdcReg = Reader["BmdcReg"].ToString();
                doctor.OtherSpecification = Reader["OtherSpecification"].ToString();
                doctor.ViewDoctors = GetDoctorSearchDetails(doctor.Id);
                doctor.ViewDoctorChambers = GetDoctorChamberSearchDetails(doctor.Id);
            }
            Reader.Close();
            Connection.Close();
            return doctor;
        }
        public bool IsUserOldPasswordValid(int userId, string oldPassword)
        {
            Query = "SELECT * FROM UserAccounts WHERE UserId = @userId AND Password = @oldPassword";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("userId", userId);
            Command.Parameters.AddWithValue("oldPassword", oldPassword);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;
        }

        public int ChangeUserPassword(ChangeUserPassword changeUserPassword)
        {
            Query = "UPDATE UserAccounts SET Password = @newPassword WHERE UserId = @userId AND Password = @oldPassword";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("newPassword", changeUserPassword.NewPassword);
            Command.Parameters.AddWithValue("userId", changeUserPassword.UserId);
            Command.Parameters.AddWithValue("oldPassword", changeUserPassword.OldPassword);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public BookAppointment GetMedicalChamberForAppoint(int doctorId, int? medicalId)
        {
            Query = "SELECT * FROM ViewDoctors WHERE Id = @doctorId AND MedicalId = @medicalId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Command.Parameters.AddWithValue("medicalId", medicalId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            BookAppointment bookAppointment = null;
            while (Reader.Read())
            {
                bookAppointment = new BookAppointment();
                bookAppointment.DoctorId = Convert.ToInt32(Reader["Id"]);
                bookAppointment.DoctorName = Reader["Name"].ToString();
                bookAppointment.ChamberId = Convert.ToInt32(Reader["MedicalId"]);
                bookAppointment.ChamberName = Reader["MedicalName"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return bookAppointment;
        }

        public BookAppointment GetDoctorChamberForAppoint(int doctorId, int? doctorChmaberId)
        {
            Query = "SELECT * FROM ViewDoctorChamber WHERE DoctorId = @doctorId AND Id = @doctorChmaberId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("doctorId", doctorId);
            Command.Parameters.AddWithValue("doctorChmaberId", doctorChmaberId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            BookAppointment bookAppointment = null;
            while (Reader.Read())
            {
                bookAppointment = new BookAppointment();
                bookAppointment.DoctorId = Convert.ToInt32(Reader["DoctorId"]);
                bookAppointment.DoctorName = Reader["Name"].ToString();
                bookAppointment.ChamberId = Convert.ToInt32(Reader["Id"]);
                bookAppointment.ChamberName = Reader["ChamberName"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return bookAppointment;
        }

        public int ConfirmBookAppointment(BookAppointment bookAppointment)
        {
            try
            {
                Query =
                    "INSERT INTO BookAppointment VALUES(@doctorId, @doctorName, @chamberId, @chamberName, @userId, @userName, @mobileNo, @schedule, @appointDate, @appointPrice, @paymentMethod, @trxId, @serialNo, @status)";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("doctorId", bookAppointment.DoctorId);
                Command.Parameters.AddWithValue("doctorName", bookAppointment.DoctorName);
                Command.Parameters.AddWithValue("chamberId", bookAppointment.ChamberId);
                Command.Parameters.AddWithValue("chamberName", bookAppointment.ChamberName);
                Command.Parameters.AddWithValue("userId", bookAppointment.UserId);
                Command.Parameters.AddWithValue("userName", bookAppointment.UserName);
                Command.Parameters.AddWithValue("mobileNo", bookAppointment.MobileNo);
                Command.Parameters.AddWithValue("schedule", bookAppointment.Schedule);
                Command.Parameters.AddWithValue("appointDate", bookAppointment.AppointDate);
                Command.Parameters.AddWithValue("appointPrice", bookAppointment.AppointPrice);
                Command.Parameters.AddWithValue("paymentMethod", bookAppointment.PaymentMethod);
                Command.Parameters.AddWithValue("trxId", bookAppointment.TrxId);
                Command.Parameters.AddWithValue("serialNo", bookAppointment.SerialNo);
                Command.Parameters.AddWithValue("status", bookAppointment.Status);
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

        public List<BookAppointment> GetUserAppointments(int userId)
        {
            Query = "SELECT * FROM BookAppointment WHERE UserId = @userId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("userId", userId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<BookAppointment> bookAppointments = new List<BookAppointment>();
            while (Reader.Read())
            {
                BookAppointment bookAppointment = new BookAppointment();
                bookAppointment.BookAppointId = Convert.ToInt32(Reader["Id"]);
                bookAppointment.DoctorId = Convert.ToInt32(Reader["DoctorId"]);
                bookAppointment.DoctorName = Reader["DoctorName"].ToString();
                bookAppointment.ChamberName = Reader["ChamberName"].ToString();
                bookAppointment.Schedule = Reader["Schedule"].ToString();
                bookAppointment.AppointDate = Reader["AppointDate"].ToString();
                bookAppointment.SerialNo = Convert.ToInt32(Reader["SerialNo"]);
                bookAppointment.Status = Reader["Status"].ToString();
                DateTime appointDate = Convert.ToDateTime(bookAppointment.AppointDate);
                if (DateTime.Now.Date <= appointDate)
                {
                    bookAppointments.Add(bookAppointment);
                }
            }
            Reader.Close();
            Connection.Close();
            return bookAppointments;
        }

        public List<BookAppointment> GetUserAllAppointments(int userId)
        {
            Query = "SELECT * FROM BookAppointment WHERE UserId = @userId AND NOT Status = 'Pending' ORDER BY AppointDate DESC";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("userId", userId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<BookAppointment> bookAppointments = new List<BookAppointment>();
            while (Reader.Read())
            {
                BookAppointment bookAppointment = new BookAppointment();
                bookAppointment.DoctorName = Reader["DoctorName"].ToString();
                bookAppointment.ChamberName = Reader["ChamberName"].ToString();
                bookAppointment.Schedule = Reader["Schedule"].ToString();
                bookAppointment.AppointDate = Reader["AppointDate"].ToString();
                bookAppointment.SerialNo = Convert.ToInt32(Reader["SerialNo"]);
                bookAppointments.Add(bookAppointment);
            }
            Reader.Close();
            Connection.Close();
            return bookAppointments;
        }
        public UserAccount GetBloodDonateStatus(int userId)
        {
            Query = "SELECT WantToGiveBlood, BloodGroup, Location FROM UserAccounts WHERE UserId = @userId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("userId", userId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            UserAccount userAccount = null;
            while (Reader.Read())
            {
                userAccount = new UserAccount();
                userAccount.WantToGiveBlood = Convert.ToBoolean(Reader["WantToGiveBlood"]);
                userAccount.BloodGroup = Reader["BloodGroup"].ToString();
                userAccount.Location = Reader["Location"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return userAccount;
        }
        public int UpdateBloodDonateStatus(UserAccount userAccount)
        {
            int rowAffected = 0;
            try
            {
                Query = "UPDATE UserAccounts SET WantToGiveBlood = @wantToGiveBlood, BloodGroup = @bloodGroup, Location = @location WHERE UserId = @userId";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("wantToGiveBlood", userAccount.WantToGiveBlood);
                Command.Parameters.AddWithValue("bloodGroup", userAccount.BloodGroup);
                Command.Parameters.AddWithValue("location", userAccount.Location);
                Command.Parameters.AddWithValue("userId", userAccount.UserId);
                Connection.Open();
                rowAffected = Command.ExecuteNonQuery();
                Connection.Close();
                return rowAffected;
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    Connection.Close();
                }
                return rowAffected;
            }
        }
    }
}