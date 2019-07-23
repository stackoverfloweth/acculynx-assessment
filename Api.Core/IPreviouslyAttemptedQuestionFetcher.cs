using System.Collections.Generic;
using Api.Contract;

namespace Api.Core {
    public interface IPreviouslyAttemptedQuestionFetcher {
        AttemptedQuestionDto FetchAttemptedQuestion(string userId, int questionId);
        IEnumerable<AttemptedQuestionDto> FetchAttemptedQuestions(string userId);
    }
}