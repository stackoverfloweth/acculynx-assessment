using System;
using Newtonsoft.Json;

namespace Api.Contract {
    public class AnswerDto {
        [JsonProperty("answer_id")]
        public int AnswerId { get; set; }
        [JsonProperty("question_id")]
        public int QuestionId { get; set; }
        [JsonProperty("is_accepted")]
        public bool IsAccepted { get; set; }
        [JsonProperty("score")]
        public int Score { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("last_activity_date")]
        public DateTime LastActivityDate { get; set; }
        [JsonProperty("creation_date")]
        public DateTime CreationDate { get; set; }
        [JsonProperty("owner")]
        public OwnerDto Owner { get; set; }
    }
}
