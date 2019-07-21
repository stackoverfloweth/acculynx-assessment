using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Repositories {
    public class AttemptRepository : IAttemptRepository {
        private readonly IStackOverflowethContext _dbContext;
        public AttemptRepository(IStackOverflowethContext dbContext) {
            _dbContext = dbContext;
        }

        public IEnumerable<Attempt> GetAttempts(string userId) {
            if (string.IsNullOrEmpty(userId)) {
                return new List<Attempt>();
            }

            return _dbContext.Attempts
                .Where(attempt => attempt.UserId == userId)
                .ToList();
        }

        public void InsertAttempt(Attempt attempt) {
            if (attempt == null) {
                return;
            }

            _dbContext.Attempts.Add(attempt);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Attempt> GetAttemptsForQuestion(int questionId) {
            return _dbContext.Attempts
                .Where(attempt=> attempt.QuestionId == questionId)
                .ToList();
        }
    }
}
