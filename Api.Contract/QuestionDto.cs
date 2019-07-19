using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Api.Contract {
    public class QuestionDto {
        [JsonProperty("question_id")]
        public int QuestionId { get; set; }
        [JsonProperty("is_answered")]
        public bool IsAnswered { get; set; }
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
    }
}