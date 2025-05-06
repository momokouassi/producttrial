using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ProductTrial.Data.Entities
{
    public class ProductTrialDbContext : DbContext
    {
        public const string ProductTrialDbConnection = "ProductTrialConnection";

        private readonly IConfiguration _configuration;

        public ProductTrialDbContext(DbContextOptions<ProductTrialDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string? connectionString = _configuration.GetConnectionString(ProductTrialDbConnection);
                if (!string.IsNullOrEmpty(connectionString))
                {
                    optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 4, 32)));
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductTrialDbContext).Assembly);
        }
    }
}