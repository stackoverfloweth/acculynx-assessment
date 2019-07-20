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
        public int Score { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        [JsonProperty("last_activity_date")]
        public DateTime LastActivityDate { get; set; }
        [JsonProperty("creation_date")]
        public DateTime CreationDate { get; set; }
        public string Body { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public OwnerDto Owner { get; set; }
    }
}