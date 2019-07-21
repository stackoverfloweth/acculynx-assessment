using System;
using Newtonsoft.Json;

namespace Api.Contract {
    public class AttemptDto {
        [JsonProperty("question_id")]
        public int QuestionId { get; set; }
        [JsonProperty("answer_id")]
        public int AnswerId { get; set; }
        [JsonProperty("accepted_answer_id")]
        public int AcceptedAnswerId { get; set; }
        [JsonProperty("attempt_date")]
        public DateTime? AttemptDate { get; set; }
        [JsonProperty("answered_correctly")]
        public bool AnsweredCorrectly => AnswerId == AcceptedAnswerId;
        [JsonProperty("score")]
        public int? Score { get; set; }
    }
}