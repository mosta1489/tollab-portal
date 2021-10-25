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
        public string CallBackUrl { get; set; }
        public string ErrorUrl { get; set; }
        public string CustomerName { get; set; }
        
        public string DisplayCurrencyIso { get; set; }
        
        public string MobileCountryCode { get; set; }
         
        
        public string CustomerMobile { get; set; }
        
        public string CustomerEmail { get; set; }
        public decimal InvoiceValue { get; set; }

        public List<InitiatePaymentMethodsModel> PaymentMethods { get; set; }
    }
}
