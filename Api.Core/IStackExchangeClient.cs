using System.Collections.Generic;
using Api.Contract;

namespace Api.Core {
    public interface IStackExchangeClient {
        QuestionResponseDto GetLatestQuestions(int page);
        QuestionResponseDto GetQuestions(List<int> ids);
    }
}
