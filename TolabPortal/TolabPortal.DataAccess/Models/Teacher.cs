using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models
{
    public class Teacher
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Bio { get; set; }
        public bool? Gender { get; set; }
        public string Address { get; set; }
        public long? CountryId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? RegisterationDate { get; set; }
        public string FaceBookLink { get; set; }
        public string TwitterLink { get; set; }
        public string Instagram { get; set; }
        public int? TakenPercentage { get; set; }
        public int? LiveTakenPercentage { get; set; }
        public bool? Enabled { get; set; }
        public string IdentityId { get; set; }


        public long StudentCount { get; internal set; }

        public long StudentSubscriptionsCount { get; set; }


        public int CourseCount { get; set; }


        public string Currency { get; internal set; }

        public string CurrencyLT { get; set; }

        public IEnumerable<Country> Countries { get; set; }


        public IEnumerable<TrackWithCourses> TrackWithCourses { get; set; }

        public JArray Token { get; set; }

        public bool IsTeacher { get; set; }

        public TeacherAssistant TeacherAssistant { get; set; }
    }

    public class TeacherResponse
    {
        [JsonProperty("model")]
        public Teacher Teacher { get; set; }
        public Metas Metas { get; set; }
        public Errors Errors { get; set; }
    }
}
