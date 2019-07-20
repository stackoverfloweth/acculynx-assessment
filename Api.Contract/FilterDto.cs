using System.Collections.Generic;
using Newtonsoft.Json;

namespace Api.Contract {
    public class FilterDto {
        [JsonProperty("filter_type")]
        public string FilterType { get; set; }
        [JsonProperty("included_fields")]
        public IEnumerable<string> IncludedFields { get; set; }
        public string Filter { get; set; }
    }
}
