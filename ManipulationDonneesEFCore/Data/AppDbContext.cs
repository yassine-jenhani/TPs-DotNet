using manipulationDonneesEFCore.Interceptors;
using manipulationDonneesEFCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace manipulationDonneesEFCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MembershipType>().HasData(
                new MembershipType { Id = 1, SignUpFee = 0, DurationInMonth = 0, DiscountRate = 0 },
                new MembershipType { Id = 2, SignUpFee = 30, DurationInMonth = 1, DiscountRate = 10 },
                new MembershipType { Id = 3, SignUpFee = 90, DurationInMonth = 3, DiscountRate = 15 },
                new MembershipType { Id = 4, SignUpFee = 300, DurationInMonth = 12, DiscountRate = 20 }
            );

            // Chemin absolu vers le fichier JSON
            string jsonPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "Movies.json");

            if (File.Exists(jsonPath))
            {
                string movJson = System.IO.File.ReadAllText(jsonPath);
                List<Movie> movies = System.Text.Json.JsonSerializer
                    .Deserialize<List<Movie>>(movJson);

                foreach (Movie m in movies)
                    modelBuilder.Entity<Movie>().HasData(m);
            }
        }

        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new AuditInterceptor());
        }
    }
}
