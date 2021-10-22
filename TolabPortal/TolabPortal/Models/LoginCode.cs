using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TolabPortal.Models
{
    public class LoginCode
    {
        [Required]
        public string ActivationCode { get; set; }
    }
}
