using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models.Exams
{
    public class StudentExam
    {
        public long Id { get; set; }
        public long? StudentId { get; set; }
        public long? ExamId { get; set; }
        public long? SolveStatusId { get; set; }
        public int? TotalScore { get; set; }
        public DateTime? CreationDate { get; set; }
        public long? TeacherAssistantId { get; set; }
        public int? Duration { get; set; }
    }
}
