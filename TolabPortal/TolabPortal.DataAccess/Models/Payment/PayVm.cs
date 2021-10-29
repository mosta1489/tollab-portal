namespace TolabPortal.DataAccess.Models.Payment
{
    public class PayVm
    {
        public decimal InvoiceAmount { get; set; }
        public string TransactionId { get; set; }
        public int TransactionType { get; set; }
        public string ReturnRoute { get; set; }
        public string CssClassName { get; set; }
    }
}