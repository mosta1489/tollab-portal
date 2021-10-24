using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models
{
    public class Department
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NameLT { get; set; }
        public long? SubCategoryId { get; set; }
    }
}
