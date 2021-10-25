using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models
{
    public class Department
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NameLT { get; set; }
        public long? SubCategoryId { get; set; }
    }
    public class DepartmentResponse
    {
        [JsonProperty("model")]
        public List<Department> Departments { get; set; }
        public Metas Metas { get; set; }
        public Errors Errors { get; set; }
        public SubCategory SelectedSubCategory { get; set; }
    }
}
