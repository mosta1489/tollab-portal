using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TolabPortal.Models
{
    public class RegisterSchoolLevel
    {
        [Required]
        public string Level { get; set; }
        public string School { get; set; }
        public string Interest { get; set; }
    }
}
