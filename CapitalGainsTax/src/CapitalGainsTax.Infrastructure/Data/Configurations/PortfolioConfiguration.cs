using CapitalGainsTax.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalGainsTax.Infrastructure.Data.Configurations
{
    public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.HasKey(p => p.PortfolioId);

            builder.Property(p => p.InvestorName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.WeightedAveragePrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.LossCarryForward)
                .HasColumnType("decimal(18,2)");

            // relacionamento 1:N (Portfolio -> Operations)
            builder.HasMany(typeof(Operation))
                   .WithOne()
                   .HasForeignKey("PortfolioId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}