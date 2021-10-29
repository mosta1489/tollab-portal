using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models.Exams
{
    public class StudentExamQuestionsWithAnswers
    {
        public StudentExam StudentExam { get; set; }
        public List<ExamQuestion> ExamQuestions { get; set; }
    }
}
