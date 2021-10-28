using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models
{
    public class LiveAttachment
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public long LiveId { get; set; }
        public int? OrderNumber { get; set; }
    }
}
