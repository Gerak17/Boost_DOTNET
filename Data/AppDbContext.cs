using Microsoft.EntityFrameworkCore;
using wpfdotnet.Model;

namespace wpfdotnet.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Artpiece> Artpieces { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=dotnet;Username=postgres;Password=germain");
        }
    }
}