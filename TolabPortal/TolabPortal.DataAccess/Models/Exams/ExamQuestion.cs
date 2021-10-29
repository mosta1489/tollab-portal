using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models.Exams
{
    public class ExamQuestion
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int? OrderNumber { get; set; }
        public long? ExamQuestionTypeId { get; set; }
        public long? ExamId { get; set; }
        public float? Degree { get; set; }
        public string FilePath { get; set; }
        public string IdealAnswerFilePath { get; set; }
        public List<ExamAnswer> ExamAnswers { get; set; }
    }
}
