using Microsoft.EntityFrameworkCore;

namespace ProductTrial.Data.Entities
{
    public class ProductTrialDbContext : DbContext
    {
        public const string ProductTrialDbConnection = "ProductTrialConnection";

        public ProductTrialDbContext()
        {
        }

        public ProductTrialDbContext(DbContextOptions<ProductTrialDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductTrialDbContext).Assembly);
        }
    }
}