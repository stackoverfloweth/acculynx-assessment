using Api.Contract;
using System.Collections.Generic;

namespace Api.Core {
    public interface IFilteredLatestQuestionsFetcher {
        IEnumerable<QuestionDto> FetchQuestions();
    }
}