using Api.Contract;
using Api.Core;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Api.Controllers {
    [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
    [RoutePrefix("attempt")]
    public class AttemptController : BaseApiController {
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