using Api.Contract;
using Api.Core;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using Data.Repositories;

namespace Api.Controllers {
    [RoutePrefix("question")]
    public class QuestionController : BaseApiController {
        private readonly IFilteredLatestQuestionsFetcher _filteredLatestQuestionsFetcher;
        private readonly IPreviouslyAttemptedQuestionFetcher _previouslyAttemptedQuestionFetcher;
        private readonly IStackExchangeClient _stackExchangeClient;
        private readonly IAttemptRepository _attemptRepository;
        private readonly IMapper _mapper;

        public QuestionController(IFilteredLatestQuestionsFetcher filteredLatestQuestionsFetcher, IPreviouslyAttemptedQuestionFetcher previouslyAttemptedQuestionFetcher, IStackExchangeClient stackExchangeClient, IAttemptRepository attemptRepository, IMapper mapper) {
            _filteredLatestQuestionsFetcher = filteredLatestQuestionsFetcher;
            _previouslyAttemptedQuestionFetcher = previouslyAttemptedQuestionFetcher;
            _stackExchangeClient = stackExchangeClient;
            _attemptRepository = attemptRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("latest/{page?}")]
        public IEnumerable<QuestionDto> FetchLatestQuestions() {
            var latestQuestions = _filteredLatestQuestionsFetcher.FetchQuestions();

            return latestQuestions;
        }

        [HttpGet]
        [Route("previous")]
        public IEnumerable<AttemptedQuestionDto> FetchPreviousQuestions() {
            var previousQuestions = _previouslyAttemptedQuestionFetcher.FetchQuestions(UserId);

            return previousQuestions;
        }

        [HttpGet]
        [Route("{questionId}/answers")]
        public IEnumerable<AnswerDto> FetchAnswersForQuestion(int questionId) {
            return _stackExchangeClient.GetAnswers(questionId).Items;
        }

        [HttpGet]
        [Route("{questionId}/attempts")]
        public IEnumerable<AttemptDto> GetAttemptsForQuestion(int questionId) {
            var attemptsForQuestion = _attemptRepository.GetAttemptsForQuestion(questionId);

            return _mapper.Map<IEnumerable<AttemptDto>>(attemptsForQuestion);
        }
    }
}
