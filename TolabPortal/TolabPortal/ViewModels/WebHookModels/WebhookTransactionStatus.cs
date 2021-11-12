 
using System;

namespace TolabPortal.ViewModels.WebHookModels
{
    public class WebhookTransactionStatus
    {
        public long InvoiceId { get; set; }
        public string InvoiceReference { get; set; }
        public string CreatedDate { get; set; }
        public string CustomerReference { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public string TransactionStatus { get; set; }
        public string PaymentMethod { get; set; }
        public string UserDefinedField { get; set; }
        public string ReferenceId { get; set; }
        public string TrackId { get; set; }
        public string PaymentId { get; set; }
        public string AuthorizationId { get; set; }
        //Invoice Amount
        public decimal InvoiceValueInBaseCurrency { get; set; }
        public string BaseCurrency { get; set; }
        public decimal InvoiceValueInDisplayCurreny { get; set; }
        public string DisplayCurrency { get; set; }
        public decimal InvoiceValueInPayCurrency { get; set; }
        public string PayCurrency { get; set; }

    }
}
