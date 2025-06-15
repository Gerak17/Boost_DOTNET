using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using wpfdotnet.Model;

namespace wpfdotnet.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Artpiece> Artpieces { get; set; }

        private readonly IConfiguration _configuration;

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection") 
                    ?? throw new InvalidOperationException("Connexion à la base manquante");

                optionsBuilder.UseNpgsql(connectionString);
            }
        }
    }
}