using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TolabPortal.Models
{
    public class RegisterInfo
    {
        public string PhoneKey { get; set; }
        public string PhoneNumber { get; set; }
        public string ConditionsAgree { get; set; }


        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
    }
}
