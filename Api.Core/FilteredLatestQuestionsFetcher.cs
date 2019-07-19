using System;
using System.Collections.Generic;
using System.Text;
using Api.Contract;

namespace Api.Core {
    public class FilteredLatestQuestionsFetcher : IFilteredLatestQuestionsFetcher {
        private readonly IStackExchangeClient _stackExchangeClient;
        private readonly IQuestionFilter _questionFilter;

        public FilteredLatestQuestionsFetcher(IStackExchangeClient stackExchangeClient, IQuestionFilter questionFilter) {
            _stackExchangeClient = stackExchangeClient;
            _questionFilter = questionFilter;
        }

        public IEnumerable<QuestionDto> FetchQuestions() {
            var questionResponseDto = _stackExchangeClient.GetLatestQuestions(1);

            return _questionFilter.FilterQuestions(questionResponseDto.Items);
        }
    }
}