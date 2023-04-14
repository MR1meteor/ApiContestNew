using ApiContestNew.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    Id = 1,
                    FirstName = "adminFirstName",
                    LastName = "adminLastName",
                    Email = "admin@simbirsoft.com",
                    Password = EncodePassword("qwerty123"),
                    Role = "ADMIN"
                },
                
                new Account
                {
                    Id = 2,
                    FirstName = "chipperFirstName",
                    LastName = "chipperLastName",
                    Email = "chipper@simbirsoft.com",
                    Password = EncodePassword("qwerty123"),
                    Role = "CHIPPER"
                },

                new Account
                {
                    Id = 3,
                    FirstName = "userFirstName",
                    LastName = "userLastName",
                    Email = "user@simbirsoft.com",
                    Password = EncodePassword("qwerty123"),
                    Role = "USER"
                }
                );
        }

        private string EncodePassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var encodedPassword = Convert.ToBase64String(bytes);
            return encodedPassword;
        }
    }
}
