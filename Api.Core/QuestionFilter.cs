using System;
using System.Collections.Generic;
using System.Text;
using Api.Contract;

namespace Api.Core {
    public class QuestionFilter : IQuestionFilter {
        public IEnumerable<QuestionDto> FilterQuestions(IEnumerable<QuestionDto> questions) {
            return questions;
        }
    }
}