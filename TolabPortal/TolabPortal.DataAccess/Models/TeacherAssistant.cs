using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models
{
    public class TeacherAssistant
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public long? TeacherId { get; set; }
        public bool? Enabled { get; set; }
        public string IdentityId { get; set; }
        public DateTime RegisterationDate { get; set; }
        public JArray Token { get; set; }
        public bool IsTeacher { get; set; }
    }
}
