using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Repositories {
    public class AttemptRepository : IAttemptRepository
    {
        private readonly IStackOverflowethContext _stackOverflowethContext;

        public AttemptRepository(IStackOverflowethContext stackOverflowethContext)
        {
            _stackOverflowethContext = stackOverflowethContext;
        }

        public IEnumerable<Attempt> GetAttempts()
        {
            var attempt = new Attempt();
            attempt.QuestionId = 14;
            _stackOverflowethContext.Attempts.Add(attempt);
            _stackOverflowethContext.SaveChanges();
            return _stackOverflowethContext.Attempts.ToList();
        }
    }
}
