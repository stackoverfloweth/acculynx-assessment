using Api.Contract;
using System.Collections.Generic;
using System.Linq;

namespace Api.Core {
    public class QuestionFilter : IQuestionFilter {
        public IEnumerable<QuestionDto> FilterQuestions(IEnumerable<QuestionDto> questions) {
            return questions;
        }

        public QuestionDto GetQuestionDtoById(IEnumerable<QuestionDto> questions, int id) {
            return questions.SingleOrDefault(question => question.QuestionId == id);
        }
    }
}