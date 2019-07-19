using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Data.Entities;

namespace Data {
    public class StackOverflowethContext : DbContext, IStackOverflowethContext {
        public StackOverflowethContext() : base("name=StackOverflowethContext") { }
        public DbSet<Attempt> Attempts { get; set; }
    }
}
