using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TolabPortal.Models
{
    public class RegisterVerification
    {
        public string PhoneKey { get; set; }
        public string PhoneNumber { get; set; }
        public string ConditionsAgree { get; set; }


        public string Name { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }

        public string VerificationCode { get; set; }
    }
}
