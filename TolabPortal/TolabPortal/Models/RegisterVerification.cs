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
        public string PhoneKey { get; set; }
        public string Gender { get; set; }
        public string Bio { get; set; }

        public RegisterVerification() { }

        public RegisterVerification(RegisterModel registerModel)
        {
            UserName = registerModel.UserName;
            Email = registerModel.Email;
            PhoneNumber = registerModel.PhoneNumber;
            Password = registerModel.Password;
            UserPoliciesAgreed = registerModel.UserPoliciesAgreed;
            PhoneKey = registerModel.PhoneKey;
            Gender = registerModel.Gender;
            Bio = registerModel.Bio;
        }

        public RegisterVerification(string userName, string email, string phoneNumber, string password, bool userPoliciesAgreed, string phoneKey, string gender, string bio)
        {
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            UserPoliciesAgreed = userPoliciesAgreed;
            PhoneKey = phoneKey;
            Gender = gender;
            Bio = bio;
        }
    }
}