namespace TolabPortal.DataAccess.Models.Payment
{
    public class SendPaymentResponse
    {
        public int InvoiceId { get; set; }
        public string InvoiceURL { get; set; }
        public string CustomerReference { get; set; }
        public string UserDefinedField { get; set; }
    }
}