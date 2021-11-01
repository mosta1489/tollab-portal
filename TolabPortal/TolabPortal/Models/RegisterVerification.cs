namespace TolabPortal.Models
{
    public class RegisterVerification
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string VerificationCode { get; set; }
        public string Password { get; set; }
        public bool UserPoliciesAgreed { get; set; }

        public RegisterVerification(string userName, string email, string phoneNumber, string password, bool userPoliciesAgreed)
        {
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            UserPoliciesAgreed = userPoliciesAgreed;
        }
    }
}