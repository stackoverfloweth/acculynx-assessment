using Api.Contract;
using AutoMapper;
using Data.Entities;
using System.Collections.Generic;

namespace Api.Core {
    public class AttemptedQuestionDtoAssembler : IAttemptedQuestionDtoAssembler {
        private readonly IMapper _mapper;
        private readonly IQuestionFilter _questionFilter;

        public AttemptedQuestionDtoAssembler(IMapper mapper, IQuestionFilter questionFilter) {
            _mapper = mapper;
            _questionFilter = questionFilter;
        }

        public IEnumerable<AttemptedQuestionDto> AssembleAttemptedQuestions(IEnumerable<Attempt> attempts, IEnumerable<QuestionDto> questions) {
            var attemptedQuestionDtos = new List<AttemptedQuestionDto>();

            foreach (var attempt in attempts) {
                var attemptDto = _mapper.Map<AttemptDto>(attempt);
                var questionDto = _questionFilter.GetQuestionDtoById(questions, attempt.QuestionId);

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
