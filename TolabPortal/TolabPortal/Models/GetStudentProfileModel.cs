using System;
using System.Collections.Generic;

namespace TolabPortal.Models
{
    public class GetStudentProfileModel
    {
        public class Category
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string NameLT { get; set; }
            public int SectionId { get; set; }
            public object SubCategories { get; set; }
        }

        public class Section
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string NameLT { get; set; }
            public string Image { get; set; }
            public int CountryId { get; set; }
            public List<Category> Categories { get; set; }
        }

        public class Model
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public object ParentName { get; set; }
            public object ParentName2 { get; set; }
            public object ParentPhone { get; set; }
            public object ParentPhone2 { get; set; }
            public string Email { get; set; }
            public string PhoneKey { get; set; }
            public string Phone { get; set; }
            public bool Gender { get; set; }
            public object Photo { get; set; }
            public string Bio { get; set; }
            public DateTime CreationDate { get; set; }
            public string IdentityId { get; set; }
            public int CountryId { get; set; }
            public bool Enabled { get; set; }
            public DateTime ExpirationVCodeDate { get; set; }
            public bool Verified { get; set; }
            public object PaymentLink { get; set; }
            public object PaymentKey { get; set; }
            public object LastSendDate { get; set; }
            public object ScreenShootCount { get; set; }
            public object LastTakenScreenshootDate { get; set; }
            public int NumberMaxLoginCount { get; set; }
            public int NumberCurrentLoginCount { get; set; }
            public object Token { get; set; }
            public int NumberOfCourses { get; set; }
            public List<Interest> Interests { get; set; }
            public List<Section> Sections { get; set; }
            public string Currency { get; set; }
            public string CurrencyLT { get; set; }
            public string CountryCode { get; set; }
            public string UserType { get; set; }
        }

        public class Metas
        {
            public string result { get; set; }
            public string message { get; set; }
        }

        public class Errors
        {
        }

        public class Interest
        {
            public long SectionId { get; set; }
            public string SectionName { get; set; }
            public string SectionNameLT { get; set; }
            public string SectionImage { get; set; }
            public string CategoryName { get; set; }
            public string CategoryNameLT { get; set; }
            public string SubCategoryName { get; set; }
            public string SubCategoryNameLT { get; set; }

            //public IEnumerable<SubjectCourse> SubjectCourses { get; set; }
            public long SubCategoryId { get; set; }
        }

        public Model model { get; set; }
        public Metas metas { get; set; }
        public Errors errors { get; set; }
    }
}