using System.ComponentModel.DataAnnotations;

namespace TolabPortal.DataAccess.Models.Payment
{
    public class DirectPaymentModel
    {

        [Required]
        [StringLength(10)]
        public string PaymentType { get; set; } //card  token authorize
        public bool SaveToken { get; set; }
        public int? IntervalDays { get; set; } = 0;
        public CardModel Card { get; set; }
        public string Token { get; set; } = "";
        public bool Bypass3DS { get; set; } = true;
    }
    public class DirectPaymentRequest : DirectPaymentModel
    {
        [Required]
        public string PaymentURL { get; set; }
    }
    public class CardModel
    {
        [StringLength(80)]
        public string Number { get; set; }

        [StringLength(2)]
        public string ExpiryMonth { get; set; }

        [StringLength(2)]
        public string ExpiryYear { get; set; }

        [StringLength(4)]
        public string SecurityCode { get; set; }
        [StringLength(50)]
        public string HolderName { get; set; }
    }
}