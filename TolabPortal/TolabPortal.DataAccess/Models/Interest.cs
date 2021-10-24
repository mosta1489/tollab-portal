using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models
{
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
}
