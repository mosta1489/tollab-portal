using System.Collections.Generic;

namespace TolabPortal.Models
{
    public class ApiErrorModel
    {
        public class Metas
        {
        }

        public class Errors
        {
            public string message { get; set; }
            public int Code { get; set; }
        }

        public List<object> model { get; set; }
        public Metas metas { get; set; }
        public Errors errors { get; set; }
    }
}