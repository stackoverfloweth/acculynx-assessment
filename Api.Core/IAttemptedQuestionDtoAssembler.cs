using System.Collections.Generic;
using Api.Contract;
using Data.Entities;

namespace Api.Core {
    public interface IAttemptedQuestionDtoAssembler {
        IEnumerable<AttemptedQuestionDto> AssembleAttemptedQuestions(IEnumerable<Attempt> attempts, IEnumerable<QuestionDto> questions);
    }
}