using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models
{
    public class Subject
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NameLT { get; set; }
        public string Image { get; set; }
        public long? DepartmentId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public long?  TeacherCount { get; set; }
        public List<Track> Tracks { get; set; }
    }

    public class SubjectResponse
    {
        [JsonProperty("model")]
        public List<Subject> Subjects { get; set; }
        public Metas Metas { get; set; }
        public Errors Errors { get; set; }
    }
}