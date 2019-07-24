using Newtonsoft.Json;

namespace Api.Contract {
    public class AttemptedQuestionDto {
        [JsonProperty("question")]
        public QuestionDto QuestionDto { get; set; }
        [JsonProperty("attempt")]
        public AttemptDto AttemptDto { get; set; }
    }
}
