using Data.Entities;
using System.Data.Entity;

namespace Data {
    public interface IStackOverflowethContext {
        int SaveChanges();
        DbSet<Attempt> Attempts { get; set; }
    }
}