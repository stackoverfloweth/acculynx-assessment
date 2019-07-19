using System.Collections.Generic;
using Api.Contract;

namespace Api.Core
{
    public interface IQuestionFetcher
    {
        IEnumerable<QuestionDto> FetchQuestions();
    }
}