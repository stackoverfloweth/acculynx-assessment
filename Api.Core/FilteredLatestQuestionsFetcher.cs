using Api.Contract;
using Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Api.Core {
    public class FilteredLatestQuestionsFetcher : IFilteredLatestQuestionsFetcher {
        private readonly IStackExchangeClient _stackExchangeClient;
        private readonly IQuestionFilter _questionFilter;
        private readonly IAttemptRepository _attemptRepository;
        private static readonly int Expected = 20;

        public FilteredLatestQuestionsFetcher(IStackExchangeClient stackExchangeClient, IQuestionFilter questionFilter, IAttemptRepository attemptRepository) {
            _stackExchangeClient = stackExchangeClient;
            _questionFilter = questionFilter;
            _attemptRepository = attemptRepository;
        }

        public IEnumerable<QuestionDto> FetchQuestions(string userId) {
            var attempts = _attemptRepository.GetAttempts(userId).Select(attempt => attempt.QuestionId).ToList();
            return FetchQuestionsRecursively(new List<QuestionDto>(), attempts, 1);
        }

        private List<QuestionDto> FetchQuestionsRecursively(List<QuestionDto> questionDtos, List<int> questionIdsToIgnore, int page) {
            var latestQuestionDtos = _stackExchangeClient.GetLatestQuestions(page);
            var filteredQuestionDtos = _questionFilter.FilterQuestions(latestQuestionDtos).Where(question => !questionIdsToIgnore.Contains(question.QuestionId));

            questionDtos.AddRange(filteredQuestionDtos);

            if (questionDtos.Count >= Expected) {
                return questionDtos.Take(Expected).ToList();
            }

            return FetchQuestionsRecursively(questionDtos, questionIdsToIgnore, page + 1);
        }
    }
}