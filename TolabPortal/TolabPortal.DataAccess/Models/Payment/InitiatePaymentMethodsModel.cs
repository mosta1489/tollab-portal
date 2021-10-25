namespace TolabPortal.DataAccess.Models.Payment
{
    public class InitiatePaymentMethodsModel
    {
        public int PaymentMethodId { get; set; }
        public string PaymentMethodAr { get; set; }
        public string PaymentMethodEn { get; set; }
        public string PaymentMethodCode { get; set; }
        public bool IsDirectPayment { get; set; }
        public decimal ServiceCharge { get; set; } = 0;
        public decimal TotalAmount { get; set; } = 0;
        public string CurrencyIso { get; set; }
        public string ImageUrl { get; set; }
    }
}
