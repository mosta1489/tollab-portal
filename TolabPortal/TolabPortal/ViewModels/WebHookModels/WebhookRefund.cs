
using System;

namespace TolabPortal.ViewModels.WebHookModels
{
    public class WebhookRefund
    {
        public string RefundReference { get; set; }
        public DateTime CreatedDate { get; set; }
        public string RefundStatus { get; set; }
        public decimal Amount { get; set; }
        public string Comments { get; set; }
        public long InvoiceId { get; set; }
    }
}
