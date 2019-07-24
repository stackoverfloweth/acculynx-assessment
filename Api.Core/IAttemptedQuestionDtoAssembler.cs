using Api.Contract;
using System.Collections.Generic;

namespace Api.Core {
    public interface IAttemptedQuestionDtoAssembler {
        IEnumerable<AttemptedQuestionDto> AssembleAttemptedQuestions(IEnumerable<AttemptDto> attemptDtos, IEnumerable<QuestionDto> questions);
    }
}