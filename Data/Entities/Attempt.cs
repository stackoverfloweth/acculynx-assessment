using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities {
    public class Attempt {
        [Key]
        public int AttemptId { get; set; }
        public string UserIpAddress { get; set; }
        public int QuestionId { get; set; }
        public DateTime AttemptDate { get; set; }
        public int AnswerId { get; set; }
        public int AttemptAnswerId { get; set; }
    }
}
