using Newtonsoft.Json;
using System.Collections.Generic;

namespace Api.Contract {
    public class ItemResponseDto<T> {
        [JsonProperty("items")]
        public IEnumerable<T> Items { get; set; }
        [JsonProperty("has_more")]
        public bool HasMore { get; set; }
        [JsonProperty("quota_max")]
        public int QuotaMax { get; set; }
        [JsonProperty("quota_remaining")]
        public int QuotaRemaining { get; set; }
    }
}
