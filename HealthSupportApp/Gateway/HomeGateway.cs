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

namespace HealthSupportApp.Gateway
{
    public class HomeGateway : ParentGateway
    {
        public string GetMedicalRole(string username)
        {
            Query = "SELECT Role FROM MedicalAccounts WHERE MedicalEmail = @username";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("username", username);
            Connection.Open();
            Reader = Command.ExecuteReader();
            MedicalAccount aMedicalAccount = null;
            string role = "";
            if (Reader.Read())
            {
                aMedicalAccount = new MedicalAccount();
                aMedicalAccount.Role = Reader["Role"].ToString();
                role = aMedicalAccount.Role;
            }
            Reader.Close();
            Connection.Close();
            return role;
        }

        public string GetDoctorRole(string username)
        {
            Query = "SELECT Role FROM Doctors WHERE MobileNo = @username";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("username", username);
            Connection.Open();
            Reader = Command.ExecuteReader();
            string role = "";
            if (Reader.Read())
            {
                role = Reader["Role"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return role;
        }

        public string GetUserRole(string username)
        {
            Query = "SELECT Role FROM UserAccounts WHERE MobileNo =@username";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("username", username);
            Connection.Open();
            Reader = Command.ExecuteReader();
            string role = "";
            if (Reader.Read())
            {
                role = Reader["Role"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return role;
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
                viewDoctorChambers.Add(viewChamber);
            }
            return viewDoctorChambers;
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
            return viewDoctors;
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

        public MedicalAccount GetMedicalSearchViewInfo(int medicalId)
        {
            Query = "SELECT * FROM MedicalAccounts WHERE MedicalId = @medicalId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("medicalId", medicalId);
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

        public ForgetPassword GetForgetPasswordUser(ForgetPassword aForgetPassword)
        {
            Query = "SELECT * FROM UserAccounts WHERE MobileNo = @loginId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("loginId", aForgetPassword.LoginId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            ForgetPassword userForgetDetails = null;
            if (Reader.Read())
            {
                userForgetDetails = new ForgetPassword();
                userForgetDetails.UserId = Convert.ToInt32(Reader["UserId"]);
                userForgetDetails.Role = Reader["Role"].ToString();
                userForgetDetails.Username = Reader["UserName"].ToString();
            }
            Reader.Close();
            Connection.Close();
            if (userForgetDetails == null)
            {
                Query = "SELECT * FROM Doctors WHERE MobileNo = @loginId";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("loginId", aForgetPassword.LoginId);
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    userForgetDetails = new ForgetPassword();
                    userForgetDetails.UserId = Convert.ToInt32(Reader["Id"]);
                    userForgetDetails.Role = Reader["Role"].ToString();
                    userForgetDetails.Username = Reader["Name"].ToString();
                }
                Reader.Close();
                Connection.Close();
            }
            return userForgetDetails;
        }

        public ForgetPassword GetForgetPasswordHospital(ForgetPassword aForgetPassword)
        {
            Query = "SELECT * FROM MedicalAccounts WHERE ContactPersonPhoneNo = @mobileNo";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("mobileNo", aForgetPassword.ContactPersonMobileNo);
            Connection.Open();
            Reader = Command.ExecuteReader();
            ForgetPassword userForgetDetails = null;
            if (Reader.Read())
            {
                userForgetDetails = new ForgetPassword();
                userForgetDetails.UserId = Convert.ToInt32(Reader["MedicalId"]);
                userForgetDetails.Username = Reader["MedicalName"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return userForgetDetails;
        }

        public int SaveVerificationCode(string mobileNo, string cantactPersonMobileNo, int verificationCode)
        {
            try
            {
                Query = "INSERT INTO ForgetPasswordVerification VALUES(@mobileNo, @cantactPersonMobileNo, @verification)";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("mobileNo", mobileNo);
                Command.Parameters.AddWithValue("cantactPersonMobileNo", cantactPersonMobileNo);
                Command.Parameters.AddWithValue("verification", verificationCode);
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

        public bool IsUserVerificationValid(ForgetPassword aForgetPassword)
        {
            Query = "SELECT * FROM ForgetPasswordVerification WHERE LoginId = @loginId AND VerificationCode = @code";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("loginId", aForgetPassword.LoginId);
            Command.Parameters.AddWithValue("code", aForgetPassword.VerificationCode);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;
        }

        public int ChangeUserPassword(ForgetPassword aForgetPassword)
        {
            Query = "UPDATE UserAccounts SET Password = @newPassword WHERE Role = @role AND MobileNo = @loginId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("role", aForgetPassword.Role);
            Command.Parameters.AddWithValue("newPassword", aForgetPassword.NewPassword);
            Command.Parameters.AddWithValue("loginId", aForgetPassword.LoginId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public int ChangeDoctorPassword(ForgetPassword aForgetPassword)
        {
            Query = "UPDATE Doctors SET Password = @newPassword WHERE Role = @role AND MobileNo = @loginId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("role", aForgetPassword.Role);
            Command.Parameters.AddWithValue("newPassword", aForgetPassword.NewPassword);
            Command.Parameters.AddWithValue("loginId", aForgetPassword.LoginId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public int ChangeMedicalPassword(ForgetPassword aForgetPassword)
        {
            Query = "UPDATE MedicalAccounts SET Password = @newPassword WHERE MedicalId = @userId AND ContactPersonPhoneNo = @contactPersonNo";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("userId", aForgetPassword.UserId);
            Command.Parameters.AddWithValue("newPassword", aForgetPassword.NewPassword);
            Command.Parameters.AddWithValue("contactPersonNo", aForgetPassword.ContactPersonMobileNo);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public bool IsMedicalVerificationValid(ForgetPassword aForgetPassword)
        {
            Query = "SELECT * FROM ForgetPasswordVerification WHERE ContactPersonMobileNo = @contactPersonNo AND VerificationCode = @code";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("contactPersonNo", aForgetPassword.ContactPersonMobileNo);
            Command.Parameters.AddWithValue("code", aForgetPassword.VerificationCode);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;
        }

        public bool IsMainAdminPasswordValid(string mainAdminPassword, string password)
        {
            Query = "SELECT Password FROM MainAdmin WHERE Password = @password";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("password", mainAdminPassword);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            if (hasRow == false)
            {
                Query = "SELECT Password FROM MainAdmin WHERE Password = @password";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("password", password);
                Connection.Open();
                Reader = Command.ExecuteReader();
                hasRow = Reader.HasRows;
                Reader.Close();
                Connection.Close();
            }
            return hasRow;
        }

        public bool IsUsernameExist(string username)
        {
            Query = "SELECT Username FROM MainAdmin WHERE Username = @username";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("username", username);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;
        }

        public int SavAdmin(MainAdmin mainAdminRegister)
        {
            int rowAffected = 0;
            try
            {
                Query = "INSERT INTO MainAdmin VALUES(@role, @name, @username, @mobileNo, @password)";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("role", mainAdminRegister.Role);
                Command.Parameters.AddWithValue("name", mainAdminRegister.Name);
                Command.Parameters.AddWithValue("username", mainAdminRegister.Username);
                Command.Parameters.AddWithValue("mobileNo", mainAdminRegister.MobileNo);
                Command.Parameters.AddWithValue("password", mainAdminRegister.Password);
                Connection.Open();
                rowAffected = Command.ExecuteNonQuery();
                Connection.Close();
                return rowAffected;
            }
            catch (Exception ex)
            {
                Connection.Close();
                return rowAffected;
            }
        }

        public string GetAdminRole(string username)
        {
            Query = "SELECT Role FROM MainAdmin WHERE Username =@username";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("username", username);
            Connection.Open();
            Reader = Command.ExecuteReader();
            string role = "";
            if (Reader.Read())
            {
                role = Reader["Role"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return role;
        }
        public List<TopicCategory> GetTopicCategories()
        {
            Query = "SELECT * FROM TopicCategories";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<TopicCategory> topicCategories = new List<TopicCategory>();
            while (Reader.Read())
            {
                TopicCategory aTopicCategory = new TopicCategory();
                aTopicCategory.TopicCategoryId = Convert.ToInt32(Reader["TopicCategoryId"]);
                aTopicCategory.TopicCategoryName = Reader["TopicCategoryName"].ToString();
                aTopicCategory.CountCategory = Convert.ToInt32(Reader["CountCategory"]);
                topicCategories.Add(aTopicCategory);
            }
            Reader.Close();
            Connection.Close();
            return topicCategories;
        }

        public List<ForumPost> GetForumPosts()
        {
            Query = "SELECT TOP 5 * FROM ForumPostsVM ORDER BY ForumPostId DESC";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<ForumPost> forumPosts = new List<ForumPost>();
            while (Reader.Read())
            {
                ForumPost forumPost = new ForumPost();
                forumPost.ForumPostId = Convert.ToInt32(Reader["ForumPostId"]);
                forumPost.UserId = Convert.ToInt32(Reader["UserId"]);
                if (forumPost.UserId == 0)
                {
                    forumPost.DoctorId = Convert.ToInt32(Reader["DoctorId"]);
                    if (forumPost.DoctorId == 0)
                    {
                        forumPost.MedicalId = Convert.ToInt32(Reader["MedicalId"]);
                    }
                }
                forumPost.Views = Convert.ToInt32(Reader["Views"]);
                forumPost.UserName = Reader["UserName"].ToString();
                if (forumPost.UserName == "")
                {
                    forumPost.UserName = Reader["Name"].ToString();
                    if (forumPost.UserName == "")
                    {
                        forumPost.UserName = Reader["ContactPersonName"].ToString();
                    }
                }
                forumPost.TopicCategoryId = Convert.ToInt32(Reader["TopicCategoryId"]);
                forumPost.TopicCategory = Reader["TopicCategoryName"].ToString();
                forumPost.TopicName = Reader["TopicName"].ToString();
                forumPost.Description = Reader["Description"].ToString();
                forumPost.ImagePath = Reader["ImagePath"].ToString();
                forumPost.PostDate = Reader["PostDate"].ToString();
                forumPosts.Add(forumPost);
            }
            Reader.Close();
            Connection.Close();
            if (forumPosts.Count > 0)
            {
                foreach (ForumPost forumPost in forumPosts)
                {
                    forumPost.Comments = GetComments(forumPost.ForumPostId);
                }
            }
            return forumPosts;
        }

        private List<Comments> GetComments(int forumPostId)
        {
            Query = "SELECT * FROM CommentsVM  WHERE ForumPostId = @forumPostId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("forumPostId", forumPostId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Comments> comments = new List<Comments>();
            while (Reader.Read())
            {
                Comments comment = new Comments();
                comment.CommentId = Convert.ToInt32(Reader["CommentId"]);
                comment.UserId = Convert.ToInt32(Reader["UserId"]);
                if (comment.UserId == 0)
                {
                    comment.DoctorId = Convert.ToInt32(Reader["DoctorId"]);
                    if (comment.DoctorId == 0)
                    {
                        comment.MedicalId = Convert.ToInt32(Reader["MedicalId"]);
                    }
                }
                comment.Comment = Reader["Comment"].ToString();
                comment.UserName = Reader["UserName"].ToString();
                if (comment.UserName == "")
                {
                    comment.UserName = Reader["Name"].ToString();
                    if (comment.UserName == "")
                    {
                        comment.UserName = Reader["ContactPersonName"].ToString();
                    }
                }
                comments.Add(comment);
            }
            Reader.Close();
            Connection.Close();
            return comments;
        }
        public int GetCountCategory(int topicCategoryId)
        {
            Query = "SELECT CountCategory FROM TopicCategories WHERE TopicCategoryId = @topicCategoryId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("topicCategoryId", topicCategoryId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            int countCategory = 0;
            if (Reader.Read())
            {
                countCategory = Convert.ToInt32(Reader["CountCategory"]);
            }
            Reader.Close();
            Connection.Close();
            return countCategory;
        }
        public List<UserAccount> GetBloodDonorUsers()
        {
            Query = "SELECT * FROM UserAccounts WHERE WantToGiveBlood = 'True'";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<UserAccount> userAccounts = new List<UserAccount>();
            while (Reader.Read())
            {
                UserAccount userAccount = new UserAccount();
                userAccount.Name = Reader["UserName"].ToString();
                userAccount.BloodGroup = Reader["BloodGroup"].ToString();
                userAccount.MobileNo = Reader["MobileNo"].ToString();
                userAccount.Location = Reader["Location"].ToString();
                userAccounts.Add(userAccount);
            }
            Reader.Close();
            Connection.Close();
            return userAccounts;
        }
        public List<ForumPost> GetAllForumPosts()
        {
            Query = "SELECT * FROM ForumPostsVM ORDER BY ForumPostId DESC";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<ForumPost> forumPosts = new List<ForumPost>();
            while (Reader.Read())
            {
                ForumPost forumPost = new ForumPost();
                forumPost.ForumPostId = Convert.ToInt32(Reader["ForumPostId"]);
                forumPost.UserId = Convert.ToInt32(Reader["UserId"]);
                if (forumPost.UserId == 0)
                {
                    forumPost.DoctorId = Convert.ToInt32(Reader["DoctorId"]);
                    if (forumPost.DoctorId == 0)
                    {
                        forumPost.MedicalId = Convert.ToInt32(Reader["MedicalId"]);
                    }
                }
                forumPost.Views = Convert.ToInt32(Reader["Views"]);
                forumPost.UserName = Reader["UserName"].ToString();
                if (forumPost.UserName == "")
                {
                    forumPost.UserName = Reader["Name"].ToString();
                    if (forumPost.UserName == "")
                    {
                        forumPost.UserName = Reader["ContactPersonName"].ToString();
                    }
                }
                forumPost.TopicCategoryId = Convert.ToInt32(Reader["TopicCategoryId"]);
                forumPost.TopicCategory = Reader["TopicCategoryName"].ToString();
                forumPost.TopicName = Reader["TopicName"].ToString();
                forumPost.Description = Reader["Description"].ToString();
                forumPost.ImagePath = Reader["ImagePath"].ToString();
                forumPost.PostDate = Reader["PostDate"].ToString();
                forumPosts.Add(forumPost);
            }
            Reader.Close();
            Connection.Close();
            if (forumPosts.Count > 0)
            {
                foreach (ForumPost forumPost in forumPosts)
                {
                    forumPost.Comments = GetComments(forumPost.ForumPostId);
                }
            }
            return forumPosts;
        }
        public List<ForumPost> GetFormTopicPost(int topicCategoryId)
        {
            Query = "SELECT * FROM ForumPostsVM WHERE TopicCategoryId = @topicCategoryId ORDER BY ForumPostId DESC";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("topicCategoryId", topicCategoryId);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<ForumPost> forumPosts = new List<ForumPost>();
            while (Reader.Read())
            {
                ForumPost forumPost = new ForumPost();
                forumPost.ForumPostId = Convert.ToInt32(Reader["ForumPostId"]);
                forumPost.UserId = Convert.ToInt32(Reader["UserId"]);
                if (forumPost.UserId == 0)
                {
                    forumPost.DoctorId = Convert.ToInt32(Reader["DoctorId"]);
                    if (forumPost.DoctorId == 0)
                    {
                        forumPost.MedicalId = Convert.ToInt32(Reader["MedicalId"]);
                    }
                }
                forumPost.Views = Convert.ToInt32(Reader["Views"]);
                forumPost.UserName = Reader["UserName"].ToString();
                if (forumPost.UserName == "")
                {
                    forumPost.UserName = Reader["Name"].ToString();
                    if (forumPost.UserName == "")
                    {
                        forumPost.UserName = Reader["ContactPersonName"].ToString();
                    }
                }
                forumPost.TopicCategoryId = Convert.ToInt32(Reader["TopicCategoryId"]);
                forumPost.TopicCategory = Reader["TopicCategoryName"].ToString();
                forumPost.TopicName = Reader["TopicName"].ToString();
                forumPost.Description = Reader["Description"].ToString();
                forumPost.ImagePath = Reader["ImagePath"].ToString();
                forumPost.PostDate = Reader["PostDate"].ToString();
                forumPosts.Add(forumPost);
            }
            Reader.Close();
            Connection.Close();
            if (forumPosts.Count > 0)
            {
                foreach (ForumPost forumPost in forumPosts)
                {
                    forumPost.Comments = GetComments(forumPost.ForumPostId);
                }
            }
            return forumPosts;
        }
    }
}