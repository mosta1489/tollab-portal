using Newtonsoft.Json;
using System.Collections.Generic;

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
        public List<Category> Categories { get; set; }

        public Metas Metas { get; set; }
        public Errors Errors { get; set; }
        public Section SelectedSection { get; set; }
    }

    public class EditInterestModel
    {
        public List<Section> Sections { get; set; }
        public int SelectedCategoryId { get; set; }
        public long SelectedSubCategoryId { get; set; }
        public long SelectedDepartmentId { get; set; }
    }
}