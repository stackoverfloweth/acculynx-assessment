﻿using Api.Contract;
using Api.Core;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;

namespace Api.Controllers {
    [RoutePrefix("question")]
    public class QuestionController : ApiController {
        private readonly IFilteredLatestQuestionsFetcher _filteredLatestQuestionsFetcher;
        private readonly IPreviouslyAttemptedQuestionFetcher _previouslyAttemptedQuestionFetcher;
        private readonly IStackExchangeClient _stackExchangeClient;

        public QuestionController(IFilteredLatestQuestionsFetcher filteredLatestQuestionsFetcher, IPreviouslyAttemptedQuestionFetcher previouslyAttemptedQuestionFetcher, IStackExchangeClient stackExchangeClient) {
            _filteredLatestQuestionsFetcher = filteredLatestQuestionsFetcher;
            _previouslyAttemptedQuestionFetcher = previouslyAttemptedQuestionFetcher;
            _stackExchangeClient = stackExchangeClient;
        }

        [HttpGet]
        [Route("latest")]
        public IEnumerable<QuestionDto> FetchLatestQuestions() {
            var latestQuestions = _filteredLatestQuestionsFetcher.FetchQuestions();

            return latestQuestions;
        }

        [HttpGet]
        [Route("previous")]
        public IEnumerable<AttemptedQuestionDto> FetchPreviousQuestions() {
            var userIpAddress = HttpContext.Current.Request.UserHostAddress;
            var previousQuestions = _previouslyAttemptedQuestionFetcher.FetchQuestions(userIpAddress);

            return previousQuestions;
        }

        [HttpGet]
        [Route("{id}/answers")]
        public IEnumerable<AnswerDto> FetchAnswersForQuestion(int id) {
            return _stackExchangeClient.GetAnswers(id).Items;
        }
    }
}
