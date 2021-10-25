using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models
{
    public class Content
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NameLT { get; set; }
        public string Path { get; set; }
        public long? GroupId { get; set; }
        public double? Duration { get; set; }
        public long? ContentTypeId { get; set; }
        public bool? IsFree { get; set; }
        public string VideoUri { get; set; }
        public string YoutubeLink { get; set; }
        public int? OrderNumber { get; set; }

        public int IsViewed { get; set; }

        public StudentContent StudentContent { get; set; }
        public string VideoLink { get; set; }
    }
}
