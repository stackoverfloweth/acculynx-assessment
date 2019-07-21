using Api.Contract;
using AutoMapper;
using Data.Entities;
using Data.Repositories;

namespace Api.Core {
    public class AttemptSubmissionManager : IAttemptSubmissionManager {
        private readonly IMapper _mapper;
        private readonly IAttemptRepository _attemptRepository;
        private readonly IAttemptScoreCalculator _attemptScoreCalculator;

        public AttemptSubmissionManager(IMapper mapper, IAttemptRepository attemptRepository, IAttemptScoreCalculator attemptScoreCalculator) {
            _mapper = mapper;
            _attemptRepository = attemptRepository;
            _attemptScoreCalculator = attemptScoreCalculator;
        }

        public AttemptDto SubmitAttempt(AttemptDto attemptDto, string userIpAddress) {
            var attempt = _mapper.Map<Attempt>(attemptDto);
            var score = _attemptScoreCalculator.CalculateScore(attempt);

            attempt.UserIpAddress = userIpAddress;
            attempt.Score = score;

            _attemptRepository.InsertAttempt(attempt);

            return _mapper.Map<AttemptDto>(attempt);
        }
    }
}
