using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models
{
    public class StudentLogoutResponse
    {
        [JsonProperty("model")]
        bool? LogoutResult { get; set; }
        public Errors Errors { get; set; }
        public Section SelectedSection { get; set; }
    }
}
