using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tolab.Common;
using TolabPortal.DataAccess.Models;

namespace TolabPortal.ViewModels
{
    public class StudentProfileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneKey { get; set; }
        public string Phone { get; set; }
        public bool Gender { get; set; }
        public object Photo { get; set; }
        public string Bio { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationDateInArabic
        {
            get
            {
                return CommonUtilities.DateFromEnglishToArabic(CreationDate);
            }
        }
        public long CountryId { get; set; }
        public string CountryCode { get; set; }
        public int NumberOfCourses { get; set; }
        public string Password{ get; set; }
        public string RePassword { get; set; }
        public string IdentityId { get; set; }
        public List<InterestViewModel> Interests { get; set; }
        public List<Category> Categories { get; set; }
    }

    public class InterestViewModel
    {
        public long SectionId { get; set; }
        public string SectionName { get; set; }
        public string SectionNameLT { get; set; }
        public string SectionImage { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameLT { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryNameLT { get; set; }
        public long SubCategoryId { get; set; }
        public List<DepartmentViewModel> Departments { get; set; }
    }

    public class DepartmentViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NameLT { get; set; }
        public long? SubCategoryId { get; set; }
    }
}
