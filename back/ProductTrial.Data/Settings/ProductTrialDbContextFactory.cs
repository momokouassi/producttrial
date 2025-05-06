using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ProductTrial.Data.Entities;

namespace ProductTrial.Data.Settings
{
    public class ProductTrialDbContextFactory : IDesignTimeDbContextFactory<ProductTrialDbContext>
    {
        private readonly IConfiguration _configuration;

        public ProductTrialDbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ProductTrialDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductTrialDbContext>();

            optionsBuilder.UseMySql(
                "server=localhost;user=usr_producttrial;password=PhwCy(hmhTXjoMGq;database=producttrial;", new MySqlServerVersion(new Version(10, 4, 32))
            );

            return new ProductTrialDbContext(optionsBuilder.Options, _configuration);
        }
    }
}