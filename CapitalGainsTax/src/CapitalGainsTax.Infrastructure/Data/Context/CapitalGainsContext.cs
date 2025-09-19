using CapitalGainsTax.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CapitalGainsTax.Infrastructure.Data.Context
{
    public class CapitalGainsContext : DbContext
    {
        public CapitalGainsContext(DbContextOptions<CapitalGainsContext> options) : base(options) { }

        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Tax> Taxes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // aplica todas as configurações da pasta /configurations
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}