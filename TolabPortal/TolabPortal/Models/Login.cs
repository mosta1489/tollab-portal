using System.ComponentModel.DataAnnotations;

namespace TolabPortal.Models
{
    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RememberMeString { get; set; }
        public bool RememberMe => RememberMeString == "on";
    }
}