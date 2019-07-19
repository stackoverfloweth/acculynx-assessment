using System.Collections.Generic;
using Api.Contract;

namespace Api.Core
{
    public interface IPreviouslyAttemptedQuestionFetcher
    {
        IEnumerable<QuestionDto> FetchQuestions(string ip);
    }
}