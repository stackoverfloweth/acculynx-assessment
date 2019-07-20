using Api.Contract;
using Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Api.Core {
    public class PreviouslyAttemptedQuestionFetcher : IPreviouslyAttemptedQuestionFetcher {
        private readonly IAttemptRepository _attemptRepository;
        private readonly IStackExchangeClient _stackExchangeClient;
        private readonly IAttemptedQuestionDtoAssembler _attemptedQuestionDtoAssembler;

        public PreviouslyAttemptedQuestionFetcher(IAttemptRepository attemptRepository, IStackExchangeClient stackExchangeClient, IAttemptedQuestionDtoAssembler attemptedQuestionDtoAssembler)
        {
            _attemptRepository = attemptRepository;
            _stackExchangeClient = stackExchangeClient;
            _attemptedQuestionDtoAssembler = attemptedQuestionDtoAssembler;
        }

        public IEnumerable<AttemptedQuestionDto> FetchQuestions(string userIpAddress)
        {
            var attempts = _attemptRepository.GetAttempts(userIpAddress).ToList();
            var attemptQuestionIds = attempts.Select(attempt => attempt.QuestionId).ToList();
            var questionResponseDto = _stackExchangeClient.GetQuestions(attemptQuestionIds);
            
            return _attemptedQuestionDtoAssembler.AssembleAttemptedQuestions(attempts, questionResponseDto.Items);
        }
    }
}
