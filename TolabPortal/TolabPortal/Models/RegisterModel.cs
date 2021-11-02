namespace TolabPortal.Models
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneKey { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string Gender { get; set; }
        public string Bio { get; set; }
        public string UserPoliciesAgreedString { get; set; }
        public bool UserPoliciesAgreed => UserPoliciesAgreedString == "on";
    }
}