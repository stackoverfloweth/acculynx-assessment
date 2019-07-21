using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities {
    public class Attempt {
        [Key]
        public int AttemptId { get; set; }
        public string UserId { get; set; }
        public int QuestionId { get; set; }
        public DateTime AttemptDate { get; set; }
        public int AnswerId { get; set; }
        public int AcceptedAnswerId { get; set; }
        public int Score { get; set; }
    }
}
