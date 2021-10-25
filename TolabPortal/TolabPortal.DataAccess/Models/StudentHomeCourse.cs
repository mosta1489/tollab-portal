using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models
{
    public class StudentHomeCourse
    {
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameLT { get; set; }
        public long SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryNameLT { get; set; }
        public long TrackId { get; set; }

        public IEnumerable<SubjectCourse> SubjectCourses { get; set; }
    }

    public class StudentHomeCourseResponse
    {
        [JsonProperty("model")]
        public List<StudentHomeCourse> StudentHomeCourses { get; set; }
        public Metas Metas { get; set; }
        public Errors Errors { get; set; }
    }
}
