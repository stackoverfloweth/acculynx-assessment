using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Api.Contract {
    public class OwnerDto {
        public int Reputation { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("user_type")]
        public string UserType { get; set; }
        [JsonProperty("profile_image")]
        public string ProfileImage { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
    }
}
