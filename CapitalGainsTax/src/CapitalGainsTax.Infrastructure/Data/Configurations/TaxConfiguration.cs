using CapitalGainsTax.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalGainsTax.Infrastructure.Data.Configurations
{
    public class TaxConfiguration : IEntityTypeConfiguration<Tax>
    {
        public void Configure(EntityTypeBuilder<Tax> builder)
        {
            builder.HasKey(t => t.TaxId);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.Value)
                .HasColumnType("decimal(5,4)") // ex: 0.2000 = 20%
                .IsRequired();
        }
    }
}