using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models
{

    public class Metas
    {
        public string Result { get; set; }
        public string Message { get; set; }
    }

    public class Errors
    {
        public string Message { get; set; }
        public string Code { get; set; }
    }
}
