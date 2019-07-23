using Api.Contract;
using Api.Core;
using AutoMapper;
using Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Api.Controllers {
    [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
    [RoutePrefix("question")]
    public class QuestionController : BaseApiController {
        private readonly IFilteredLatestQuestionsFetcher _filteredLatestQuestionsFetcher;
        private readonly IStackExchangeClient _stackExchangeClient;
        private readonly IAttemptRepository _attemptRepository;
        private readonly IMapper _mapper;

        public QuestionController(IFilteredLatestQuestionsFetcher filteredLatestQuestionsFetcher, IStackExchangeClient stackExchangeClient, IAttemptRepository attemptRepository, IMapper mapper) {
            _filteredLatestQuestionsFetcher = filteredLatestQuestionsFetcher;
            _stackExchangeClient = stackExchangeClient;
            _attemptRepository = attemptRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("latest")]
        public IEnumerable<QuestionDto> FetchLatestQuestions() {
            var latestQuestions = _filteredLatestQuestionsFetcher.FetchQuestions();

            return latestQuestions;
        }

        [HttpGet]
        [Route("{questionId}")]
        public QuestionDto FetchQuestion(int questionId) {
            return _stackExchangeClient.GetQuestion(questionId);
        }

        [HttpGet]
        [Route("{questionId}/answers")]
        public IEnumerable<AnswerDto> FetchAnswersForQuestion(int questionId) {
            var answerDtos = _stackExchangeClient.GetAnswers(questionId).ToList();
            foreach (var answerDto in answerDtos) {
                answerDto.AttemptCount = _attemptRepository.GetAttemptsForAnswer(answerDto.AnswerId).Count();
            }

            return answerDtos;
        }

        [HttpGet]
        [Route("{questionId}/attempts")]
        public IEnumerable<AttemptDto> GetAttemptsForQuestion(int questionId) {
            var attemptsForQuestion = _attemptRepository.GetAttemptsForQuestion(questionId);

            return _mapper.Map<IEnumerable<AttemptDto>>(attemptsForQuestion);
        }
    }
}
