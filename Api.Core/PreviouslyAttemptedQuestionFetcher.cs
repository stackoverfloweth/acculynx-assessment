using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Contract;
using Data.Repositories;

namespace Api.Core {
    public class PreviouslyAttemptedQuestionFetcher : IPreviouslyAttemptedQuestionFetcher {
        private readonly IAttemptRepository _attemptRepository;
        private readonly IStackExchangeClient _stackExchangeClient;

        public PreviouslyAttemptedQuestionFetcher(IAttemptRepository attemptRepository, IStackExchangeClient stackExchangeClient)
        {
            _attemptRepository = attemptRepository;
            _stackExchangeClient = stackExchangeClient;
        }

        public IEnumerable<QuestionDto> FetchQuestions(string ip)
        {
            var attempts = _attemptRepository.GetAttempts(ip);
            var attemptQuestionIds = attempts.Select(attempt => attempt.QuestionId).ToList();
            var questionResponseDto = _stackExchangeClient.GetQuestions(attemptQuestionIds);

            return questionResponseDto.Items;
        }
    }
}
