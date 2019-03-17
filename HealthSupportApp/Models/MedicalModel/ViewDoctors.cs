using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HealthSupportApp.Models.DoctorModel;

namespace HealthSupportApp.Models.MedicalModel
{
    public class ViewDoctors
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Title { get; set; }
        public string Speciality { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string BmdcReg { get; set; }
        public string OtherSpecification { get; set; }
        public string MedicalName { get; set; }
        public string MedicalAddress { get; set; }
        public string MedicalArea{ get; set; }
        public string MedicalCity { get; set; }
        public int Fee { get; set; }
        public int ReturningFee { get; set; }
        public int MedicalId { get; set; }
        public bool Sat { get; set; }
        public bool Sun { get; set; }
        public bool Mon { get; set; }
        public bool Tue { get; set; }
        public bool Wed { get; set; }
        public bool Thu { get; set; }
        public bool Fri { get; set; }
        public string SatTime { get; set; }
        public string SunTime { get; set; }
        public string MonTime { get; set; }
        public string TueTime { get; set; }
        public string WedTime { get; set; }
        public string ThuTime { get; set; }
        public string FriTime { get; set; }
        public ManageAppointment ManageAppointment { get; set; }
    }
}