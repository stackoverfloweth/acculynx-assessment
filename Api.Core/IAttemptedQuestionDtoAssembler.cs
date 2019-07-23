using System.Collections.Generic;
using Api.Contract;
using Data.Entities;

namespace Api.Core {
    public interface IAttemptedQuestionDtoAssembler {
        IEnumerable<AttemptedQuestionDto> AssembleAttemptedQuestions(IEnumerable<AttemptDto> attemptDtos, IEnumerable<QuestionDto> questions);
    }
}