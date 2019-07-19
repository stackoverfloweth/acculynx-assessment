using System;
using System.Collections.Generic;
using System.Text;
using Api.Contract;

namespace Api.Core {
    public class QuestionFetcher : IQuestionFetcher {
        private readonly IQuestionFilter _questionFilter;

        public QuestionFetcher(IQuestionFilter questionFilter) {
            _questionFilter = questionFilter;
        }

        public IEnumerable<QuestionDto> FetchQuestions() {
            var questions = new List<QuestionDto>();
            
            return _questionFilter.FilterQuestions(questions);
        }
    }
}
