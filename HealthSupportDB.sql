USE [HealthSupportDB]
GO
/****** Object:  Table [dbo].[TopicCategories]    Script Date: 23-Mar-19 7:59:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopicCategories](
	[TopicCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[TopicCategoryName] [varchar](50) NULL,
	[CountCategory] [int] NULL,
 CONSTRAINT [PK_TopicCategories] PRIMARY KEY CLUSTERED 
(
	[TopicCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctors]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](50) NULL,
	[Name] [varchar](80) NULL,
	[Gender] [varchar](20) NULL,
	[Title] [varchar](80) NULL,
	[Speciality] [varchar](100) NULL,
	[Email] [varchar](80) NULL,
	[MobileNo] [varchar](20) NULL,
	[Password] [varchar](80) NULL,
	[PhoneNo] [varchar](20) NULL,
	[BmdcReg] [varchar](80) NULL,
	[OtherSpecification] [varchar](100) NULL,
	[ImagePath] [varchar](100) NULL,
	[PasswordVerified] [bit] NULL,
	[Status] [varchar](20) NULL,
	[AccountCreatedDate] [varchar](50) NULL,
 CONSTRAINT [PK_Doctors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalAccounts]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalAccounts](
	[MedicalId] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](20) NOT NULL,
	[MedicalType] [varchar](50) NOT NULL,
	[MedicalName] [varchar](100) NOT NULL,
	[ContactPersonName] [varchar](50) NOT NULL,
	[ContactPersonPosition] [varchar](50) NOT NULL,
	[ContactPersonPhoneNo] [varchar](50) NOT NULL,
	[MedicalEmail] [varchar](50) NOT NULL,
	[MedicalContact1] [varchar](11) NOT NULL,
	[MedicalContact2] [varchar](11) NULL,
	[Address] [varchar](100) NOT NULL,
	[Area] [varchar](50) NULL,
	[City] [varchar](20) NOT NULL,
	[AmbulanceService] [varchar](20) NOT NULL,
	[AmbulanceContact] [varchar](11) NULL,
	[Password] [varchar](100) NOT NULL,
	[IsEmailVerified] [bit] NOT NULL,
	[ActivationCode] [uniqueidentifier] NOT NULL,
	[Status] [varchar](20) NULL,
	[AccountCreatedDate] [varchar](50) NULL,
 CONSTRAINT [PK_dbo.MedicalAccounts] PRIMARY KEY CLUSTERED 
(
	[MedicalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAccounts]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccounts](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](50) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[MobileNo] [varchar](11) NULL,
	[Gender] [varchar](10) NULL,
	[Age] [varchar](10) NULL,
	[BloodGroup] [varchar](10) NOT NULL,
	[WantToGiveBlood] [bit] NOT NULL,
	[Location] [varchar](100) NOT NULL,
 CONSTRAINT [PK_dbo.UserAccounts] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ForumPosts]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ForumPosts](
	[ForumPostId] [int] IDENTITY(1,1) NOT NULL,
	[TopicCategoryId] [int] NULL,
	[TopicName] [varchar](120) NULL,
	[Description] [varchar](500) NULL,
	[ImagePath] [varchar](500) NULL,
	[Views] [int] NULL,
	[UserId] [int] NULL,
	[DoctorId] [int] NULL,
	[MedicalId] [int] NULL,
	[PostDate] [varchar](50) NULL,
 CONSTRAINT [PK_ForumPosts] PRIMARY KEY CLUSTERED 
(
	[ForumPostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ForumPostsVM]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[ForumPostsVM]
AS
SELECT fp.ForumPostId, fp.TopicName, fp.Description, fp.ImagePath, fp.Views, fp.PostDate, tc.TopicCategoryId, tc.TopicCategoryName, fp.UserId, ua.UserName, fp.DoctorId, d.Name, fp.MedicalId, ma.ContactPersonName FROM ((((ForumPosts AS fp JOIN TopicCategories AS tc ON fp.TopicCategoryId = tc.TopicCategoryId ) LEFT JOIN UserAccounts AS ua ON fp.UserId = ua.UserId) LEFT JOIN Doctors AS d ON fp.DoctorId = d.Id) LEFT JOIN MedicalAccounts AS ma ON fp.MedicalId = ma.MedicalId)
GO
/****** Object:  Table [dbo].[ForumComments]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ForumComments](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[DoctorId] [int] NULL,
	[MedicalId] [int] NULL,
	[ForumPostId] [int] NULL,
	[Comment] [varchar](200) NULL,
 CONSTRAINT [PK_ForumComments] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[CommentsVM]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[CommentsVM]
AS
SELECT fc.CommentId, fc.Comment, fc.ForumPostId, fc.UserId, ua.UserName, fc.DoctorId,d.Name, fc.MedicalId, ma.ContactPersonName FROM (((ForumComments AS fc LEFT JOIN UserAccounts AS ua ON fc.UserId = ua.UserId) LEFT JOIN Doctors AS d ON fc.DoctorId = d.Id) LEFT JOIN MedicalAccounts AS ma ON fc.MedicalId = ma.MedicalId)
GO
/****** Object:  Table [dbo].[ManageAppointment]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManageAppointment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SatCapacity] [int] NULL,
	[SunCapacity] [int] NULL,
	[MonCapacity] [int] NULL,
	[TueCapacity] [int] NULL,
	[WedCapacity] [int] NULL,
	[ThuCapacity] [int] NULL,
	[FriCapacity] [int] NULL,
	[SatAvailableAppoint] [int] NULL,
	[SunAvailableAppoint] [int] NULL,
	[MonAvailableAppoint] [int] NULL,
	[TueAvailableAppoint] [int] NULL,
	[WedAvailableAppoint] [int] NULL,
	[ThuAvailableAppoint] [int] NULL,
	[FriAvailableAppoint] [int] NULL,
	[OnlineAppoint] [bit] NULL,
	[UsedAppoint] [int] NULL,
	[TotalAvailableAppoint] [int] NULL,
	[DoctorId] [int] NULL,
	[MedicalId] [int] NULL,
	[DoctorChamberId] [int] NULL,
 CONSTRAINT [PK_ManageAppointment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoctorsChambers]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorsChambers](
	[Id] [int] IDENTITY(2000,1) NOT NULL,
	[DoctorId] [int] NULL,
	[ChamberName] [varchar](100) NULL,
	[Address] [varchar](100) NULL,
	[Area] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[ContactNo] [varchar](50) NULL,
	[PhoneNo] [varchar](50) NULL,
	[Fee] [varchar](50) NULL,
	[ReturningFee] [varchar](50) NULL,
	[Sat] [bit] NULL,
	[Sun] [bit] NULL,
	[Mon] [bit] NULL,
	[Tue] [bit] NULL,
	[Wed] [bit] NULL,
	[Thu] [bit] NULL,
	[Fri] [bit] NULL,
	[SatTime] [varchar](50) NULL,
	[SunTime] [varchar](50) NULL,
	[MonTime] [varchar](50) NULL,
	[TueTime] [varchar](50) NULL,
	[WedTime] [varchar](50) NULL,
	[ThuTime] [varchar](50) NULL,
	[FriTime] [varchar](50) NULL,
 CONSTRAINT [PK_DoctorsChambers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoctorAppointOrder]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorAppointOrder](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[ManageAppointId] [int] NULL,
	[AppointAmount] [int] NULL,
	[TotalPrice] [int] NULL,
	[PaymentMethod] [varchar](50) NULL,
	[TrxId] [varchar](50) NULL,
	[OrderDate] [datetime] NULL,
	[ExpireDate] [datetime] NULL,
	[RemainingAppoint] [int] NULL,
	[TotalAppoint] [int] NULL,
	[Expired] [bit] NULL,
	[Status] [varchar](50) NULL,
 CONSTRAINT [PK_DoctorAppointOrder] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[DoctorChamberOrder]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[DoctorChamberOrder] AS
SELECT da.OrderId, ma.Id,ma.DoctorId, ma.MedicalId, ma.DoctorChamberId, d.Name, maccnt.MedicalName, dc.ChamberName, da.AppointAmount, da.TotalPrice, da.PaymentMethod, da.TrxId, da. OrderDate, da.ExpireDate, da.Expired, da.Status FROM (((DoctorAppointOrder AS da JOIN ManageAppointment AS ma ON da.ManageAppointId = ma.Id AND Expired ='False') LEFT JOIN MedicalAccounts AS maccnt ON ma.MedicalId=maccnt.MedicalId) LEFT JOIN DoctorsChambers AS dc ON dc.Id = ma.DoctorChamberId) JOIN Doctors AS d ON d.Id = ma.DoctorId;
GO
/****** Object:  View [dbo].[ManageAppointVM]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[ManageAppointVM]
AS
SELECT ma.Id, ma.SatCapacity, ma.SunCapacity, ma.MonCapacity, ma.TueCapacity, ma.WedCapacity, ma.ThuCapacity, ma.FriCapacity, ma.SatAvailableAppoint,ma.SunAvailableAppoint, ma.MonAvailableAppoint, ma.TueAvailableAppoint, ma.WedAvailableAppoint, ma.ThuAvailableAppoint, ma.FriAvailableAppoint, ma.OnlineAppoint, ma.UsedAppoint, ma.TotalAvailableAppoint, ma.DoctorId, ma.MedicalId, ma.DoctorChamberId, da.OrderId, da.AppointAmount, da.RemainingAppoint, da.TotalAppoint, da.ExpireDate, da.Expired, da.Status FROM ManageAppointment AS ma JOIN DoctorAppointOrder AS da ON ma.Id = da.ManageAppointId
GO
/****** Object:  View [dbo].[ViewDoctorChamber]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[ViewDoctorChamber] AS
SELECT dc.DoctorId, d.Name, d.Speciality, d.OtherSpecification, dc.Id, dc.ChamberName, dc.Address, dc.Area, dc.City, dc.ContactNo, dc.PhoneNo, dc.Fee, dc.ReturningFee, dc.Sat, dc.Sun, dc.Mon, dc.Tue, dc.Wed, dc.Thu, dc.Fri, dc.SatTime, dc.SunTime, dc.MonTime, dc.TueTime, dc.WedTime, dc.ThuTime, dc.FriTime FROM Doctors AS d JOIN DoctorsChambers AS dc ON d.Id = dc.DoctorId;
GO
/****** Object:  View [dbo].[ChambersAppointOrder]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ChambersAppointOrder]
AS
SELECT mappoint.Id, mappoint.DoctorId, maccount.MedicalName FROM ManageAppointment AS mappoint JOIN MedicalAccounts AS maccount ON mappoint.MedicalId = maccount.MedicalId
GO
/****** Object:  View [dbo].[DoctorChambersAppointOrder]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DoctorChambersAppointOrder]
AS
SELECT mappoint.Id, mappoint.DoctorId, dc.ChamberName FROM ManageAppointment AS mappoint JOIN DoctorsChambers AS dc ON mappoint.DoctorChamberId = dc.Id
GO
/****** Object:  Table [dbo].[AssignedDoctors]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssignedDoctors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MedicalId] [int] NULL,
	[DoctorId] [int] NULL,
	[DoctorFee] [int] NULL,
	[DoctorReturningFee] [int] NULL,
	[Sat] [bit] NULL,
	[Sun] [bit] NULL,
	[Mon] [bit] NULL,
	[Tue] [bit] NULL,
	[Wed] [bit] NULL,
	[Thu] [bit] NULL,
	[Fri] [bit] NULL,
	[SatTime] [varchar](50) NULL,
	[SunTime] [varchar](50) NULL,
	[MonTime] [varchar](50) NULL,
	[TueTime] [varchar](50) NULL,
	[WedTime] [varchar](50) NULL,
	[ThuTime] [varchar](50) NULL,
	[FriTime] [varchar](50) NULL,
 CONSTRAINT [PK_AssignedDoctors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewDoctors]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE VIEW [dbo].[ViewDoctors] AS
SELECT d.Id, d.ImagePath, d.Name,d.Gender, d.Title, d.Speciality, d.Email, d.MobileNo, d.PhoneNo, d.BmdcReg, d.OtherSpecification, ad.MedicalId, ma.MedicalName, ma.Address, ma.Area, ma.City, ad.DoctorFee, ad.DoctorReturningFee, ad.Sat, ad.Sun, ad.Mon, ad.Tue, ad.Wed, ad.Thu, ad.Fri, ad.SatTime, ad.SunTime, ad.MonTime, ad.TueTime, ad.WedTime, ad.ThuTime, ad.FriTime FROM (Doctors AS d JOIN AssignedDoctors AS ad ON d.Id = ad.DoctorId) JOIN MedicalAccounts AS ma ON ad.MedicalId = ma.MedicalId;
GO
/****** Object:  Table [dbo].[BookAppointment]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookAppointment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DoctorId] [int] NULL,
	[DoctorName] [varchar](50) NULL,
	[ChamberId] [int] NULL,
	[ChamberName] [varchar](50) NULL,
	[UserId] [int] NULL,
	[UserName] [varchar](50) NULL,
	[MobileNo] [varchar](50) NULL,
	[Schedule] [varchar](50) NULL,
	[AppointDate] [varchar](50) NULL,
	[AppointPrice] [int] NULL,
	[PaymentMethod] [varchar](50) NULL,
	[TrxId] [varchar](80) NULL,
	[SerialNo] [int] NULL,
	[Status] [varchar](50) NULL,
 CONSTRAINT [PK_BookAppointment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DiagnosticServices]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiagnosticServices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ServiceName] [varchar](100) NULL,
	[MedicalId] [int] NULL,
 CONSTRAINT [PK_DiagnosticServices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoctorsSpecialities]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorsSpecialities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Speciality] [varchar](100) NULL,
 CONSTRAINT [PK_DoctorsSpecialities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ForgetPasswordVerification]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ForgetPasswordVerification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoginId] [varchar](50) NULL,
	[ContactPersonMobileNo] [varchar](50) NULL,
	[VerificationCode] [int] NULL,
 CONSTRAINT [PK_ForgetPasswordVerification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HospitalServices]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HospitalServices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ServiceName] [varchar](100) NULL,
	[MedicalId] [int] NULL,
 CONSTRAINT [PK_HospitalServices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MainAdmin]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MainAdmin](
	[AdminId] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[Username] [varchar](50) NULL,
	[MobileNo] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
 CONSTRAINT [PK_MainAdmin] PRIMARY KEY CLUSTERED 
(
	[AdminId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalConsultants]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalConsultants](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConsultantName] [varchar](100) NULL,
	[MedicalId] [int] NULL,
 CONSTRAINT [PK_MedicalConsultants] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalEmergency]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalEmergency](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TwentyFourService] [varchar](20) NULL,
	[EmergencyService] [varchar](20) NULL,
	[AmbulanceService] [varchar](20) NULL,
	[SeatCapacity] [int] NULL,
	[EmergencyPhoneNo] [varchar](20) NULL,
	[EmergencyPhoneNo2] [varchar](20) NULL,
	[AmbulancePhoneNo] [varchar](20) NULL,
	[AmbulancePhoneNo2] [varchar](20) NULL,
	[MedicalId] [int] NULL,
 CONSTRAINT [PK_MedicalServices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalFacilities]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalFacilities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FacilityName] [varchar](100) NULL,
	[MedicalId] [int] NULL,
 CONSTRAINT [PK_MedicalFacilities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OtherMedicalServices]    Script Date: 23-Mar-19 7:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OtherMedicalServices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ServiceName] [varchar](100) NULL,
	[MedicalId] [int] NULL,
 CONSTRAINT [PK_OtherMedicalServices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AssignedDoctors] ON 

INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (2003, 8, 1004, 1200, 1000, 1, 1, 1, 1, 1, 0, 0, N'6.00 p.m. - 10.00 p.m.', N'6.00 p.m. - 10.00 p.m.', N'6.00 p.m. - 10.00 p.m.', N'6.00 p.m. - 10.00 p.m.', N'6.00 p.m. - 10.00 p.m.', N'', N'')
INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (2004, 8, 1005, 1000, 800, 0, 0, 0, 1, 1, 1, 0, N'', N'', N'', N'6.00 p.m. - 9.00 p.m.', N'6.00 p.m. - 9.00 p.m.', N'6.00 p.m. - 9.00 p.m.', N'')
INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (2005, 5, 1008, 1000, 800, 0, 0, 1, 1, 1, 0, 0, N'', N'', N'3.00 p.m. - 6.00 p.m.', N'3.00 p.m. - 6.00 p.m.', N'3.00 p.m. - 6.00 p.m.', N'', N'')
INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (2006, 5, 1009, 700, 500, 1, 1, 1, 0, 0, 0, 0, N'4.00 p.m. - 7.00 p.m.', N'4.00 p.m. - 7.00 p.m.', N'4.00 p.m. - 7.00 p.m.', N'', N'', N'', N'')
INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (4006, 1010, 3003, 800, 500, 1, 1, 0, 0, 0, 0, 0, N'6.00 p.m. - 8.00 p.m.', N'6.00 p.m. - 9.00 p.m.', N'', N'', N'', N'', N'')
INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (4007, 1010, 4003, 1000, 800, 1, 0, 1, 0, 0, 0, 0, N'4.00 p.m. - 7.00 p.m.', N'', N'4.00 p.m. - 7.00 p.m.', N'', N'', N'', N'')
INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (5003, 5, 5003, 800, 700, 1, 1, 1, 1, 0, 0, 0, N'5.00 p.m. - 8.00 p.m.', N'5.00 p.m. - 8.00 p.m.', N'5.00 p.m. - 8.00 p.m.', N'5.00 p.m. - 8.00 p.m.', N'', N'', N'')
INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (5004, 5, 5004, 1000, 800, 1, 1, 1, 0, 0, 0, 0, N'4.00 p.m. - 7.00 p.m.', N'4.00 p.m. - 7.00 p.m.', N'4.00 p.m. - 7.00 p.m.', N'', N'', N'', N'')
INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (5005, 5, 5005, 1200, 1000, 1, 1, 0, 1, 0, 0, 0, N'5.00 p.m. - 8.00 p.m.', N'5.00 p.m. - 8.00 p.m.', N'', N'5.00 p.m. - 8.00 p.m.', N'', N'', N'')
INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (5006, 5, 5006, 800, 700, 1, 1, 1, 1, 1, 0, 0, N'6.00 p.m. - 9.00 p.m.', N'6.00 p.m. - 9.00 p.m.', N'6.00 p.m. - 9.00 p.m.', N'6.00 p.m. - 9.00 p.m.', N'6.00 p.m. - 9.00 p.m.', N'', N'')
INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (5007, 5, 5007, 1000, 800, 1, 1, 1, 1, 1, 0, 0, N'10.00 a.m. - 1.00 p.m.', N'10.00 a.m. - 1.00 p.m.', N'10.00 a.m. - 1.00 p.m.', N'10.00 a.m. - 1.00 p.m.', N'10.00 a.m. - 1.00 p.m.', N'', N'')
INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (5008, 5, 5008, 1200, 1000, 1, 1, 1, 1, 1, 0, 0, N'6.00 p.m. - 9.00 p.m.', N'6.00 p.m. - 9.00 p.m.', N'6.00 p.m. - 9.00 p.m.', N'6.00 p.m. - 9.00 p.m.', N'6.00 p.m. - 9.00 p.m.', N'', N'')
INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (5009, 5, 5009, 1500, 1200, 1, 1, 1, 1, 1, 0, 0, N'7.00 p.m. - 10.00 p.m.', N'7.00 p.m. - 10.00 p.m.', N'7.00 p.m. - 10.00 p.m.', N'7.00 p.m. - 10.00 p.m.', N'7.00 p.m. - 10.00 p.m.', N'', N'')
INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (5010, 2010, 5010, 1000, 800, 0, 0, 1, 1, 1, 0, 0, N'', N'', N'11.00 a.m. - 2.00 p.m.', N'11.00 a.m. - 2.00 p.m.', N'11.00 a.m. - 2.00 p.m.', N'', N'')
INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (5011, 2010, 5011, 1200, 1000, 1, 1, 1, 1, 1, 0, 0, N'9.00 a.m. - 1.00 p.m.', N'9.00 a.m. - 1.00 p.m.', N'9.00 a.m. - 1.00 p.m.', N'9.00 a.m. - 1.00 p.m.', N'9.00 a.m. - 1.00 p.m.', N'', N'')
INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (5012, 2010, 5012, 1000, 700, 1, 1, 1, 1, 0, 0, 0, N'7.00 p.m. - 10.00 p.m.', N'7.00 p.m. - 10.00 p.m.', N'7.00 p.m. - 10.00 p.m.', N'7.00 p.m. - 10.00 p.m.', N'', N'', N'')
INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (5013, 2010, 5013, 800, 700, 1, 1, 1, 1, 0, 0, 0, N'7.00 p.m. - 11.00 p.m.', N'7.00 p.m. - 11.00 p.m.', N'7.00 p.m. - 11.00 p.m.', N'7.00 p.m. - 11.00 p.m.', N'', N'', N'')
INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (5014, 2010, 5014, 1000, 900, 1, 1, 1, 1, 0, 0, 0, N'10.00 a.m. - 1.00 p.m.', N'10.00 a.m. - 1.00 p.m.', N'6.00 p.m. - 9.00 p.m.', N'6.00 p.m. - 9.00 p.m.', N'', N'', N'')
INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (5015, 2010, 5015, 1200, 1000, 0, 1, 1, 1, 0, 0, 0, N'', N'7.00 p.m. - 11.00 p.m.', N'7.00 p.m. - 11.00 p.m.', N'7.00 p.m. - 11.00 p.m.', N'', N'', N'')
INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (5016, 2010, 5016, 1000, 800, 1, 1, 1, 0, 0, 0, 0, N'9.00 p.m. - 11.00 p.m.', N'9.00 p.m. - 11.00 p.m.', N'9.00 p.m. - 11.00 p.m.', N'', N'', N'', N'')
INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (5017, 7, 5017, 1000, 800, 1, 1, 1, 0, 0, 0, 0, N'6.00 p.m. - 10.00 p.m.', N'6.00 p.m. - 10.00 p.m.', N'6.00 p.m. - 10.00 p.m.', N'', N'', N'', N'')
INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (5018, 7, 5018, 800, 700, 0, 0, 1, 1, 0, 0, 0, N'', N'', N'6.00 p.m. - 9.00 p.m.', N'6.00 p.m. - 9.00 p.m.', N'', N'', N'')
INSERT [dbo].[AssignedDoctors] ([Id], [MedicalId], [DoctorId], [DoctorFee], [DoctorReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (5019, 7, 5021, 1000, 900, 1, 1, 1, 0, 0, 0, 0, N'3.00 p.m. - 8.00 p.m.', N'3.00 p.m. - 8.00 p.m.', N'3.00 p.m. - 8.00 p.m.', N'', N'', N'', N'')
SET IDENTITY_INSERT [dbo].[AssignedDoctors] OFF
SET IDENTITY_INSERT [dbo].[BookAppointment] ON 

INSERT [dbo].[BookAppointment] ([Id], [DoctorId], [DoctorName], [ChamberId], [ChamberName], [UserId], [UserName], [MobileNo], [Schedule], [AppointDate], [AppointPrice], [PaymentMethod], [TrxId], [SerialNo], [Status]) VALUES (1, 2, N'Dr. Amimul Ihasan', 5, N'Epic Health Care', 1005, N'Ihasan Zarif', N'01858828805', N'Saturday, 6.00 p.m. - 10.00 p.m.', N'Thursday, November 8, 2018', 20, N'BKash', N'regt34treg', 1, N'Canceled')
INSERT [dbo].[BookAppointment] ([Id], [DoctorId], [DoctorName], [ChamberId], [ChamberName], [UserId], [UserName], [MobileNo], [Schedule], [AppointDate], [AppointPrice], [PaymentMethod], [TrxId], [SerialNo], [Status]) VALUES (4, 2, N'Dr. Amimul Ihasan', 5, N'Epic Health Care', 1006, N'Amimul Ihasan', N'01834224406', N'Saturday, 6.00 p.m. - 10.00 p.m.', N'Tuesday, November 6, 2018', 20, N'BKash', N'xcfsfdg54w5g', 3, N'Canceled')
INSERT [dbo].[BookAppointment] ([Id], [DoctorId], [DoctorName], [ChamberId], [ChamberName], [UserId], [UserName], [MobileNo], [Schedule], [AppointDate], [AppointPrice], [PaymentMethod], [TrxId], [SerialNo], [Status]) VALUES (8, 2, N'Dr. Amimul Ihasan', 2000, N'Chevron Lab', 1008, N'Imran Morshed', N'01857818610', N'Wednesday, 6.00 p.m. - 10.00 p.m.', N'Wednesday, October 10, 2018', 20, N'Rocket', N'eg54g45gtfdg', 3, N'Accepted')
INSERT [dbo].[BookAppointment] ([Id], [DoctorId], [DoctorName], [ChamberId], [ChamberName], [UserId], [UserName], [MobileNo], [Schedule], [AppointDate], [AppointPrice], [PaymentMethod], [TrxId], [SerialNo], [Status]) VALUES (1002, 2, N'Dr. Amimul Ihasan', 2000, N'Chevron Lab', 1006, N'Amimul Ihasan', N'01834224406', N'Tuesday, 6.00 p.m. - 9.00 p.m.', N'Tuesday, December 4, 2018', 20, N'BKash', N'fdgw53gsdf', 1, N'Accepted')
INSERT [dbo].[BookAppointment] ([Id], [DoctorId], [DoctorName], [ChamberId], [ChamberName], [UserId], [UserName], [MobileNo], [Schedule], [AppointDate], [AppointPrice], [PaymentMethod], [TrxId], [SerialNo], [Status]) VALUES (2002, 2, N'Dr. Amimul Ihasan', 2000, N'Chevron Lab', 1008, N'Imran Morshed', N'01857818610', N'Wednesday, 6.00 p.m. - 10.00 p.m.', N'Wednesday, December 5, 2018', 20, N'BKash', N'56LOO34XJ', 2, N'Accepted')
INSERT [dbo].[BookAppointment] ([Id], [DoctorId], [DoctorName], [ChamberId], [ChamberName], [UserId], [UserName], [MobileNo], [Schedule], [AppointDate], [AppointPrice], [PaymentMethod], [TrxId], [SerialNo], [Status]) VALUES (2003, 2, N'Dr. Amimul Ihasan', 2000, N'Chevron Lab', 2007, N'Samia Islam', N'01834224402', N'Wednesday, 6.00 p.m. - 10.00 p.m.', N'Wednesday, December 5, 2018', 20, N'BKash', N'521FFA4H', 3, N'Accepted')
INSERT [dbo].[BookAppointment] ([Id], [DoctorId], [DoctorName], [ChamberId], [ChamberName], [UserId], [UserName], [MobileNo], [Schedule], [AppointDate], [AppointPrice], [PaymentMethod], [TrxId], [SerialNo], [Status]) VALUES (2004, 2, N'Dr. Amimul Ihasan', 5, N'Epic Health Care', 2010, N'Robiul Hossain', N'01834224403', N'Saturday, 5.00 p.m. - 8.00 p.m.', N'Saturday, December 8, 2018', 20, N'BKash', N'fdgw53gsdf', 1, N'Accepted')
INSERT [dbo].[BookAppointment] ([Id], [DoctorId], [DoctorName], [ChamberId], [ChamberName], [UserId], [UserName], [MobileNo], [Schedule], [AppointDate], [AppointPrice], [PaymentMethod], [TrxId], [SerialNo], [Status]) VALUES (2005, 2, N'Dr. Amimul Ihasan', 5, N'Epic Health Care', 2005, N'Moinul Hasan', N'01829381633', N'Sunday, 5.00 p.m. - 8.00 p.m.', N'Sunday, December 9, 2018', 20, N'Rocket', N'hsdfg623gd6', 2, N'Accepted')
INSERT [dbo].[BookAppointment] ([Id], [DoctorId], [DoctorName], [ChamberId], [ChamberName], [UserId], [UserName], [MobileNo], [Schedule], [AppointDate], [AppointPrice], [PaymentMethod], [TrxId], [SerialNo], [Status]) VALUES (3002, 3003, N'Dr. Sahnoor Meher', 3002, N'Bell View Hospital Ltd', 1006, N'Amimul Ihasan', N'01834224406', N'Monday, 6.00 p.m. - 9.00 p.m.', N'Monday, December 17, 2018', 20, N'BKash', N'hsdfg623gd6', 8, N'Accepted')
INSERT [dbo].[BookAppointment] ([Id], [DoctorId], [DoctorName], [ChamberId], [ChamberName], [UserId], [UserName], [MobileNo], [Schedule], [AppointDate], [AppointPrice], [PaymentMethod], [TrxId], [SerialNo], [Status]) VALUES (3006, 3003, N'Dr. Sahnoor Meher', 3002, N'Bell View Hospital Ltd', 4005, N'Helen Akter', N'01938018677', N'Monday, 6.00 p.m. - 9.00 p.m.', N'Monday, December 17, 2018', 20, N'BKash', N'fdgw53gsdf', 7, N'Accepted')
INSERT [dbo].[BookAppointment] ([Id], [DoctorId], [DoctorName], [ChamberId], [ChamberName], [UserId], [UserName], [MobileNo], [Schedule], [AppointDate], [AppointPrice], [PaymentMethod], [TrxId], [SerialNo], [Status]) VALUES (3007, 4003, N'Dr. Mubtaseem', 1010, N'Imperial Hospital', 4006, N'Sayedul Alam', N'01834224422', N'Saturday, 4.00 p.m. - 7.00 p.m.', N'Saturday, December 15, 2018', 20, N'Rocket', N'521FFA4H', 1, N'Pending')
INSERT [dbo].[BookAppointment] ([Id], [DoctorId], [DoctorName], [ChamberId], [ChamberName], [UserId], [UserName], [MobileNo], [Schedule], [AppointDate], [AppointPrice], [PaymentMethod], [TrxId], [SerialNo], [Status]) VALUES (3008, 4003, N'Dr. Mubtaseem', 1010, N'Imperial Hospital', 4005, N'Helen Akter', N'01938018677', N'Saturday, 4.00 p.m. - 7.00 p.m.', N'Saturday, December 15, 2018', 20, N'Rocket', N'521FFA4Y', 2, N'Accepted')
INSERT [dbo].[BookAppointment] ([Id], [DoctorId], [DoctorName], [ChamberId], [ChamberName], [UserId], [UserName], [MobileNo], [Schedule], [AppointDate], [AppointPrice], [PaymentMethod], [TrxId], [SerialNo], [Status]) VALUES (3009, 3003, N'Dr. Sahnoor Meher', 1010, N'Imperial Hospital', 1008, N'Imran Morshed', N'01857818610', N'Saturday, 6.00 p.m. - 8.00 p.m.', N'Saturday, December 15, 2018', 20, N'BKash', N'521FFA4G', 1, N'Accepted')
INSERT [dbo].[BookAppointment] ([Id], [DoctorId], [DoctorName], [ChamberId], [ChamberName], [UserId], [UserName], [MobileNo], [Schedule], [AppointDate], [AppointPrice], [PaymentMethod], [TrxId], [SerialNo], [Status]) VALUES (4008, 3003, N'Dr. Sahnoor Meher', 1010, N'Imperial Hospital', 4005, N'Helen Akter', N'01938018677', N'Saturday, 6.00 p.m. - 8.00 p.m.', N'Saturday, January 5, 2019', 20, N'BKash', N'521FFA4Y', 4005, N'Canceled')
INSERT [dbo].[BookAppointment] ([Id], [DoctorId], [DoctorName], [ChamberId], [ChamberName], [UserId], [UserName], [MobileNo], [Schedule], [AppointDate], [AppointPrice], [PaymentMethod], [TrxId], [SerialNo], [Status]) VALUES (5008, 3003, N'Dr. Sahnoor Meher', 3002, N'Bell View Hospital Ltd', 2007, N'Samia Islam', N'01834224402', N'Monday, 6.00 p.m. - 9.00 p.m.', N'Monday, January 7, 2019', 20, N'BKash', N'56LOO34XJ', 9, N'Canceled')
INSERT [dbo].[BookAppointment] ([Id], [DoctorId], [DoctorName], [ChamberId], [ChamberName], [UserId], [UserName], [MobileNo], [Schedule], [AppointDate], [AppointPrice], [PaymentMethod], [TrxId], [SerialNo], [Status]) VALUES (5009, 3003, N'Dr. Sahnoor Meher', 1010, N'Imperial Hospital', 2006, N'Belal Hossain', N'01834224401', N'Saturday, 6.00 p.m. - 8.00 p.m.', N'Saturday, January 12, 2019', 20, N'BKash', N'521FFA4Y', 1, N'Canceled')
INSERT [dbo].[BookAppointment] ([Id], [DoctorId], [DoctorName], [ChamberId], [ChamberName], [UserId], [UserName], [MobileNo], [Schedule], [AppointDate], [AppointPrice], [PaymentMethod], [TrxId], [SerialNo], [Status]) VALUES (5010, 6003, N'Dr. Amimul Ihasan', 5004, N'Chevron Lab', 5005, N'Shohidul Alam', N'01309001316', N'Monday, 4.00 p.m. - 8.00 p.m.', N'Monday, January 14, 2019', 20, N'BKash', N'521PFA4Y0', 1, N'Accepted')
SET IDENTITY_INSERT [dbo].[BookAppointment] OFF
SET IDENTITY_INSERT [dbo].[DiagnosticServices] ON 

INSERT [dbo].[DiagnosticServices] ([Id], [ServiceName], [MedicalId]) VALUES (1012, N'MRI', 5)
INSERT [dbo].[DiagnosticServices] ([Id], [ServiceName], [MedicalId]) VALUES (1013, N'Pathology', 5)
INSERT [dbo].[DiagnosticServices] ([Id], [ServiceName], [MedicalId]) VALUES (1014, N'CT Scan', 5)
INSERT [dbo].[DiagnosticServices] ([Id], [ServiceName], [MedicalId]) VALUES (1015, N'Blood Test', 5)
INSERT [dbo].[DiagnosticServices] ([Id], [ServiceName], [MedicalId]) VALUES (1016, N'Digital X-Ray', 2010)
INSERT [dbo].[DiagnosticServices] ([Id], [ServiceName], [MedicalId]) VALUES (1017, N'C.T. Scan', 2010)
INSERT [dbo].[DiagnosticServices] ([Id], [ServiceName], [MedicalId]) VALUES (1018, N'M.R.I.', 2010)
INSERT [dbo].[DiagnosticServices] ([Id], [ServiceName], [MedicalId]) VALUES (1019, N'Ultrasnography', 2010)
INSERT [dbo].[DiagnosticServices] ([Id], [ServiceName], [MedicalId]) VALUES (1020, N'Endoscopy', 2010)
INSERT [dbo].[DiagnosticServices] ([Id], [ServiceName], [MedicalId]) VALUES (1021, N'E.C.G', 2010)
INSERT [dbo].[DiagnosticServices] ([Id], [ServiceName], [MedicalId]) VALUES (1022, N'Imaging', 7)
INSERT [dbo].[DiagnosticServices] ([Id], [ServiceName], [MedicalId]) VALUES (1023, N'Pathology', 7)
INSERT [dbo].[DiagnosticServices] ([Id], [ServiceName], [MedicalId]) VALUES (1024, N'Diagnosis & Treatment of Infertility', 7)
INSERT [dbo].[DiagnosticServices] ([Id], [ServiceName], [MedicalId]) VALUES (1025, N'Bronchoscopy', 7)
INSERT [dbo].[DiagnosticServices] ([Id], [ServiceName], [MedicalId]) VALUES (1026, N'GE Prodigy Oracle-Bone Densitometer(BMD)', 7)
INSERT [dbo].[DiagnosticServices] ([Id], [ServiceName], [MedicalId]) VALUES (1027, N'ET Scan', 8)
SET IDENTITY_INSERT [dbo].[DiagnosticServices] OFF
SET IDENTITY_INSERT [dbo].[DoctorAppointOrder] ON 

INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (1004, 3, 0, 0, N'', N'', CAST(N'1971-02-01T00:00:00.000' AS DateTime), CAST(N'1971-02-01T00:00:00.000' AS DateTime), 0, 0, 1, N'Active')
INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (2004, 1003, 0, 0, N'', N'', CAST(N'1971-02-01T00:00:00.000' AS DateTime), CAST(N'1971-02-01T00:00:00.000' AS DateTime), 0, 0, 0, N'Active')
INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (3004, 2003, 0, 0, N'', N'', CAST(N'1971-02-01T00:00:00.000' AS DateTime), CAST(N'1971-02-01T00:00:00.000' AS DateTime), 0, 0, 1, N'Active')
INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (3006, 2004, 0, 0, N'', N'', CAST(N'1971-02-01T00:00:00.000' AS DateTime), CAST(N'1971-02-01T00:00:00.000' AS DateTime), 0, 0, 1, N'Active')
INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (3007, 2004, 120, 2400, N'Rocket', N'40PO7BC', CAST(N'2018-12-02T00:00:00.000' AS DateTime), CAST(N'2018-12-30T00:00:00.000' AS DateTime), 118, 120, 0, N'Active')
INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (4004, 3003, 0, 0, N'', N'', CAST(N'1971-02-01T00:00:00.000' AS DateTime), CAST(N'1971-02-01T00:00:00.000' AS DateTime), 0, 0, 1, N'Active')
INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (5005, 3004, 0, 0, N'', N'', CAST(N'1971-02-01T00:00:00.000' AS DateTime), CAST(N'1971-02-01T00:00:00.000' AS DateTime), 0, 0, 1, N'Active')
INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (5006, 3004, 100, 2000, N'BKash', N'40PO7BC', CAST(N'2018-12-10T00:00:00.000' AS DateTime), CAST(N'2019-01-07T00:00:00.000' AS DateTime), 91, 100, 0, N'Active')
INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (5007, 3005, 0, 0, N'', N'', CAST(N'1971-02-01T00:00:00.000' AS DateTime), CAST(N'1971-02-01T00:00:00.000' AS DateTime), 0, 0, 1, N'Active')
INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (5008, 3005, 100, 2000, N'BKash', N'3fdg43ge', CAST(N'2018-12-10T00:00:00.000' AS DateTime), CAST(N'2019-01-07T00:00:00.000' AS DateTime), 100, 100, 0, N'Active')
INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (6005, 3005, 50, 1000, N'Rocket', N'76jhg78', CAST(N'2018-12-11T00:00:00.000' AS DateTime), CAST(N'2019-01-08T00:00:00.000' AS DateTime), 149, 150, 0, N'Active')
INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (6007, 4004, 0, 0, N'', N'', CAST(N'1971-02-01T00:00:00.000' AS DateTime), CAST(N'1971-02-01T00:00:00.000' AS DateTime), 0, 0, 1, N'Active')
INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (6009, 4005, 0, 0, N'', N'', CAST(N'1971-02-01T00:00:00.000' AS DateTime), CAST(N'1971-02-01T00:00:00.000' AS DateTime), 0, 0, 1, N'Active')
INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (6010, 4005, 100, 2000, N'Rocket', N'vt3443vg', CAST(N'2018-12-17T00:00:00.000' AS DateTime), CAST(N'2019-01-14T00:00:00.000' AS DateTime), 100, 100, 1, N'Active')
INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (7005, 4004, 50, 1000, N'Rocket', N'vt3443vg', CAST(N'2019-01-06T00:00:00.000' AS DateTime), CAST(N'2019-02-03T00:00:00.000' AS DateTime), 49, 50, 0, N'Active')
INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (7006, 5004, 0, 0, N'', N'', CAST(N'1971-02-01T00:00:00.000' AS DateTime), CAST(N'1971-02-01T00:00:00.000' AS DateTime), 0, 0, 1, N'Active')
INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (7007, 5004, 1, 20, N'Rocket', N'40PO7BT', CAST(N'2019-01-06T00:00:00.000' AS DateTime), CAST(N'2019-02-03T00:00:00.000' AS DateTime), 1, 1, 0, N'Pending')
INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (7008, 5005, 0, 0, N'', N'', CAST(N'1971-02-01T00:00:00.000' AS DateTime), CAST(N'1971-02-01T00:00:00.000' AS DateTime), 0, 0, 1, N'Active')
INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (7009, 5005, 100, 2000, N'Rocket', N'50APO7BC', CAST(N'2019-01-07T00:00:00.000' AS DateTime), CAST(N'2019-02-04T00:00:00.000' AS DateTime), 100, 100, 0, N'Active')
INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (7010, 5006, 0, 0, N'', N'', CAST(N'1971-02-01T00:00:00.000' AS DateTime), CAST(N'1971-02-01T00:00:00.000' AS DateTime), 0, 0, 1, N'Active')
INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (7011, 5006, 120, 2400, N'Rocket', N'AE45TR75TT', CAST(N'2019-01-07T00:00:00.000' AS DateTime), CAST(N'2019-02-04T00:00:00.000' AS DateTime), 120, 120, 0, N'Active')
INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (7012, 5007, 0, 0, N'', N'', CAST(N'1971-02-01T00:00:00.000' AS DateTime), CAST(N'1971-02-01T00:00:00.000' AS DateTime), 0, 0, 1, N'Active')
INSERT [dbo].[DoctorAppointOrder] ([OrderId], [ManageAppointId], [AppointAmount], [TotalPrice], [PaymentMethod], [TrxId], [OrderDate], [ExpireDate], [RemainingAppoint], [TotalAppoint], [Expired], [Status]) VALUES (7013, 5007, 50, 1000, N'BKash', N'67SFPO7BC', CAST(N'2019-01-07T00:00:00.000' AS DateTime), CAST(N'2019-02-04T00:00:00.000' AS DateTime), 49, 50, 0, N'Active')
SET IDENTITY_INSERT [dbo].[DoctorAppointOrder] OFF
SET IDENTITY_INSERT [dbo].[Doctors] ON 

INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (2, N'Doctor', N'Dr. Imran Morshed', N'Male', N'Assistant Professor', N'Cardiology (Heart, Cardiac Surgery, Cardiovascular, Hypertension, Blood Pressure)', N'imran@mail.com', N'01834224423', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'01715740300', N'654321', N'MBBS, Chittagong Medical College', N'~/Image/doctor15297517-f675-40d2-94d8-ebf96eaf18c3.png', 1, N'Active', N'Tuesday, October 2, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (1004, N'Doctor', N'DR M.SHAIDER RUSHNI', N'Male', N'Kidney Disease Specialists, Chittagong Medical College and Hospital', N'Nephrology (Kidney, Ureter, Urinary Bladder)', N'rushni123@gmail.com', N'01829621240', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'123456', N'MBBS', N'', 1, N'Active', N'Sunday, December 2, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (1005, N'Doctor', N'PROFESSOR DR M.A ROUF', N'Male', N'Professor, Bangabandhu Memorial Hospital.', N'Cardiology (Heart, Cardiac Surgery, Cardiovascular, Hypertension, Blood Pressure)', N'sunny@gmail.com', N'01681884372', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'123457', N'MBBS, FCPS (Medicine), DTIDD,  FRTTM (London), Post Doctor Fellow (Intervention Cardiology)', N'', 0, N'Active', N'Sunday, December 2, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (1006, N'Doctor', N'Samia hoque', N'Female', N'Professor', N'Dentistry (Dental, Orthodontics, Maxillofacial Surgery, Scaling)', N'samiahq@mail.com', N'01834224410', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'123458', N'MBBS', N'', 0, N'Active', N'Sunday, December 2, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (1007, N'Doctor', N'Dr. Ismat Ara', N'Female', N'Assistant Professor', N'Medicine (All Diseases of Adults)', N'ismat@gmail.com', N'01834224415', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'123459', N'MBBS, USTC', N'', 0, N'Active', N'Sunday, December 2, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (1008, N'Doctor', N'Dr. Naimul Alam', N'Male', N'Professor', N'General Physician (All or any common diseases and health issues)', N'naim@gmail.com', N'01811889072', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'123460', N'MBBS', N'', 0, N'Active', N'Sunday, December 2, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (1009, N'Doctor', N'Dr. Najnin Akter', N'Female', N'Professor', N'Dermatology (Skin, Venereology, Sexual, Hair, Allergy)', N'najnin@gmail.com', N'01834224411', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'123461', N'MBBS', N'', 1, N'Active', N'Sunday, December 2, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (2003, N'Doctor', N'Dr. Anisul Alam', N'Male', N'Professor', N'Medicine (All Diseases of Adults)', N'anis@mail.com', N'01834224420', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'123462', N'MBBS', N'', 0, N'Active', N'Sunday, December 9, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (3003, N'Doctor', N'Dr. Sahnoor Meher', N'Female', N'Professor', N'Medicine (All Diseases of Adults)', N'sahnoor@gmail.com', N'01309001310', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'123450', N'FCPS', N'', 1, N'Active', N'Monday, December 10, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (4003, N'Doctor', N'Dr. Mubtaseem', N'Male', N'Professor', N'Haematology (Blood related diseases)', N'mubtaseem@gmail.com', N'01309001311', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'12351', N'FCPS', N'~/Image/doctorbded8301-c545-4ab9-add9-fc2bdd3ae8da.png', 1, N'Active', N'Monday, December 10, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (5003, N'Doctor', N'Dr. Monsurul Alam', N'Male', N'Professor, Chittagong Medical College', N'Dermatology (Skin, Venereology, Sexual, Hair, Allergy)', N'monsurul123@mail.com', N'01834224501', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'20001', N'MBBS, FCPS', N'', 1, N'Active', N'Wednesday, December 12, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (5004, N'Doctor', N'Dr. Abdullah Al Mahmud', N'Male', N'Assistant Professor, Hepatology, Chittagong Medical College', N'Hepatology (Liver, Gallbladder, Biliary system)', N'abdullah@mail.com', N'01834224502', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'20002', N'MD (Hepatology)', N'', 0, N'Active', N'Wednesday, December 12, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (5005, N'Doctor', N'Dr. Mohammad Habibur Rahman', N'Male', N'Assistant Professor, Chitagong Medical College', N'Medicine (All Diseases of Adults)', N'habibur@gmail.com', N'01834224503', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'20003', N'MBBS, FCPS (Medicine)', N'', 0, N'Active', N'Wednesday, December 12, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (5006, N'Doctor', N'Dr. Tania Tazrin', N'Female', N'Medical Consultant', N'Medicine (All Diseases of Adults)', N'tania@gmail.com', N'01834224504', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'20004', N'MBBS, BCS (Health), FCPS (Medicine)', N'', 0, N'Active', N'Wednesday, December 12, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (5007, N'Doctor', N'Dr. Muhammad Abdullah Al Anis', N'Male', N'Retired Assistant Professor, CEMKH', N'Haematology (Blood related diseases)', N'anis@mail.com', N'01834224505', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'20005', N'FCPS (Hematology), MBBS (CMH)', N'', 0, N'Active', N'Wednesday, December 12, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (5008, N'Doctor', N'Dr. Asifa Ali', N'Female', N'Consultant', N'Gynaecology and Obstetrics (Pregnancy, Menstrual, Uterus, Female)', N'asifa@gmail.com', N'01834224506', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'20006', N'MBBS, BCS (Health), MCPS, FCPS', N'', 0, N'Active', N'Wednesday, December 12, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (5009, N'Doctor', N'Dr. Shahidul Haque', N'Male', N'Assistant Professor', N'General Surgery (Incision, Operation)', N'shahidul@mail.com', N'01834224507', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'20007', N'MBBS, FCPS (Surgery)', N'', 0, N'Active', N'Wednesday, December 12, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (5010, N'Doctor', N'Dr. Mahbub Kamal Chowdhury', N'Male', N'Professor', N'Medicine (All Diseases of Adults)', N'mahbub@gmail.com', N'01834224601', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'30001', N'MBBS, FCPS (Medicine)', N'', 0, N'Active', N'Wednesday, December 12, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (5011, N'Doctor', N'Dr. Mehrunnissa Khanom', N'Female', N'Medicine Specialist', N'Medicine (All Diseases of Adults)', N'mehrunnissa@gmail.com', N'01834224602', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'30002', N'MBBS (Gold Medalist), MRCP (UK)', N'', 0, N'Active', N'Wednesday, December 12, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (5012, N'Doctor', N'Dr. Md. Iftikher Hossain Khan', N'Male', N'Assistant Professor', N'Endocrinology (Diabetes, Hormones, Thyroid, etc.)', N'iftikher@gmail.com', N'01834224603', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'30003', N'MBBS, M.D (E.M)', N'', 0, N'Active', N'Wednesday, December 12, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (5013, N'Doctor', N'Dr. Jamal Ahmed', N'Male', N'Asstt. Professor of Cardiology', N'Cardiology (Heart, Cardiac Surgery, Cardiovascular, Hypertension, Blood Pressure)', N'jamal@mail.com', N'01834224604', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'30004', N'MBBS D. Card (D.U)', N'', 0, N'Active', N'Wednesday, December 12, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (5014, N'Doctor', N'Dr. Nil Kantha Bhatachharja', N'Male', N'Professor & Head of ENT, CMH', N'ENT (Ear, Nose & Throat, Otorhinolaryngology)', N'nilkantha@gmail.com', N'01834224605', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'30005', N'MBBS, FCPS', N'', 0, N'Active', N'Wednesday, December 12, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (5015, N'Doctor', N'Dr. Zeenat Meraj Chowdhury', N'Male', N'Associate Professor of Dermatology BGC Trust Medical College & Hospital', N'Dermatology (Skin, Venereology, Sexual, Hair, Allergy)', N'meraj@gmail.com', N'01834224606', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'30006', N'MBBS, MCPS, Dip Derm (Bangkok), FCPS', N'', 0, N'Active', N'Wednesday, December 12, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (5016, N'Doctor', N'Dr. Shahena Akter', N'Female', N'Assistant Professor', N'Gynaecology and Obstetrics (Pregnancy, Menstrual, Uterus, Female)', N'shahena@mail.com', N'01834224607', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'30007', N'MBBS, DGO, FCPS (Gynae & Obs.)', N'', 0, N'Active', N'Wednesday, December 12, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (5017, N'Doctor', N'DR. NAZMA NASRIN ', N'Female', N'Medical Consultant', N'Gynaecology and Obstetrics (Pregnancy, Menstrual, Uterus, Female)', N'nasrin@mail.com', N'01834224701', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'40001', N'MBBS(DHAKA), FCPS(GYNAE)', N'', 0, N'Active', N'Wednesday, December 12, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (5018, N'Doctor', N'DR. FERDOUSI BEGUM', N'Female', N'OBSTETRICS AND GYNAECOLOGY Specailist', N'Gynaecology and Obstetrics (Pregnancy, Menstrual, Uterus, Female)', N'ferdousi@gmail.com', N'01834224702', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'40002', N'MBBS, DGO, FCPS', N'', 0, N'Active', N'Wednesday, December 12, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (5021, N'Doctor', N'PROF. DR. M A AZHAR', N'Male', N'Professor & Head, Department of Medicine (EX)', N'Medicine (All Diseases of Adults)', N'azhar@mail.com', N'01834224704', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'40004', N'MBBS, FCPS(MEDICINE), FACP, FRCP(EDIN)', N'', 0, N'Active', N'Wednesday, December 12, 2018')
INSERT [dbo].[Doctors] ([Id], [Role], [Name], [Gender], [Title], [Speciality], [Email], [MobileNo], [Password], [PhoneNo], [BmdcReg], [OtherSpecification], [ImagePath], [PasswordVerified], [Status], [AccountCreatedDate]) VALUES (6003, N'Doctor', N'Dr. Amimul Ihasan', N'Male', N'Professor', N'Ophthalmology (Eye, Vision, Cataracts, etc.)', N'zarif@mail.com', N'01834224404', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'', N'120001', N'MBBS, FCPS', N'', 1, N'Active', N'Monday, January 7, 2019')
SET IDENTITY_INSERT [dbo].[Doctors] OFF
SET IDENTITY_INSERT [dbo].[DoctorsChambers] ON 

INSERT [dbo].[DoctorsChambers] ([Id], [DoctorId], [ChamberName], [Address], [Area], [City], [ContactNo], [PhoneNo], [Fee], [ReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (2001, 1004, N'Popular Diagnostic Center', N'Medical Road, Chawkbazar', N'Chawkbazar', N'Chittagong', N'01930388282', N'01753843824', N'600', N'400', 1, 1, 0, 0, 0, 0, 0, N'5.00 p.m. - 8.00 p.m.', N'5.00 p.m. - 8.00 p.m.', N'', N'', N'', N'', N'')
INSERT [dbo].[DoctorsChambers] ([Id], [DoctorId], [ChamberName], [Address], [Area], [City], [ContactNo], [PhoneNo], [Fee], [ReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (3002, 3003, N'Bell View Hospital Ltd', N'Near GEC moore', N'O R Nizam Road', N'Chittagong', N'01834225500', N'', N'800', N'700', 0, 0, 1, 1, 0, 0, 0, N'', N'', N'6.00 p.m. - 9.00 p.m.', N'6.00 p.m. - 9.00 p.m.', N'', N'', N'')
INSERT [dbo].[DoctorsChambers] ([Id], [DoctorId], [ChamberName], [Address], [Area], [City], [ContactNo], [PhoneNo], [Fee], [ReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (4002, 2, N'Chevron Lab', N'Probortok Moore', N'Panchlaish', N'Chittagong', N'01834224405', N'', N'1000', N'800', 1, 1, 0, 0, 0, 0, 0, N'10.00 a.m. - 1.00 p.m.', N'10.00 a.m. - 1.00 p.m.', N'', N'', N'', N'', N'')
INSERT [dbo].[DoctorsChambers] ([Id], [DoctorId], [ChamberName], [Address], [Area], [City], [ContactNo], [PhoneNo], [Fee], [ReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (5003, 4003, N'Chevron Lab', N'Probortok Moore', N'Panchlaish', N'Chattogram', N'01834225500', N'', N'1200', N'1000', 0, 0, 1, 1, 1, 0, 0, N'', N'', N'10.00 a.m. - 1.00 p.m.', N'10.00 a.m. - 1.00 p.m.', N'10.00 a.m. - 1.00 p.m.', N'', N'')
INSERT [dbo].[DoctorsChambers] ([Id], [DoctorId], [ChamberName], [Address], [Area], [City], [ContactNo], [PhoneNo], [Fee], [ReturningFee], [Sat], [Sun], [Mon], [Tue], [Wed], [Thu], [Fri], [SatTime], [SunTime], [MonTime], [TueTime], [WedTime], [ThuTime], [FriTime]) VALUES (5004, 6003, N'Chevron Lab', N'Probortok Moore', N'Panchlaish', N'Chittagong', N'01834224404', N'', N'1000', N'700', 0, 0, 1, 1, 1, 0, 0, N'', N'', N'4.00 p.m. - 8.00 p.m.', N'4.00 p.m. - 8.00 p.m.', N'4.00 p.m. - 8.00 p.m.', N'', N'')
SET IDENTITY_INSERT [dbo].[DoctorsChambers] OFF
SET IDENTITY_INSERT [dbo].[DoctorsSpecialities] ON 

INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (1, N'Allergy & Immunology')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (2, N'Anesthesia')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (3, N'Cardiology (Heart, Cardiac Surgery, Cardiovascular, Hypertension, Blood Pressure)')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (4, N'Colorectal Surgery (Surgery of Anal Canal, Rectum, etc.)')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (5, N'Dentistry (Dental, Orthodontics, Maxillofacial Surgery, Scaling)')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (6, N'Dermatology (Skin, Venereology, Sexual, Hair, Allergy)')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (7, N'Endocrinology (Diabetes, Hormones, Thyroid, etc.)')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (8, N'ENT (Ear, Nose & Throat, Otorhinolaryngology)')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (9, N'Gastroenterology (Stomach, Pancreas and Intestine)')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (10, N'General Physician (All or any common diseases and health issues)')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (11, N'General Surgery (Incision, Operation)')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (12, N'Gynaecologic Oncology (Cancer of Female Reproductive System)')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (13, N'Gynaecology and Obstetrics (Pregnancy, Menstrual, Uterus, Female)')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (14, N'Haematology (Blood related diseases)')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (15, N'Hepato-Biliary- Pancreatic Surgery')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (16, N'Hepatology (Liver, Gallbladder, Biliary system)')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (17, N'Infectious Diseases')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (18, N'Infertility')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (19, N'Medicine (All Diseases of Adults)')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (20, N'Neonatology (New Born Issues)')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (1001, N'Nephrology (Kidney, Ureter, Urinary Bladder)')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (1002, N'Nutritionist (Food, Diet, Weight Management)')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (1003, N'Ophthalmology (Eye, Vision, Cataracts, etc.)')
INSERT [dbo].[DoctorsSpecialities] ([Id], [Speciality]) VALUES (1004, N'Physiotherapy')
SET IDENTITY_INSERT [dbo].[DoctorsSpecialities] OFF
SET IDENTITY_INSERT [dbo].[ForgetPasswordVerification] ON 

INSERT [dbo].[ForgetPasswordVerification] ([Id], [LoginId], [ContactPersonMobileNo], [VerificationCode]) VALUES (2, N'01834224404', NULL, 662992)
INSERT [dbo].[ForgetPasswordVerification] ([Id], [LoginId], [ContactPersonMobileNo], [VerificationCode]) VALUES (3, N'01834224404', NULL, 476668)
INSERT [dbo].[ForgetPasswordVerification] ([Id], [LoginId], [ContactPersonMobileNo], [VerificationCode]) VALUES (4, N'01834224404', NULL, 165176)
INSERT [dbo].[ForgetPasswordVerification] ([Id], [LoginId], [ContactPersonMobileNo], [VerificationCode]) VALUES (5, N'01834224404', NULL, 577394)
INSERT [dbo].[ForgetPasswordVerification] ([Id], [LoginId], [ContactPersonMobileNo], [VerificationCode]) VALUES (6, N'', N'01834224404', 131329)
INSERT [dbo].[ForgetPasswordVerification] ([Id], [LoginId], [ContactPersonMobileNo], [VerificationCode]) VALUES (1002, N'01938018677', N'', 622843)
SET IDENTITY_INSERT [dbo].[ForgetPasswordVerification] OFF
SET IDENTITY_INSERT [dbo].[ForumComments] ON 

INSERT [dbo].[ForumComments] ([CommentId], [UserId], [DoctorId], [MedicalId], [ForumPostId], [Comment]) VALUES (1, 1006, 0, 0, 5, N'you should go doctor')
INSERT [dbo].[ForumComments] ([CommentId], [UserId], [DoctorId], [MedicalId], [ForumPostId], [Comment]) VALUES (3, 0, 2, 0, 3, N'this doesn''t look good')
INSERT [dbo].[ForumComments] ([CommentId], [UserId], [DoctorId], [MedicalId], [ForumPostId], [Comment]) VALUES (4, 1008, 0, 0, 11, N'hi, Doctor!')
INSERT [dbo].[ForumComments] ([CommentId], [UserId], [DoctorId], [MedicalId], [ForumPostId], [Comment]) VALUES (5, 1006, 0, 0, 11, N'i have same problem')
INSERT [dbo].[ForumComments] ([CommentId], [UserId], [DoctorId], [MedicalId], [ForumPostId], [Comment]) VALUES (6, 1006, 0, 0, 10, N'i don''t understand what you write')
INSERT [dbo].[ForumComments] ([CommentId], [UserId], [DoctorId], [MedicalId], [ForumPostId], [Comment]) VALUES (7, 1006, 0, 0, 10, N'you can see this related posts')
INSERT [dbo].[ForumComments] ([CommentId], [UserId], [DoctorId], [MedicalId], [ForumPostId], [Comment]) VALUES (8, 1006, 0, 0, 3, N'this will be helpful for me too')
INSERT [dbo].[ForumComments] ([CommentId], [UserId], [DoctorId], [MedicalId], [ForumPostId], [Comment]) VALUES (9, 1006, 0, 0, 12, N'thanks')
INSERT [dbo].[ForumComments] ([CommentId], [UserId], [DoctorId], [MedicalId], [ForumPostId], [Comment]) VALUES (1005, 1006, 0, 0, 5, N'What happened?')
INSERT [dbo].[ForumComments] ([CommentId], [UserId], [DoctorId], [MedicalId], [ForumPostId], [Comment]) VALUES (2005, 0, 0, 5, 1012, N'We have some tips. Please you can visit our medical')
INSERT [dbo].[ForumComments] ([CommentId], [UserId], [DoctorId], [MedicalId], [ForumPostId], [Comment]) VALUES (2006, 5005, 0, 0, 1012, N'if you find anything please share it in forum')
INSERT [dbo].[ForumComments] ([CommentId], [UserId], [DoctorId], [MedicalId], [ForumPostId], [Comment]) VALUES (2008, 5005, 0, 0, 2012, N'Please go to general physician at 1st')
INSERT [dbo].[ForumComments] ([CommentId], [UserId], [DoctorId], [MedicalId], [ForumPostId], [Comment]) VALUES (2009, 5005, 0, 0, 2012, N'Please go to general physician at 1st')
INSERT [dbo].[ForumComments] ([CommentId], [UserId], [DoctorId], [MedicalId], [ForumPostId], [Comment]) VALUES (2010, 5005, 0, 0, 2012, N'Please go to general physician at 1st')
SET IDENTITY_INSERT [dbo].[ForumComments] OFF
SET IDENTITY_INSERT [dbo].[ForumPosts] ON 

INSERT [dbo].[ForumPosts] ([ForumPostId], [TopicCategoryId], [TopicName], [Description], [ImagePath], [Views], [UserId], [DoctorId], [MedicalId], [PostDate]) VALUES (3, 1, N'I have allergy problems', N'fdsafsdfj skladfhslajdkf jklsadfh lasdjfh jsdfhl
sd;kfjskldaf kldsjf;askldfj 
dsfkljasd lf;sdj sdklfjlksd ljhfdjks', N'', 0, 1006, 0, 0, N'Monday, October 8, 2018')
INSERT [dbo].[ForumPosts] ([ForumPostId], [TopicCategoryId], [TopicName], [Description], [ImagePath], [Views], [UserId], [DoctorId], [MedicalId], [PostDate]) VALUES (5, 1002, N'My eyes have some problems', N'jksdfh sdjkfha sdkjfh sjkdfhasdkj
dskfjsldfh flskdfjsldfh
Mummy''s alright
Dad''s alright....
', N' ', 0, 1006, 0, 0, N'Monday, October 8, 2018')
INSERT [dbo].[ForumPosts] ([ForumPostId], [TopicCategoryId], [TopicName], [Description], [ImagePath], [Views], [UserId], [DoctorId], [MedicalId], [PostDate]) VALUES (10, 1, N'Information about allergy', N'jfhsdjkfh sfjsdhfsks fhsdfj jdfhsdkjsdhsd. jksdhfjksdff sdjhfsjkd. This is very important to know ...sdjkfsdkhd', N'', 0, 0, 2, 0, N'Monday, October 8, 2018')
INSERT [dbo].[ForumPosts] ([ForumPostId], [TopicCategoryId], [TopicName], [Description], [ImagePath], [Views], [UserId], [DoctorId], [MedicalId], [PostDate]) VALUES (11, 1002, N'I will tell you about caring eyes', N'ajfhajdkf sjkdfhsda fsjdfh salfjk dfsahjksdh shdlfj and then you have to sdjkfhsdflahjdf
sdfhsdfhsl. sdjkfhsdjhfj dslfjsld', N'', 0, 0, 2, 0, N'Monday, October 8, 2018')
INSERT [dbo].[ForumPosts] ([ForumPostId], [TopicCategoryId], [TopicName], [Description], [ImagePath], [Views], [UserId], [DoctorId], [MedicalId], [PostDate]) VALUES (12, 1003, N'Tips for oily skin', N'asjdh fskfhskg sdfgk gjlsd and you have to care about your skin then akdlfhsjkd
sdjkfhsdjshf fhsd lfhsfg dgjis', N'', 0, 1006, 0, 0, N'Wednesday, October 10, 2018')
INSERT [dbo].[ForumPosts] ([ForumPostId], [TopicCategoryId], [TopicName], [Description], [ImagePath], [Views], [UserId], [DoctorId], [MedicalId], [PostDate]) VALUES (1012, 1, N'How to avoid allergy problem?', N'Give some advise anyone, how could I avoid allergy problems.', N'', 0, 1006, 0, 0, N'Monday, December 10, 2018')
INSERT [dbo].[ForumPosts] ([ForumPostId], [TopicCategoryId], [TopicName], [Description], [ImagePath], [Views], [UserId], [DoctorId], [MedicalId], [PostDate]) VALUES (2012, 1003, N'How to treat my skin?', N'I have skin problem. I want to get rid of it. Please help me out by giving valuable tips.', N'', 0, 4005, 0, 0, N'Monday, December 31, 2018')
INSERT [dbo].[ForumPosts] ([ForumPostId], [TopicCategoryId], [TopicName], [Description], [ImagePath], [Views], [UserId], [DoctorId], [MedicalId], [PostDate]) VALUES (2013, 2, N'Dental Problem', N'', N'', 0, 5006, 0, 0, N'Sunday, March 3, 2019')
SET IDENTITY_INSERT [dbo].[ForumPosts] OFF
SET IDENTITY_INSERT [dbo].[HospitalServices] ON 

INSERT [dbo].[HospitalServices] ([Id], [ServiceName], [MedicalId]) VALUES (24, N'ICU', 5)
INSERT [dbo].[HospitalServices] ([Id], [ServiceName], [MedicalId]) VALUES (25, N'CCU', 5)
INSERT [dbo].[HospitalServices] ([Id], [ServiceName], [MedicalId]) VALUES (27, N'DIALYSIS', 5)
INSERT [dbo].[HospitalServices] ([Id], [ServiceName], [MedicalId]) VALUES (28, N'HDU', 5)
INSERT [dbo].[HospitalServices] ([Id], [ServiceName], [MedicalId]) VALUES (29, N'Emergency', 2010)
INSERT [dbo].[HospitalServices] ([Id], [ServiceName], [MedicalId]) VALUES (30, N'Operation Theater', 2010)
INSERT [dbo].[HospitalServices] ([Id], [ServiceName], [MedicalId]) VALUES (31, N'C.C.U.', 2010)
INSERT [dbo].[HospitalServices] ([Id], [ServiceName], [MedicalId]) VALUES (32, N'I.C.U./H.D.U.', 2010)
INSERT [dbo].[HospitalServices] ([Id], [ServiceName], [MedicalId]) VALUES (33, N'N.I.C.U.', 2010)
INSERT [dbo].[HospitalServices] ([Id], [ServiceName], [MedicalId]) VALUES (34, N'Dialysis', 2010)
INSERT [dbo].[HospitalServices] ([Id], [ServiceName], [MedicalId]) VALUES (35, N'ICU', 8)
SET IDENTITY_INSERT [dbo].[HospitalServices] OFF
SET IDENTITY_INSERT [dbo].[MainAdmin] ON 

INSERT [dbo].[MainAdmin] ([AdminId], [Role], [Name], [Username], [MobileNo], [Password]) VALUES (1, N'MainAdmin', N'Ihasan Zarif', N'xyz', N'5435435', N'123456')
INSERT [dbo].[MainAdmin] ([AdminId], [Role], [Name], [Username], [MobileNo], [Password]) VALUES (2, N'MainAdmin', N'Ihasan Zarif', N'zarif', N'01834224404', N'LcAmn6VNJpqHU2gQ7EU8sJW0uS9F5jgmoh3/HC528Wk=')
SET IDENTITY_INSERT [dbo].[MainAdmin] OFF
SET IDENTITY_INSERT [dbo].[ManageAppointment] ON 

INSERT [dbo].[ManageAppointment] ([Id], [SatCapacity], [SunCapacity], [MonCapacity], [TueCapacity], [WedCapacity], [ThuCapacity], [FriCapacity], [SatAvailableAppoint], [SunAvailableAppoint], [MonAvailableAppoint], [TueAvailableAppoint], [WedAvailableAppoint], [ThuAvailableAppoint], [FriAvailableAppoint], [OnlineAppoint], [UsedAppoint], [TotalAvailableAppoint], [DoctorId], [MedicalId], [DoctorChamberId]) VALUES (3, 0, 0, 0, 30, 30, 20, 0, 0, 0, 0, 29, 28, 20, 0, 1, 80, 77, 2, 0, 2000)
INSERT [dbo].[ManageAppointment] ([Id], [SatCapacity], [SunCapacity], [MonCapacity], [TueCapacity], [WedCapacity], [ThuCapacity], [FriCapacity], [SatAvailableAppoint], [SunAvailableAppoint], [MonAvailableAppoint], [TueAvailableAppoint], [WedAvailableAppoint], [ThuAvailableAppoint], [FriAvailableAppoint], [OnlineAppoint], [UsedAppoint], [TotalAvailableAppoint], [DoctorId], [MedicalId], [DoctorChamberId]) VALUES (1003, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1002, 5, 0)
INSERT [dbo].[ManageAppointment] ([Id], [SatCapacity], [SunCapacity], [MonCapacity], [TueCapacity], [WedCapacity], [ThuCapacity], [FriCapacity], [SatAvailableAppoint], [SunAvailableAppoint], [MonAvailableAppoint], [TueAvailableAppoint], [WedAvailableAppoint], [ThuAvailableAppoint], [FriAvailableAppoint], [OnlineAppoint], [UsedAppoint], [TotalAvailableAppoint], [DoctorId], [MedicalId], [DoctorChamberId]) VALUES (2003, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1004, 8, 0)
INSERT [dbo].[ManageAppointment] ([Id], [SatCapacity], [SunCapacity], [MonCapacity], [TueCapacity], [WedCapacity], [ThuCapacity], [FriCapacity], [SatAvailableAppoint], [SunAvailableAppoint], [MonAvailableAppoint], [TueAvailableAppoint], [WedAvailableAppoint], [ThuAvailableAppoint], [FriAvailableAppoint], [OnlineAppoint], [UsedAppoint], [TotalAvailableAppoint], [DoctorId], [MedicalId], [DoctorChamberId]) VALUES (3003, 20, 20, 10, 0, 0, 0, 0, 20, 20, 10, 0, 0, 0, 0, 1, 50, 50, 2, 0, 2002)
INSERT [dbo].[ManageAppointment] ([Id], [SatCapacity], [SunCapacity], [MonCapacity], [TueCapacity], [WedCapacity], [ThuCapacity], [FriCapacity], [SatAvailableAppoint], [SunAvailableAppoint], [MonAvailableAppoint], [TueAvailableAppoint], [WedAvailableAppoint], [ThuAvailableAppoint], [FriAvailableAppoint], [OnlineAppoint], [UsedAppoint], [TotalAvailableAppoint], [DoctorId], [MedicalId], [DoctorChamberId]) VALUES (3004, 0, 0, 40, 50, 0, 0, 0, 0, 0, 39, 50, 0, 0, 0, 1, 90, 89, 3003, 0, 3002)
INSERT [dbo].[ManageAppointment] ([Id], [SatCapacity], [SunCapacity], [MonCapacity], [TueCapacity], [WedCapacity], [ThuCapacity], [FriCapacity], [SatAvailableAppoint], [SunAvailableAppoint], [MonAvailableAppoint], [TueAvailableAppoint], [WedAvailableAppoint], [ThuAvailableAppoint], [FriAvailableAppoint], [OnlineAppoint], [UsedAppoint], [TotalAvailableAppoint], [DoctorId], [MedicalId], [DoctorChamberId]) VALUES (3005, 50, 0, 50, 0, 0, 0, 0, 49, 0, 50, 0, 0, 0, 0, 1, 100, 99, 4003, 1010, 0)
INSERT [dbo].[ManageAppointment] ([Id], [SatCapacity], [SunCapacity], [MonCapacity], [TueCapacity], [WedCapacity], [ThuCapacity], [FriCapacity], [SatAvailableAppoint], [SunAvailableAppoint], [MonAvailableAppoint], [TueAvailableAppoint], [WedAvailableAppoint], [ThuAvailableAppoint], [FriAvailableAppoint], [OnlineAppoint], [UsedAppoint], [TotalAvailableAppoint], [DoctorId], [MedicalId], [DoctorChamberId]) VALUES (4004, 20, 20, 0, 0, 0, 0, 0, 19, 20, 0, 0, 0, 0, 0, 1, 40, 39, 3003, 1010, 0)
INSERT [dbo].[ManageAppointment] ([Id], [SatCapacity], [SunCapacity], [MonCapacity], [TueCapacity], [WedCapacity], [ThuCapacity], [FriCapacity], [SatAvailableAppoint], [SunAvailableAppoint], [MonAvailableAppoint], [TueAvailableAppoint], [WedAvailableAppoint], [ThuAvailableAppoint], [FriAvailableAppoint], [OnlineAppoint], [UsedAppoint], [TotalAvailableAppoint], [DoctorId], [MedicalId], [DoctorChamberId]) VALUES (4005, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 2, 0, 4002)
INSERT [dbo].[ManageAppointment] ([Id], [SatCapacity], [SunCapacity], [MonCapacity], [TueCapacity], [WedCapacity], [ThuCapacity], [FriCapacity], [SatAvailableAppoint], [SunAvailableAppoint], [MonAvailableAppoint], [TueAvailableAppoint], [WedAvailableAppoint], [ThuAvailableAppoint], [FriAvailableAppoint], [OnlineAppoint], [UsedAppoint], [TotalAvailableAppoint], [DoctorId], [MedicalId], [DoctorChamberId]) VALUES (5004, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 4003, 0, 5003)
INSERT [dbo].[ManageAppointment] ([Id], [SatCapacity], [SunCapacity], [MonCapacity], [TueCapacity], [WedCapacity], [ThuCapacity], [FriCapacity], [SatAvailableAppoint], [SunAvailableAppoint], [MonAvailableAppoint], [TueAvailableAppoint], [WedAvailableAppoint], [ThuAvailableAppoint], [FriAvailableAppoint], [OnlineAppoint], [UsedAppoint], [TotalAvailableAppoint], [DoctorId], [MedicalId], [DoctorChamberId]) VALUES (5005, 30, 30, 30, 0, 0, 0, 0, 30, 30, 30, 0, 0, 0, 0, 1, 90, 90, 1009, 5, 0)
INSERT [dbo].[ManageAppointment] ([Id], [SatCapacity], [SunCapacity], [MonCapacity], [TueCapacity], [WedCapacity], [ThuCapacity], [FriCapacity], [SatAvailableAppoint], [SunAvailableAppoint], [MonAvailableAppoint], [TueAvailableAppoint], [WedAvailableAppoint], [ThuAvailableAppoint], [FriAvailableAppoint], [OnlineAppoint], [UsedAppoint], [TotalAvailableAppoint], [DoctorId], [MedicalId], [DoctorChamberId]) VALUES (5006, 20, 20, 20, 0, 0, 0, 0, 20, 20, 20, 0, 0, 0, 0, 1, 60, 60, 5003, 5, 0)
INSERT [dbo].[ManageAppointment] ([Id], [SatCapacity], [SunCapacity], [MonCapacity], [TueCapacity], [WedCapacity], [ThuCapacity], [FriCapacity], [SatAvailableAppoint], [SunAvailableAppoint], [MonAvailableAppoint], [TueAvailableAppoint], [WedAvailableAppoint], [ThuAvailableAppoint], [FriAvailableAppoint], [OnlineAppoint], [UsedAppoint], [TotalAvailableAppoint], [DoctorId], [MedicalId], [DoctorChamberId]) VALUES (5007, 0, 0, 20, 20, 0, 0, 0, 0, 0, 19, 20, 0, 0, 0, 1, 40, 39, 6003, 0, 5004)
SET IDENTITY_INSERT [dbo].[ManageAppointment] OFF
SET IDENTITY_INSERT [dbo].[MedicalAccounts] ON 

INSERT [dbo].[MedicalAccounts] ([MedicalId], [Role], [MedicalType], [MedicalName], [ContactPersonName], [ContactPersonPosition], [ContactPersonPhoneNo], [MedicalEmail], [MedicalContact1], [MedicalContact2], [Address], [Area], [City], [AmbulanceService], [AmbulanceContact], [Password], [IsEmailVerified], [ActivationCode], [Status], [AccountCreatedDate]) VALUES (5, N'Medical', N'General Hospital/Medical', N'Epic Health Care', N'Nurul Mostafa', N'IT Manager', N'01715740302', N'epic@mail.com', N'01715740303', N'01715740305', N'Probortok Moore, Opposite Medical', N'Panchlaish', N'Chittagong', N'Available', N'01715740304', N'73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=', 1, N'8abea267-2422-4c67-8a2a-83d096fcdb72', N'Active', N'Tuesday, October 2, 2018')
INSERT [dbo].[MedicalAccounts] ([MedicalId], [Role], [MedicalType], [MedicalName], [ContactPersonName], [ContactPersonPosition], [ContactPersonPhoneNo], [MedicalEmail], [MedicalContact1], [MedicalContact2], [Address], [Area], [City], [AmbulanceService], [AmbulanceContact], [Password], [IsEmailVerified], [ActivationCode], [Status], [AccountCreatedDate]) VALUES (7, N'Medical', N'Diagnostic Center', N'Popular Diagnostic Center', N'Morshedul Alam', N'Executive Manager', N'01715740301', N'popular@mail.com', N'01858828801', N'', N'20/B K.B. Fazlul Kader Road', N'Panchlaish', N'Chittagong', N'Not available', N'', N'73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=', 1, N'a6d2cd1e-f48c-40ae-8dbb-d0b253060e91', N'Active', N'Wednesday, October 10, 2018')
INSERT [dbo].[MedicalAccounts] ([MedicalId], [Role], [MedicalType], [MedicalName], [ContactPersonName], [ContactPersonPosition], [ContactPersonPhoneNo], [MedicalEmail], [MedicalContact1], [MedicalContact2], [Address], [Area], [City], [AmbulanceService], [AmbulanceContact], [Password], [IsEmailVerified], [ActivationCode], [Status], [AccountCreatedDate]) VALUES (8, N'Medical', N'Hospital & Diagnostic Center', N'Max Hospital & Diagnostic Ltd.', N'Rashedul Islam', N'Executive Manager', N'01715740302', N'maxhospitalltd@gmail.com', N'01858828802', N'', N' Late Alhaj Zahur Ahmed Chowdhury Rd', N'Mehedi Bag', N'Chittagong', N'Available', N'01713998199', N'73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=', 1, N'0fa5a1c0-d935-4c62-a450-dd137138d072', N'Active', N'Wednesday, October 10, 2018')
INSERT [dbo].[MedicalAccounts] ([MedicalId], [Role], [MedicalType], [MedicalName], [ContactPersonName], [ContactPersonPosition], [ContactPersonPhoneNo], [MedicalEmail], [MedicalContact1], [MedicalContact2], [Address], [Area], [City], [AmbulanceService], [AmbulanceContact], [Password], [IsEmailVerified], [ActivationCode], [Status], [AccountCreatedDate]) VALUES (9, N'Medical', N'General Hospital/Medical', N'Chattogram Metropolitan Hospital Limited', N'Karim Hasan', N'Manager', N'01715740303', N'cmhl@mail.com', N'01858828803', N'031651242', N'698/452 O.R. Nizam Road, GEC', N'O R Nizam', N'Chittagong', N'Available', N'01713998199', N'73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=', 1, N'665d7bed-8edd-45a0-82d3-6771d7a09d92', N'Active', N'Wednesday, October 10, 2018')
INSERT [dbo].[MedicalAccounts] ([MedicalId], [Role], [MedicalType], [MedicalName], [ContactPersonName], [ContactPersonPosition], [ContactPersonPhoneNo], [MedicalEmail], [MedicalContact1], [MedicalContact2], [Address], [Area], [City], [AmbulanceService], [AmbulanceContact], [Password], [IsEmailVerified], [ActivationCode], [Status], [AccountCreatedDate]) VALUES (10, N'Medical', N'Hospital & Diagnostic Center', N'Parkview Hospital Ltd', N'Nazmul Huda', N'It Admin', N'01715740305', N'parkview@gmail.com', N'01858828800', N'', N'Katalgonj', N'Panchlaish', N'Chittagong', N'Available', N'01713998100', N'73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=', 1, N'53bab40d-0ed6-47d2-aef9-53a893a353a4', N'Active', N'Sunday, December 9, 2018')
INSERT [dbo].[MedicalAccounts] ([MedicalId], [Role], [MedicalType], [MedicalName], [ContactPersonName], [ContactPersonPosition], [ContactPersonPhoneNo], [MedicalEmail], [MedicalContact1], [MedicalContact2], [Address], [Area], [City], [AmbulanceService], [AmbulanceContact], [Password], [IsEmailVerified], [ActivationCode], [Status], [AccountCreatedDate]) VALUES (1010, N'Medical', N'General Hospital/Medical', N'Imperial Hospital', N'Rashed Hossain', N'It Admin', N'01715740300', N'imperial@gmail.com', N'01999000004', N'', N'Foys Lake', N'Pahartali', N'Chittagong', N'Not available', N'', N'73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=', 1, N'a9c394f2-b424-4f38-b09c-4c9db1590462', N'Active', N'Monday, December 10, 2018')
INSERT [dbo].[MedicalAccounts] ([MedicalId], [Role], [MedicalType], [MedicalName], [ContactPersonName], [ContactPersonPosition], [ContactPersonPhoneNo], [MedicalEmail], [MedicalContact1], [MedicalContact2], [Address], [Area], [City], [AmbulanceService], [AmbulanceContact], [Password], [IsEmailVerified], [ActivationCode], [Status], [AccountCreatedDate]) VALUES (2010, N'Medical', N'Hospital & Diagnostic Center', N'CSCR (Pvt.) Limited', N'Abdul Hamid', N'It Admin', N'01715740402', N'cscr@mail.com', N'01744589950', N'031656565', N'1675/A', N'O R Nizam Road', N'Chittagong', N'Available', N'01876844882', N'73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=', 1, N'6d67fc83-cf51-4ec6-902e-d29fbe5fcd69', N'Active', N'Wednesday, December 12, 2018')
SET IDENTITY_INSERT [dbo].[MedicalAccounts] OFF
SET IDENTITY_INSERT [dbo].[MedicalConsultants] ON 

INSERT [dbo].[MedicalConsultants] ([Id], [ConsultantName], [MedicalId]) VALUES (23, N'Allergy & Immunology', 5)
INSERT [dbo].[MedicalConsultants] ([Id], [ConsultantName], [MedicalId]) VALUES (25, N'Cardiology (Heart, Cardiac Surgery, Cardiovascular, Hypertension, Blood Pressure)', 5)
INSERT [dbo].[MedicalConsultants] ([Id], [ConsultantName], [MedicalId]) VALUES (26, N'Dermatology (Skin, Venereology, Sexual, Hair, Allergy)', 5)
INSERT [dbo].[MedicalConsultants] ([Id], [ConsultantName], [MedicalId]) VALUES (27, N'General Physician (All or any common diseases and health issues)', 5)
INSERT [dbo].[MedicalConsultants] ([Id], [ConsultantName], [MedicalId]) VALUES (28, N'Medicine (All Diseases of Adults)', 2010)
INSERT [dbo].[MedicalConsultants] ([Id], [ConsultantName], [MedicalId]) VALUES (29, N'Cardiology (Heart, Cardiac Surgery, Cardiovascular, Hypertension, Blood Pressure)', 2010)
INSERT [dbo].[MedicalConsultants] ([Id], [ConsultantName], [MedicalId]) VALUES (30, N'Hepatology (Liver, Gallbladder, Biliary system)', 2010)
INSERT [dbo].[MedicalConsultants] ([Id], [ConsultantName], [MedicalId]) VALUES (31, N'General Surgery (Incision, Operation)', 2010)
INSERT [dbo].[MedicalConsultants] ([Id], [ConsultantName], [MedicalId]) VALUES (32, N'Dermatology (Skin, Venereology, Sexual, Hair, Allergy)', 2010)
INSERT [dbo].[MedicalConsultants] ([Id], [ConsultantName], [MedicalId]) VALUES (33, N'Gynaecology and Obstetrics (Pregnancy, Menstrual, Uterus, Female)', 2010)
INSERT [dbo].[MedicalConsultants] ([Id], [ConsultantName], [MedicalId]) VALUES (34, N'Endocrinology (Diabetes, Hormones, Thyroid, etc.)', 2010)
INSERT [dbo].[MedicalConsultants] ([Id], [ConsultantName], [MedicalId]) VALUES (35, N'Haematology (Blood related diseases)', 2010)
INSERT [dbo].[MedicalConsultants] ([Id], [ConsultantName], [MedicalId]) VALUES (36, N'Medicine (All Diseases of Adults)', 7)
INSERT [dbo].[MedicalConsultants] ([Id], [ConsultantName], [MedicalId]) VALUES (37, N'ENT (Ear, Nose & Throat, Otorhinolaryngology)', 7)
INSERT [dbo].[MedicalConsultants] ([Id], [ConsultantName], [MedicalId]) VALUES (38, N'Cardiology (Heart, Cardiac Surgery, Cardiovascular, Hypertension, Blood Pressure)', 7)
INSERT [dbo].[MedicalConsultants] ([Id], [ConsultantName], [MedicalId]) VALUES (39, N'Colorectal Surgery (Surgery of Anal Canal, Rectum, etc.)', 7)
INSERT [dbo].[MedicalConsultants] ([Id], [ConsultantName], [MedicalId]) VALUES (40, N'Gynaecology and Obstetrics (Pregnancy, Menstrual, Uterus, Female)', 7)
SET IDENTITY_INSERT [dbo].[MedicalConsultants] OFF
SET IDENTITY_INSERT [dbo].[MedicalEmergency] ON 

INSERT [dbo].[MedicalEmergency] ([Id], [TwentyFourService], [EmergencyService], [AmbulanceService], [SeatCapacity], [EmergencyPhoneNo], [EmergencyPhoneNo2], [AmbulancePhoneNo], [AmbulancePhoneNo2], [MedicalId]) VALUES (1014, N'Available', N'Available', N'Available', 90, N'01715740303', N'', N'01715740304', N'', 5)
INSERT [dbo].[MedicalEmergency] ([Id], [TwentyFourService], [EmergencyService], [AmbulanceService], [SeatCapacity], [EmergencyPhoneNo], [EmergencyPhoneNo2], [AmbulancePhoneNo], [AmbulancePhoneNo2], [MedicalId]) VALUES (1015, N'Available', N'Available', N'Available', 100, N'01744589950', N'', N'01755019576', N'', 2010)
INSERT [dbo].[MedicalEmergency] ([Id], [TwentyFourService], [EmergencyService], [AmbulanceService], [SeatCapacity], [EmergencyPhoneNo], [EmergencyPhoneNo2], [AmbulancePhoneNo], [AmbulancePhoneNo2], [MedicalId]) VALUES (1016, N'Available', N'Not Available', N'Available', 1, N'', N'', N'01715740402', N'', 7)
INSERT [dbo].[MedicalEmergency] ([Id], [TwentyFourService], [EmergencyService], [AmbulanceService], [SeatCapacity], [EmergencyPhoneNo], [EmergencyPhoneNo2], [AmbulancePhoneNo], [AmbulancePhoneNo2], [MedicalId]) VALUES (1017, N'Not Available', N'Available', N'Available', 120, N'01831558995', N'', N'', N'', 8)
SET IDENTITY_INSERT [dbo].[MedicalEmergency] OFF
SET IDENTITY_INSERT [dbo].[MedicalFacilities] ON 

INSERT [dbo].[MedicalFacilities] ([Id], [FacilityName], [MedicalId]) VALUES (1005, N'Washing Plant', 5)
INSERT [dbo].[MedicalFacilities] ([Id], [FacilityName], [MedicalId]) VALUES (1006, N'Water Plant', 5)
INSERT [dbo].[MedicalFacilities] ([Id], [FacilityName], [MedicalId]) VALUES (1007, N'Cabin Service', 2010)
INSERT [dbo].[MedicalFacilities] ([Id], [FacilityName], [MedicalId]) VALUES (1008, N'Patient Facilities', 2010)
SET IDENTITY_INSERT [dbo].[MedicalFacilities] OFF
SET IDENTITY_INSERT [dbo].[OtherMedicalServices] ON 

INSERT [dbo].[OtherMedicalServices] ([Id], [ServiceName], [MedicalId]) VALUES (1010, N'Two Generators', 5)
INSERT [dbo].[OtherMedicalServices] ([Id], [ServiceName], [MedicalId]) VALUES (1012, N'Medicine Store', 5)
SET IDENTITY_INSERT [dbo].[OtherMedicalServices] OFF
SET IDENTITY_INSERT [dbo].[TopicCategories] ON 

INSERT [dbo].[TopicCategories] ([TopicCategoryId], [TopicCategoryName], [CountCategory]) VALUES (1, N'Allergy', 3)
INSERT [dbo].[TopicCategories] ([TopicCategoryId], [TopicCategoryName], [CountCategory]) VALUES (2, N'
Blood and Blood Vessels', 1)
INSERT [dbo].[TopicCategories] ([TopicCategoryId], [TopicCategoryName], [CountCategory]) VALUES (3, N'Bones, joints and muscles', 0)
INSERT [dbo].[TopicCategories] ([TopicCategoryId], [TopicCategoryName], [CountCategory]) VALUES (1002, N'Eyes', 2)
INSERT [dbo].[TopicCategories] ([TopicCategoryId], [TopicCategoryName], [CountCategory]) VALUES (1003, N'
Skin and nails', 2)
INSERT [dbo].[TopicCategories] ([TopicCategoryId], [TopicCategoryName], [CountCategory]) VALUES (1004, N'
Teenage health', 0)
INSERT [dbo].[TopicCategories] ([TopicCategoryId], [TopicCategoryName], [CountCategory]) VALUES (1005, N'
Women''s health', 0)
SET IDENTITY_INSERT [dbo].[TopicCategories] OFF
SET IDENTITY_INSERT [dbo].[UserAccounts] ON 

INSERT [dbo].[UserAccounts] ([UserId], [Role], [UserName], [Email], [Password], [MobileNo], [Gender], [Age], [BloodGroup], [WantToGiveBlood], [Location]) VALUES (1006, N'User', N'Amimul Ihasan', N'ihasan@mail.com', N'i7DPbrmxfQ99IrRW8SElfcElTh8BZlNwR2OD6ndt9BQ=', N'01834224406', N'Male', N'', N'A+', 1, N'Muradpur')
INSERT [dbo].[UserAccounts] ([UserId], [Role], [UserName], [Email], [Password], [MobileNo], [Gender], [Age], [BloodGroup], [WantToGiveBlood], [Location]) VALUES (1007, N'User', N'Miraz Chowdhury', N'', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'01829621246', N'', N'', N'O+', 0, N'')
INSERT [dbo].[UserAccounts] ([UserId], [Role], [UserName], [Email], [Password], [MobileNo], [Gender], [Age], [BloodGroup], [WantToGiveBlood], [Location]) VALUES (1008, N'User', N'Imran Morshed', N'', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'01857818610', N'', N'', N'O+', 0, N'')
INSERT [dbo].[UserAccounts] ([UserId], [Role], [UserName], [Email], [Password], [MobileNo], [Gender], [Age], [BloodGroup], [WantToGiveBlood], [Location]) VALUES (2005, N'User', N'Moinul Hasan', N'', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'01829381633', N'', N'', N'B+', 1, N'Sitakund')
INSERT [dbo].[UserAccounts] ([UserId], [Role], [UserName], [Email], [Password], [MobileNo], [Gender], [Age], [BloodGroup], [WantToGiveBlood], [Location]) VALUES (2006, N'User', N'Belal Hossain', N'', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'01834224401', N'', N'', N'A+', 1, N'Kumira')
INSERT [dbo].[UserAccounts] ([UserId], [Role], [UserName], [Email], [Password], [MobileNo], [Gender], [Age], [BloodGroup], [WantToGiveBlood], [Location]) VALUES (2007, N'User', N'Samia Islam', N'samia@mail.com', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'01834224402', N'', N'', N'B+', 1, N'Chawkbazar')
INSERT [dbo].[UserAccounts] ([UserId], [Role], [UserName], [Email], [Password], [MobileNo], [Gender], [Age], [BloodGroup], [WantToGiveBlood], [Location]) VALUES (2008, N'User', N'Samia hoque', N'samiahq@mail.com', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'01715740303', N'', N'', N'O+', 0, N'')
INSERT [dbo].[UserAccounts] ([UserId], [Role], [UserName], [Email], [Password], [MobileNo], [Gender], [Age], [BloodGroup], [WantToGiveBlood], [Location]) VALUES (2009, N'User', N'Miraz Rahman', N'', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'01829621247', N'', N'', N'O+', 1, N'Sitakund')
INSERT [dbo].[UserAccounts] ([UserId], [Role], [UserName], [Email], [Password], [MobileNo], [Gender], [Age], [BloodGroup], [WantToGiveBlood], [Location]) VALUES (2010, N'User', N'Robiul Hossain', N'robiul@mail.com', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'01834224403', N'', N'', N'A+', 1, N'Bohoddarhat')
INSERT [dbo].[UserAccounts] ([UserId], [Role], [UserName], [Email], [Password], [MobileNo], [Gender], [Age], [BloodGroup], [WantToGiveBlood], [Location]) VALUES (3006, N'User', N'Sahnoor Meher', N'', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'01834224400', N'', N'', N'', 0, N'')
INSERT [dbo].[UserAccounts] ([UserId], [Role], [UserName], [Email], [Password], [MobileNo], [Gender], [Age], [BloodGroup], [WantToGiveBlood], [Location]) VALUES (4005, N'User', N'Helen Akter', N'', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'01938018677', N'', N'', N'B+', 1, N'Chittagong')
INSERT [dbo].[UserAccounts] ([UserId], [Role], [UserName], [Email], [Password], [MobileNo], [Gender], [Age], [BloodGroup], [WantToGiveBlood], [Location]) VALUES (4006, N'User', N'Sayedul Alam', N'', N'vvV+x/U6bUC+tkCngKY5yDvCmsipgW8fxsXG3Nk8RyE=', N'01834224422', N'', N'', N'', 0, N'')
INSERT [dbo].[UserAccounts] ([UserId], [Role], [UserName], [Email], [Password], [MobileNo], [Gender], [Age], [BloodGroup], [WantToGiveBlood], [Location]) VALUES (5005, N'User', N'Shohidul Alam', N'', N'LcAmn6VNJpqHU2gQ7EU8sJW0uS9F5jgmoh3/HC528Wk=', N'01309001316', N'Male', N'25', N'A+', 1, N'Agrabad')
INSERT [dbo].[UserAccounts] ([UserId], [Role], [UserName], [Email], [Password], [MobileNo], [Gender], [Age], [BloodGroup], [WantToGiveBlood], [Location]) VALUES (5006, N'User', N'Mosharraf Hossain', N'mosharraf@outlook.com', N'ONpHG4zD9t1BmkhQEi2Vudp2MAaN0vm6VHYVLrHMiqI=', N'01800000001', N'Male', N'25', N'A+', 1, N'Khulshi')
SET IDENTITY_INSERT [dbo].[UserAccounts] OFF
ALTER TABLE [dbo].[AssignedDoctors]  WITH CHECK ADD  CONSTRAINT [FK_AssignedDoctors_Doctors] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Doctors] ([Id])
GO
ALTER TABLE [dbo].[AssignedDoctors] CHECK CONSTRAINT [FK_AssignedDoctors_Doctors]
GO
ALTER TABLE [dbo].[AssignedDoctors]  WITH CHECK ADD  CONSTRAINT [FK_AssignedDoctors_MedicalAccounts] FOREIGN KEY([MedicalId])
REFERENCES [dbo].[MedicalAccounts] ([MedicalId])
GO
ALTER TABLE [dbo].[AssignedDoctors] CHECK CONSTRAINT [FK_AssignedDoctors_MedicalAccounts]
GO
ALTER TABLE [dbo].[DiagnosticServices]  WITH CHECK ADD  CONSTRAINT [FK_DiagnosticServices_DiagnosticServices] FOREIGN KEY([MedicalId])
REFERENCES [dbo].[MedicalAccounts] ([MedicalId])
GO
ALTER TABLE [dbo].[DiagnosticServices] CHECK CONSTRAINT [FK_DiagnosticServices_DiagnosticServices]
GO
ALTER TABLE [dbo].[DoctorsChambers]  WITH CHECK ADD  CONSTRAINT [FK_DoctorsChambers_Doctors] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Doctors] ([Id])
GO
ALTER TABLE [dbo].[DoctorsChambers] CHECK CONSTRAINT [FK_DoctorsChambers_Doctors]
GO
ALTER TABLE [dbo].[ForumComments]  WITH CHECK ADD  CONSTRAINT [FK_ForumComments_ForumPosts] FOREIGN KEY([ForumPostId])
REFERENCES [dbo].[ForumPosts] ([ForumPostId])
GO
ALTER TABLE [dbo].[ForumComments] CHECK CONSTRAINT [FK_ForumComments_ForumPosts]
GO
ALTER TABLE [dbo].[ForumPosts]  WITH NOCHECK ADD  CONSTRAINT [FK_ForumPosts_Doctors] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Doctors] ([Id])
GO
ALTER TABLE [dbo].[ForumPosts] NOCHECK CONSTRAINT [FK_ForumPosts_Doctors]
GO
ALTER TABLE [dbo].[ForumPosts]  WITH NOCHECK ADD  CONSTRAINT [FK_ForumPosts_MedicalAccounts] FOREIGN KEY([MedicalId])
REFERENCES [dbo].[MedicalAccounts] ([MedicalId])
GO
ALTER TABLE [dbo].[ForumPosts] NOCHECK CONSTRAINT [FK_ForumPosts_MedicalAccounts]
GO
ALTER TABLE [dbo].[ForumPosts]  WITH CHECK ADD  CONSTRAINT [FK_ForumPosts_TopicCategories] FOREIGN KEY([TopicCategoryId])
REFERENCES [dbo].[TopicCategories] ([TopicCategoryId])
GO
ALTER TABLE [dbo].[ForumPosts] CHECK CONSTRAINT [FK_ForumPosts_TopicCategories]
GO
ALTER TABLE [dbo].[ForumPosts]  WITH NOCHECK ADD  CONSTRAINT [FK_ForumPosts_UserAccounts] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserAccounts] ([UserId])
GO
ALTER TABLE [dbo].[ForumPosts] NOCHECK CONSTRAINT [FK_ForumPosts_UserAccounts]
GO
ALTER TABLE [dbo].[HospitalServices]  WITH CHECK ADD  CONSTRAINT [FK_HospitalServices_HospitalServices] FOREIGN KEY([MedicalId])
REFERENCES [dbo].[MedicalAccounts] ([MedicalId])
GO
ALTER TABLE [dbo].[HospitalServices] CHECK CONSTRAINT [FK_HospitalServices_HospitalServices]
GO
ALTER TABLE [dbo].[MedicalConsultants]  WITH CHECK ADD  CONSTRAINT [FK_MedicalConsultants_MedicalAccounts] FOREIGN KEY([MedicalId])
REFERENCES [dbo].[MedicalAccounts] ([MedicalId])
GO
ALTER TABLE [dbo].[MedicalConsultants] CHECK CONSTRAINT [FK_MedicalConsultants_MedicalAccounts]
GO
ALTER TABLE [dbo].[MedicalEmergency]  WITH CHECK ADD  CONSTRAINT [FK_MedicalServices_MedicalAccounts] FOREIGN KEY([MedicalId])
REFERENCES [dbo].[MedicalAccounts] ([MedicalId])
GO
ALTER TABLE [dbo].[MedicalEmergency] CHECK CONSTRAINT [FK_MedicalServices_MedicalAccounts]
GO
ALTER TABLE [dbo].[MedicalFacilities]  WITH CHECK ADD  CONSTRAINT [FK_MedicalFacilities_MedicalAccounts] FOREIGN KEY([MedicalId])
REFERENCES [dbo].[MedicalAccounts] ([MedicalId])
GO
ALTER TABLE [dbo].[MedicalFacilities] CHECK CONSTRAINT [FK_MedicalFacilities_MedicalAccounts]
GO
ALTER TABLE [dbo].[OtherMedicalServices]  WITH CHECK ADD  CONSTRAINT [FK_OtherMedicalServices_MedicalAccounts] FOREIGN KEY([MedicalId])
REFERENCES [dbo].[MedicalAccounts] ([MedicalId])
GO
ALTER TABLE [dbo].[OtherMedicalServices] CHECK CONSTRAINT [FK_OtherMedicalServices_MedicalAccounts]
GO