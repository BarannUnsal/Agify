using Agify.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Agify.DAL.Contexts
{
    public class AgifyDbContext : DbContext
    {
        public AgifyDbContext(DbContextOptions<AgifyDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1450;Database=AgifyDb;Persist Security Info=True;User ID=SA;Password=guclusifreguclubeyin;MultipleActiveResultSets=true",
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 4,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null
                        );
                });
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence("FE_Sequence");

            modelBuilder.Entity<User>()
                .Property(p => p.Id)
                .HasDefaultValueSql("NEXT VALUE FOR FE_Sequence");

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Name)
                .IsUnique();
        }
    }
}
