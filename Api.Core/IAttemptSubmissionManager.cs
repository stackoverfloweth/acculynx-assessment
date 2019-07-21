using Api.Contract;

namespace Api.Core {
    public interface IAttemptSubmissionManager {
        AttemptDto SubmitAttempt(AttemptDto attemptDto, string userIpAddress);
    }
}