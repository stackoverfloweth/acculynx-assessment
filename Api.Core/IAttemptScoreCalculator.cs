using Data.Entities;

namespace Api.Core {
    public interface IAttemptScoreCalculator {
        int CalculateScore(Attempt attempt);
    }
}