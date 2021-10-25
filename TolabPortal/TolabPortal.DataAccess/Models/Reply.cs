using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models
{
    public class Reply
    {
        public long Id { get; set; }
        public string Comment { get; set; }
        public long? VideoQuestionId { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool? ViewMyAccount { get; set; }
        public string Image { get; set; }
        public string Voice { get; set; }
        public long? StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentPhoto { get; set; }
        public long? TeacherId { get; set; }
        public long? TeacherAssistantId { get; set; }
        public string TeacherName { get; set; }
        public string TeacherPhoto { get; set; }
    }
}
