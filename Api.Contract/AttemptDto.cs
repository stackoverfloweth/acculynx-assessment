using System;

namespace Api.Contract {
    public class AttemptDto {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int AcceptedAnswerId { get; set; }
        public DateTime? AttemptDate { get; set; }
        public bool AnsweredCorrectly => AnswerId == AcceptedAnswerId;
        public int? Score { get; set; }
    }
}