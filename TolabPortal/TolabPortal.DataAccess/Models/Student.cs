using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TolabPortal.DataAccess.Models
{
    public class Student
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }
        public string ParentName2 { get; set; }
        public string ParentPhone { get; set; }
        public string ParentPhone2 { get; set; }
        public string Email { get; set; }
        public string PhoneKey { get; set; }
        public string Phone { get; set; }
        public bool? Gender { get; set; }
        public string Photo { get; set; }
        public string Bio { get; set; }
        public DateTime? CreationDate { get; set; }
        public string IdentityId { get; set; }
        public long CountryId { get; set; }
        public bool? Enabled { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public int Vcode { get; set; }

        public DateTime? ExpirationVCodeDate { get; set; }
        public bool Verified { get; set; }
        public string PaymentLink { get; set; }
        public string PaymentKey { get; set; }
        public DateTime? LastSendDate { get; set; }
        public int? ScreenShootCount { get; set; }
        public DateTime? LastTakenScreenshootDate { get; set; }
        public int? NumberMaxLoginCount { get; set; }
        public int? NumberCurrentLoginCount { get; set; }
        public JArray Token { get; set; }
        public int NumberOfCourses { get; set; }
        public IEnumerable<Interest> Interests { get; set; }
        public IEnumerable<Section> Sections { get; set; }
        public string Currency { get; internal set; }
        public string CurrencyLT { get; set; }
        public string CountryCode { get; set; }
        public string UserType { get; set; }
        public string Password { get; set; }

        public Student(string phoneKey, string phone, string name, string email, bool? gender, string bio, int countryId, string password)
        {
            this.PhoneKey = phoneKey;
            this.Phone = phone;
            this.Name = name;
            this.Email = email;
            this.Gender = gender;
            this.Bio = bio;
            CountryId = countryId;
            Password = password;
        }
        public Student() { }
    }

    public class StudentResponse
    {
        [JsonProperty("model")]
        public Student Student { get; set; }
        public Metas Metas { get; set; }
        public Errors Errors { get; set; }
    }

    public class GenericStudentResponse
    {
        [JsonProperty("model")]
        public object Student { get; set; }
        public Metas Metas { get; set; }
        public Errors Errors { get; set; }
    }
}