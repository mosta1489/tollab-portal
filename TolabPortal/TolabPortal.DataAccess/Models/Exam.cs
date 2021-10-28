using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models
{
    public class Exam
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public int OrderNumber { get; set; }
        public int Duration { get; set; }
        public long ExamTypeId { get; set; }
        public long CourseId { get; set; }
        public bool Publish { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
