using System;

namespace TolabPortal.ViewModels.WebHookModels
{
    public class WebhookDeposit
    {
        public string DepositReference { get; set; }
        public decimal DepositedAmount { get; set; }
        public int NumberOfTransactions { get; set; }
    }
}
