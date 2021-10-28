using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models
{
    public class StudentTransactionVM
    {
        public float? TotalBalance { get; set; }
        public IEnumerable<StudentTransaction> studentTransactions { get; set; }

    }

    public class StudentTransactionsResponse
    {
        [JsonProperty("model")]
        public StudentTransactionVM studentTransactionsVM { get; set; }
        public Metas Metas { get; set; }
        public Errors Errors { get; set; }
    }
}
