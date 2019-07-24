using Api.Contract;
using System.Collections.Generic;

namespace Api.Core {
    public interface IPreviouslyAttemptedQuestionFetcher {
        AttemptedQuestionDto FetchAttemptedQuestion(string userId, int questionId);
        IEnumerable<AttemptedQuestionDto> FetchAttemptedQuestions(string userId);
    }
}