using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models
{
    public class StudentLiveHome
    {
        public long? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameLT { get; set; }
        public long? SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryNameLT { get; set; }
        public IEnumerable<LiveDTO> Lives { get; set; }
    }

    public class StudentLiveHomeResponse
    {
        [JsonProperty("model")]
        public List<StudentLiveHome> StudentLives { get; set; }
        public Metas Metas { get; set; }
        public Errors Errors { get; set; }
    }

    public class PaginatedStudentLiveHome
    {
        public List<StudentLiveHome> StudentLives { get; set; }
        public int PageIndex { get; set; }
    }
}
