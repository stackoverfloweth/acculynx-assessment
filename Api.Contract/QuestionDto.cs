using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Contract {
    public class QuestionDto {
        public bool QuestionId { get; set; }
        public bool IsAnswered { get; set; }
        public int ViewCount { get; set; }
        public int AnswerCount { get; set; }
        public int Score { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public DateTime LastActivityDate { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
