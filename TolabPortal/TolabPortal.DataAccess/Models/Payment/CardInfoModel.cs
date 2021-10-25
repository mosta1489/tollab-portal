namespace TolabPortal.DataAccess.Models.Payment
{
    public class CardInfoModel
    {
        public string Number { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string Brand { get; set; }
        public string Issuer { get; set; }
    }
}