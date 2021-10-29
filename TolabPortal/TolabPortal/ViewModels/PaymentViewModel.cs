using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TolabPortal.DataAccess.Models.Payment;

namespace TolabPortal.ViewModels
{





    public class PaymentViewModel
    {
        public int? PaymentMethodId { get; set; } = 0;
        public string ReturnUrl { get; set; }
        public string CustomerName { get; set; }
        
        public string DisplayCurrencyIso { get; set; }
        
        public decimal InvoiceValue { get; set; }
        public string CustomerReference  { get; set; }
        public int TransactionType { get; set; }
         


        public List<InitiatePaymentMethodsModel> PaymentMethods { get; set; }
    }

    public enum TransactionType
    {
        Track=1,
        Course,
        Live
    }
}
