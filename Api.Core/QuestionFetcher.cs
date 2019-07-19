using System;
using System.Collections.Generic;
using System.Text;
using Api.Contract;

namespace Api.Core {
    public class QuestionFetcher : IQuestionFetcher {
        private readonly IStackExchangeClient _stackExchangeClient;
        private readonly IQuestionFilter _questionFilter;

        public QuestionFetcher(IStackExchangeClient stackExchangeClient, IQuestionFilter questionFilter) {
            _stackExchangeClient = stackExchangeClient;
            _questionFilter = questionFilter;
        }

        public IEnumerable<QuestionDto> FetchQuestions() {
            var questionResponse = _stackExchangeClient.Questions(1);

            return _questionFilter.FilterQuestions(questionResponse.Items);
        }
    }
}