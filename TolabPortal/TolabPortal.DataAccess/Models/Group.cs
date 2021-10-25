using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models
{
    public class Group
    {
        public long Id { get; set; }
        public string GroupCourse { get; set; }
        public string Name { get; set; }
        public string NameLT { get; set; }
        public long? CourseId { get; set; }
        public List<Content> Contents { get; set; }
        public int OrderNumber { get; set; }
    }
}
