using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Api.Core {
    public class AttemptScoreCalculator : IAttemptScoreCalculator {
        public int CalculateScore(Attempt attempt) {
            return 50;
        }
    }
}
