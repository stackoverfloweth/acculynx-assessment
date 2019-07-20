using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Api.Contract {
    public class AnswerDto {
        [JsonProperty("answer_id")]
        public int AnswerId { get; set; }
        [JsonProperty("question_id")]
        public int QuestionId { get; set; }
        [JsonProperty("is_accepted")]
        public bool IsAccepted { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        [JsonProperty("last_activity_date")]
        public DateTime LastActivityDate { get; set; }
        [JsonProperty("creation_date")]
        public DateTime CreationDate { get; set; }
        public OwnerDto Owner { get; set; }
    }
}
