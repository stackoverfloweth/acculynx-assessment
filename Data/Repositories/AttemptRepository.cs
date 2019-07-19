using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Repositories {
    public class AttemptRepository : IAttemptRepository {
        private readonly IStackOverflowethContext _stackOverflowethContext;

        public AttemptRepository(IStackOverflowethContext stackOverflowethContext) {
            _stackOverflowethContext = stackOverflowethContext;
        }

        public IEnumerable<Attempt> GetAttempts(string ip) {
            if (string.IsNullOrEmpty(ip)) {
                return new List<Attempt>();
            }

            return _stackOverflowethContext.Attempts
                .Where(attempt => attempt.UserIpAddress == ip)
                .ToList();
        }
    }
}
