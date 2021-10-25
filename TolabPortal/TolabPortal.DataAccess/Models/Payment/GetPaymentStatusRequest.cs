using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TolabPortal.DataAccess.Models.Payment
{
    public class GetPaymentStatusRequest
    {
        [Required]
        public string Key { get; set; }
        [Required]
        public string KeyType { get; set; } // InvoiceId  PaymentId
    }
    public class GetPaymentStatusResponse
    {
        
        public long InvoiceId { get; set; }
        public string InvoiceStatus { get; set; }
        public string InvoiceReference { get; set; }
        public string CustomerReference { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ExpiryDate { get; set; }
        public decimal InvoiceValue { get; set; }
        public string Comments { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public string UserDefinedField { get; set; }
        public string InvoiceDisplayValue { get; set; }

    }
    
}
