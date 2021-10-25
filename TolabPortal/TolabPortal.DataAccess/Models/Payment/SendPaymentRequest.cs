using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TolabPortal.DataAccess.Models.Payment
{
    public class SendPaymentRequest
    {
        public SendPaymentRequest()
        {
           
        }
        [Required]
        [StringLength(250)]
        public string CustomerName { get; set; }

        [Required]
        public string NotificationOption { get; set; }  // SMS EML ALL

        public string MobileCountryCode { get; set; }
        [StringLength(11)]
        [RegularExpression(@"^(?:(\+)|(00)|(\\*)|())[0-9]{3,14}((\\#)|())$", ErrorMessage = "Invalid mobile number")]
        public string CustomerMobile { get; set; }

        [StringLength(254)]
        [EmailAddress]
        public string CustomerEmail { get; set; }
        [Required]
        public decimal InvoiceValue { get; set; }

        public string DisplayCurrencyIso { get; set; }


        [StringLength(254)]
        [Url]
        public string CallBackUrl { get; set; }

        [StringLength(254)]
        [Url]
        public string ErrorUrl { get; set; }

        public string Language { get; set; } = "en";

        [StringLength(50)]
        public string CustomerReference { get; set; }

        [StringLength(20)]
        public string CustomerCivilId { get; set; }
        public string UserDefinedField { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? SupplierCode { get; set; }
        public decimal? SupplierValue { get; set; }
        public string SourceInfo { get; set; }
     
    }
}