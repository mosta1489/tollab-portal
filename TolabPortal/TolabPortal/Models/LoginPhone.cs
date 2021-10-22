using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TolabPortal.Models
{
    public class LoginPhone
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public bool ConditionsAgree { get; set; }
    }
}
