using System.Collections.Generic;
using Api.Contract;

namespace Api.Core {
    public interface IStackExchangeClient {
        ItemResponseDto<QuestionDto> GetLatestQuestions(int page);
        ItemResponseDto<QuestionDto> GetQuestions(List<int> ids);
        ItemResponseDto<AnswerDto> GetAnswers(int id);
        QuestionDto GetQuestion(int questionId);
    }
}
