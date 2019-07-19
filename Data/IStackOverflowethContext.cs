using System.Data.Entity;
using Data.Entities;

namespace Data
{
    public interface IStackOverflowethContext
    {
        int SaveChanges();
        DbSet<Attempt> Attempts { get; set; }
    }
}