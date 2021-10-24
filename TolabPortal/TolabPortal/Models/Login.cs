using System.ComponentModel.DataAnnotations;

namespace TolabPortal.Models
{
    public class Login
    {
        public string PhoneKey { get; set; }
        public string PhoneNumber { get; set; }
        public bool ConditionsAgree { get; set; }
    }
}