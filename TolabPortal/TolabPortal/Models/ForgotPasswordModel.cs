namespace TolabPortal.Models
{
    public class ForgotPasswordModel
    {
        public string IdentityId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneKey { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public int? ActivationCode { get; set; }

        public ForgotPasswordModel() { }

        public ForgotPasswordModel(string id, string email, string phoneNumber, string phoneKey, int? activationCode=null)
        {
            IdentityId = id;
            Email = email;
            PhoneNumber = phoneNumber;
            PhoneKey = phoneKey;
            activationCode = ActivationCode;
         }
    }
}