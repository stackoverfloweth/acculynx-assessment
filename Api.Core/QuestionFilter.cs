using Api.Contract;
using System.Collections.Generic;

namespace Api.Core {
    public class QuestionFilter : IQuestionFilter {
        public IEnumerable<QuestionDto> FilterQuestions(IEnumerable<QuestionDto> questions) {
            return questions;
        }
    }
}