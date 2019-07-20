using Api.Contract;
using System.Collections.Generic;
using System.Linq;

namespace Api.Core {
    public class FilteredLatestQuestionsFetcher : IFilteredLatestQuestionsFetcher {
        private readonly IStackExchangeClient _stackExchangeClient;
        private readonly IQuestionFilter _questionFilter;
        private static readonly int Expected = 20;

        public FilteredLatestQuestionsFetcher(IStackExchangeClient stackExchangeClient, IQuestionFilter questionFilter) {
            _stackExchangeClient = stackExchangeClient;
            _questionFilter = questionFilter;
        }

        public IEnumerable<QuestionDto> FetchQuestions() {
            return FetchQuestionsRecursively(new List<QuestionDto>(), 1);
        }

        private List<QuestionDto> FetchQuestionsRecursively(List<QuestionDto> questionDtos, int page) {
            var questionResponseDto = _stackExchangeClient.GetLatestQuestions(page);
            var filteredQuestionDtos = _questionFilter.FilterQuestions(questionResponseDto.Items);

            questionDtos.AddRange(filteredQuestionDtos);

            if (questionDtos.Count >= Expected) {
                return questionDtos.Take(Expected).ToList();
            }

            return FetchQuestionsRecursively(questionDtos, page + 1);
        }
    }
}