using System.Collections.Generic;

namespace TolabPortal.DataAccess.Models.Payment
{
    public class InitiatePaymentResponse
    {
        public List<InitiatePaymentMethodsModel> PaymentMethods { get; set; }
    }
}
