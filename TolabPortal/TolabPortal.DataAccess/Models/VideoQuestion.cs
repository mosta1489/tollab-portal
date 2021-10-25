using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models
{
    public class VideoQuestion
    {
        public long Id { get; set; }
        public string Question { get; set; }
        public float? Minute { get; set; }
        public string Image { get; set; }
        public string Voice { get; set; }

        public bool? ViewMyAccount { get; set; }
        public long? ContentId { get; set; }
        public long? LiveId { get; set; }
        public DateTime? CreationDate { get; set; }
        public long? StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentPhoto { get; set; }
        public string ContentName { get; set; }
        public string ContentNameLT { get; set; }
        public IEnumerable<Reply> Replies { get; set; }
    }
}
