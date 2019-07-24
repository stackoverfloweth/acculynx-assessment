using Newtonsoft.Json;
using System.Collections.Generic;

namespace Api.Contract {
    public class FilterDto {
        [JsonProperty("filter_type")]
        public string FilterType { get; set; }
        [JsonProperty("included_fields")]
        public IEnumerable<string> IncludedFields { get; set; }
        [JsonProperty("filter")]
        public string Filter { get; set; }
    }
}
