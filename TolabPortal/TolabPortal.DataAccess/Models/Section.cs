using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolabPortal.DataAccess.Models
{
    public class Section
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NameLT { get; set; }
        public string Image { get; set; }
        public long? CountryId { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }

    public class SectionResponse
    {
        [JsonProperty("model")]
        public IEnumerable<Section> Sections { get; set; }
        public Metas Metas { get; set; }
        public Errors Errors { get; set; }
    }
}
