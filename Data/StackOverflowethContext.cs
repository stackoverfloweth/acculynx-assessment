using Data.Entities;
using System.Data.Entity;

namespace Data {
    public class StackOverflowethContext : DbContext, IStackOverflowethContext {
        public StackOverflowethContext() : base("name=StackOverflowethContext") { }
        public DbSet<Attempt> Attempts { get; set; }
    }
}
