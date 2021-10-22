using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Login.Models
{
    public class Section
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NameLT { get; set; }
        public string Image { get; set; }
        public long? CountryId { get; set; }
       
        //public IEnumerable<Category> Categories { get; set; }
    }
}
