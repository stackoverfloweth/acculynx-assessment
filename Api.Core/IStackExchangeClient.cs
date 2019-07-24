using Api.Contract;
using System.Collections.Generic;

namespace Api.Core {
    public interface IStackExchangeClient {
        IEnumerable<QuestionDto> GetLatestQuestions(int page);
        IEnumerable<QuestionDto> GetQuestions(List<int> questionIds);
        IEnumerable<AnswerDto> GetAnswers(int questionId);
        QuestionDto GetQuestion(int questionId);
    }
}