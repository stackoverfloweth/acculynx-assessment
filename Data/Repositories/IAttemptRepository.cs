﻿using System.Collections.Generic;
using Data.Entities;

namespace Data.Repositories
{
    public interface IAttemptRepository
    {
        IEnumerable<Attempt> GetAttempts(string ip);
        void InsertAttempt(Attempt attempt);
    }
}