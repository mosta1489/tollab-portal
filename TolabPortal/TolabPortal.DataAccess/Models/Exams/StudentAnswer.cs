using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models.Exams
{
    public class StudentAnswer
    {
        public long Id { get; set; }
        public long? StudentExamId { get; set; }
        public long? ExamQuestionId { get; set; }
        public long? ExamQuestionTypeId { get; set; }

        public ExamQuestion ExamQuestion { get; set; }
        public long? ExamAnswerId { get; set; }
        public ExamAnswer ExamAnswer { get; set; }
        public float? Degree { get; set; }
        public string AnswerText { get; set; }
        public string TeacherCorrectance { get; set; }
        public string VoicePath { get; set; }
        public long? Duration { get; set; }
        public bool? IsTrue { get; set; }
        public DateTime? CreationDate { get; set; }
        public string PdfAnswerPath { get; set; }
        public bool? Corrected { get; set; }
    }
}
