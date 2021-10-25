using System.ComponentModel.DataAnnotations;

namespace TolabPortal.DataAccess.Models.Payment
{
    public class InitiatePaymentRequest
    {
        [Required]
        public decimal InvoiceAmount { get; set; }
        [Required]
        [StringLength(3)]
        public string CurrencyIso { get; set; }
    }
}
