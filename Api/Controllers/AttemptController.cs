using System.Web;
using Api.Contract;
using Api.Core;
using System.Web.Http;

namespace Api.Controllers {
    [RoutePrefix("attempt")]
    public class AttemptController :  BaseApiController {
        private readonly IAttemptSubmissionManager _attemptSubmissionManager;

        public AttemptController(IAttemptSubmissionManager attemptSubmissionManager) {
            _attemptSubmissionManager = attemptSubmissionManager;
        }

        [HttpPost]
        [Route("")]
        public AttemptDto CreateAttempt(AttemptDto attemptDto) {
            return _attemptSubmissionManager.SubmitAttempt(attemptDto, UserId);
        }
    }
}