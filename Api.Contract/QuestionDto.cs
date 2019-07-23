using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Api.Contract {
    public class QuestionDto {
        [JsonProperty("question_id")]
        public int QuestionId { get; set; }
        [JsonProperty("accepted_answer_id")]
        public int? AcceptedAnswerId { get; set; }
        [JsonProperty("view_count")]
        public int ViewCount { get; set; }
        [JsonProperty("answer_count")]
        public int AnswerCount { get; set; }
        [JsonProperty("score")]
        public int Score { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("last_activity_date")]
        public DateTime LastActivityDate { get; set; }
        [JsonProperty("creation_date")]
        public DateTime CreationDate { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("tags")]
        public IEnumerable<string> Tags { get; set; }
        [JsonProperty("owner")]
        public OwnerDto Owner { get; set; }
    }
}