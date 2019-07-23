using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Data.Repositories;

namespace Api.Core {
    public class AttemptScoreCalculator : IAttemptScoreCalculator {
        private readonly IAttemptRepository _attemptRepository;

        public AttemptScoreCalculator(IAttemptRepository attemptRepository) {
            _attemptRepository = attemptRepository;
        }

        public int CalculateScore(Attempt attempt) {
            if (attempt.AnswerId == attempt.AcceptedAnswerId) {
                return 100;
            }

            var attemptsOnQuestion = _attemptRepository.GetAttemptsForQuestion(attempt.QuestionId).ToList();
            if (!attemptsOnQuestion.Any()) {
                return 0;
            }

            var attemptCountWithSameAnswer = attemptsOnQuestion.Count(similar => similar.AnswerId == attempt.AnswerId);
            var score = (double) attemptCountWithSameAnswer / attemptsOnQuestion.Count() * 100;

            return (int)Math.Round(score);
        }
    }
}
