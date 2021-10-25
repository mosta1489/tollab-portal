namespace TolabPortal.DataAccess.Models.Payment
{
    public class DirectPaymentResponse
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string PaymentId { get; set; }
        public string Token { get; set; }
        public string PaymentURL { get; set; }
        public CardInfoModel CardInfo { get; set; }

    }
}