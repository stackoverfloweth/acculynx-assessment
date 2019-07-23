using System.Collections.Generic;
using Api.Contract;

namespace Api.Core {
    public interface IFilteredLatestQuestionsFetcher {
        IEnumerable<QuestionDto> FetchQuestions();
    }
}