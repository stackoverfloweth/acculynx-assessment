using Api.Contract;
using Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Api.Controllers {
    [RoutePrefix("attempt")]
    public class AttemptController : BaseApiController {
        private readonly IAttemptSubmissionManager _attemptSubmissionManager;
        private readonly IPreviouslyAttemptedQuestionFetcher _previouslyAttemptedQuestionFetcher;

        public AttemptController(IAttemptSubmissionManager attemptSubmissionManager, IPreviouslyAttemptedQuestionFetcher previouslyAttemptedQuestionFetcher) {
            _attemptSubmissionManager = attemptSubmissionManager;
            _previouslyAttemptedQuestionFetcher = previouslyAttemptedQuestionFetcher;
        }

        [HttpGet]
        [Route("previous")]
        public IEnumerable<AttemptedQuestionDto> FetchPreviousQuestions() {
            var previousQuestions = _previouslyAttemptedQuestionFetcher.FetchAttemptedQuestions(UserId);

            return previousQuestions;
        }

        [HttpGet]
        [Route("{questionId}")]
        public AttemptedQuestionDto FetchAttempt(int questionId) {
            return _previouslyAttemptedQuestionFetcher.FetchAttemptedQuestion(UserId, questionId);
        }

        [HttpPost]
        [Route("")]
        public AttemptDto CreateAttempt(AttemptDto attemptDto) {
            return _attemptSubmissionManager.SubmitAttempt(attemptDto, UserId);
        }
    }
}