using Api.Contract;
using System.Collections.Generic;

namespace Api.Core {
    public class AttemptedQuestionDtoAssembler : IAttemptedQuestionDtoAssembler {
        private readonly IQuestionFilter _questionFilter;

        public AttemptedQuestionDtoAssembler(IQuestionFilter questionFilter) {
            _questionFilter = questionFilter;
        }

        public IEnumerable<AttemptedQuestionDto> AssembleAttemptedQuestions(IEnumerable<AttemptDto> attemptDtos, IEnumerable<QuestionDto> questions) {
            var attemptedQuestionDtos = new List<AttemptedQuestionDto>();

            foreach (var attemptDto in attemptDtos) {
                var questionDto = _questionFilter.GetQuestionDtoById(questions, attemptDto.QuestionId);

                if (attemptDto != null && questionDto != null) {
                    attemptedQuestionDtos.Add(new AttemptedQuestionDto {
                        AttemptDto = attemptDto,
                        QuestionDto = questionDto
                    });
                }
            }

            return attemptedQuestionDtos;
        }
    }
}
