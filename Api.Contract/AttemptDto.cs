using System;

namespace Api.Contract {
    public class AttemptDto {
        public int QuestionId { get; set; }
        public DateTime AttemptDate { get; set; }
        public bool AnsweredCorrectly { get; set; }
    }
}