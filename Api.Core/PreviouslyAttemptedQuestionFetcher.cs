using Api.Contract;
using Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Api.Core {
    public class PreviouslyAttemptedQuestionFetcher : IPreviouslyAttemptedQuestionFetcher {
        private readonly IAttemptRepository _attemptRepository;
        private readonly IStackExchangeClient _stackExchangeClient;
        private readonly IAttemptedQuestionDtoAssembler _attemptedQuestionDtoAssembler;
        private readonly IMapper _mapper;

        public PreviouslyAttemptedQuestionFetcher(IAttemptRepository attemptRepository, IStackExchangeClient stackExchangeClient, IAttemptedQuestionDtoAssembler attemptedQuestionDtoAssembler, IMapper mapper) {
            _attemptRepository = attemptRepository;
            _stackExchangeClient = stackExchangeClient;
            _attemptedQuestionDtoAssembler = attemptedQuestionDtoAssembler;
            _mapper = mapper;
        }

        public AttemptedQuestionDto FetchAttemptedQuestion(string userId, int questionId) {
            var attempt = _attemptRepository.GetAttempt(questionId, userId);
            var attemptDto = _mapper.Map<AttemptDto>(attempt);
            var questionDto = _stackExchangeClient.GetQuestion(questionId);

            return new AttemptedQuestionDto {
                AttemptDto = attemptDto,
                QuestionDto = questionDto
            };
        }

        public IEnumerable<AttemptedQuestionDto> FetchAttemptedQuestions(string userId) {
            var attempts = _attemptRepository.GetAttempts(userId).ToList();
            var attemptDtos = _mapper.Map<IEnumerable<AttemptDto>>(attempts);
            var attemptQuestionIds = attempts.Select(attempt => attempt.QuestionId).ToList();
            var questionDtos = _stackExchangeClient.GetQuestions(attemptQuestionIds);

            return _attemptedQuestionDtoAssembler.AssembleAttemptedQuestions(attemptDtos, questionDtos);
        }
    }
}
