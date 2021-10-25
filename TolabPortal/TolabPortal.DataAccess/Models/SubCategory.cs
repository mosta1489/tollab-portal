using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models
{
    public class SubCategory
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NameLT { get; set; }
        public long? CategoryId { get; set; }
        public List<Department> Departments { get; set; }
    }

    public class SubCategoryResponse
    {
        [JsonProperty("model")]
        public List<SubCategory> SubCategories { get; set; }
        public Metas Metas { get; set; }
        public Errors Errors { get; set; }
        public Category SelectedCategory { get; set; }
    }
}
