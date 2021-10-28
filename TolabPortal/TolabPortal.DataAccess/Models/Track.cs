namespace TolabPortal.DataAccess.Models
{
    public class Track
    {
        public long Id { get; set; }
        public string TrackSubject { get; set; }
        public string Name { get; set; }
        public string NameLT { get; set; }
        public string Image { get; set; }
        public long? TeacherId { get; set; }
        public long? SubjectId { get; set; }
        public int? SubscriptionDuration { get; set; }
        public decimal? SubscriptionCurrentPrice { get; set; }
        public decimal? SubscriptionOldPrice { get; set; }
        public string SKUNumber { get; set; }
        public decimal? SKUPrice { get; set; }
        public decimal? OldSKUPrice { get; set; }

        public int? OrderNumber { get; set; }

        public bool? BySubscription { get; set; }
        public bool? ShowWaterMark { get; set; }
        public string TrackCode { get; set; }
        public int CountCourse { get; set; }
        public decimal TotalCurrentCost { get; set; }
        public decimal TotalOldCost { get; set; }
        public string TeacherName { get; set; }
        public string TeatcherPhoto { get; set; }

        public bool IsSubscriped { get; set; }
    }
}