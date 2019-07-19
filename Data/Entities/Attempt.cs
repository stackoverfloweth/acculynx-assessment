using System.ComponentModel.DataAnnotations;

namespace Data.Entities {
    public class Attempt {
        [Key]
        public int AttemptId { get; set; }
        public int QuestionId { get; set; }
    }
}
