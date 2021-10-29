using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TolabPortal.DataAccess.Models.Exams;

namespace TolabPortal.DataAccess.Models
{
    public class Course
    {
        public long Id { get; set; }
        public string CourseTrack { get; set; }
        public string Name { get; set; }
        public string NameLT { get; set; }
        public long? TrackId { get; set; }
        public decimal? CurrentCost { get; set; }
        public decimal? PreviousCost { get; set; }
        public string SKUNumber { get; set; }
        public decimal? SKUPrice { get; set; }
        public decimal? OldSKUPrice { get; set; }

        public DateTime? CreationDate { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public long? SubscriptionCount { get; set; }
        public string Image { get; set; }
        public string IntroVideo { get; set; }
        public string AlbumUri { get; set; }

        public bool? BySubscription { get; set; }
        public bool? ShowWaterMark { get; set; }
        public bool? NeedParent { get; set; }

        public bool? TrackShowWaterMark { get; set; }

        public int? OrderNumber { get; set; }

        public long? CourseStatusId { get; set; }


        public string TeacherName { get; set; }

        public long TeacherId { get; set; }

        public long? VideoCount { get; set; }

        public long? FilesCount { get; set; }

        public double? HoursCount { get; set; }

        public int IsFavourite { get; set; }

        public int Enrollment { get; set; }

        public int ContentCount { get; set; }

        public int ViewedContent { get; set; }

        public int CountStudentEnrolled { get; set; }

        public decimal GainedMoney { get; set; }

        public List<Group> Groups { get; set; }

        public IEnumerable<CourseTags> CourseTags { get; set; }
        public string IntroVideoUri { get; set; }
        public string CourseCode { get; set; }

        public IEnumerable<VideoQuestion> VideoQuestions { get; set; }

        public Content Content { get; set; }
        public IEnumerable<StudentExamsToCorrect> StudentExams { get; set; }
        public bool IsCurrentStudentSubscribedToCourse { get; set; }
        public ItemDetails ItemDetails { get; set; }
    }

    public class CourseResponse
    {
        [JsonProperty("model")]
        public Course Course { get; set; }
        public Metas Metas { get; set; }
        public Errors Errors { get; set; }
    }
}
