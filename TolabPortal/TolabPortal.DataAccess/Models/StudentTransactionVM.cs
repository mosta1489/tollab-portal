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
        public decimal? TotalBalance { get; set; }
        public IEnumerable<StudentTransaction> studentTransactions { get; set; }

    }

   public class model
    {
        public decimal? TotalBalance { get; set; }
        public IEnumerable<StudentTransaction> studentTransactions { get; set; }
    }

    public class StudentTransactionsResponse
    {
        public model Model { get; set; }
        public Metas Metas { get; set; }
        public Errors Errors { get; set; }
    }
}
