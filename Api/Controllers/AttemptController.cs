using Api.Contract;
using Api.Core;
using Data.Repositories;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Api.Controllers {
    [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
    [RoutePrefix("attempt")]
    public class AttemptController : BaseApiController {
        private readonly IAttemptSubmissionManager _attemptSubmissionManager;
        private readonly IAttemptRepository _attemptRepository;
        private readonly IPreviouslyAttemptedQuestionFetcher _previouslyAttemptedQuestionFetcher;

        public AttemptController(IAttemptSubmissionManager attemptSubmissionManager, IAttemptRepository attemptRepository, IPreviouslyAttemptedQuestionFetcher previouslyAttemptedQuestionFetcher) {
            _attemptSubmissionManager = attemptSubmissionManager;
            _attemptRepository = attemptRepository;
            _previouslyAttemptedQuestionFetcher = previouslyAttemptedQuestionFetcher;
        }

        [HttpPost]
        [Route("")]
        public AttemptDto CreateAttempt(AttemptDto attemptDto) {
            return _attemptSubmissionManager.SubmitAttempt(attemptDto, UserId);
        }

        [HttpPost]
        [Route("{questionId}")]
        public AttemptDto FetchAttempt(int questionId) {
            var attempt = _attemptRepository.GetAttempt(questionId, UserId);

            return null;
            //return _previouslyAttemptedQuestionFetcher.FetchAttemptQuestion(attempt);
        }
    }
}