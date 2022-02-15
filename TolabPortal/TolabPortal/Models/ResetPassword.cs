using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TolabPortal.Models
{
    public class ResetPassword
    {
        public string PhoneKey { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public string ActivationCode { get; set; }

    }
}
