using ApiContestNew.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiContestNew.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalType> AnimalTypes { get; set; }
        public DbSet<AnimalVisitedLocation> AnimalVisitedLocations { get; set; }
        public DbSet<LocationPoint> LocationPoints { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 
        
        }
    }
}
