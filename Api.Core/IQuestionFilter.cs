using System.Collections.Generic;
using Api.Contract;

namespace Api.Core
{
    public interface IQuestionFilter
    {
        IEnumerable<QuestionDto> FilterQuestions(IEnumerable<QuestionDto> questions);
    }
}