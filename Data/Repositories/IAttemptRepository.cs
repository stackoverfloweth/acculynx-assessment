using System.Collections.Generic;
using Data.Entities;

namespace Data.Repositories {
    public interface IAttemptRepository {
        IEnumerable<Attempt> GetAttempts(string userId);
        void InsertAttempt(Attempt attempt);
        IEnumerable<Attempt> GetAttemptsForQuestion(int attempt);
        Attempt GetAttempt(int questionId, string userId);
        IEnumerable<Attempt> GetAttemptsForAnswer(int answerId);
    }
}