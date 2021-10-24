using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NameLT { get; set; }
        public long? SectionId { get; set; }
        public List<SubCategory> SubCategories { get; set; }
    }

    public class CategoryResponse
    {
        [JsonProperty("model")]
        public IEnumerable<Category> Categories { get; set; }
        public Metas Metas { get; set; }
        public Errors Errors { get; set; }
    }
}
