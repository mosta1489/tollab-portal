using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models
{
    public class ExamAnswer
    {
        public long Id { get; set; }
        public long? ExamQuestionId { get; set; }
        public string Answer { get; set; }
        public bool? IsTrue { get; set; }
    }
}
