using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models.Payment
{
    public class ExecutePaymentResponse
    {
        public long InvoiceId { get; set; }
        public bool IsDirectPayment { get; set; }
        public string PaymentURL { get; set; }
        public string CustomerReference { get; set; }
        public string UserDefinedField { get; set; }
        public string RecurringId { get; set; }

    }

}
