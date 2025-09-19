using CapitalGainsTax.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalGainsTax.Infrastructure.Data.Configurations
{
    public class OperationConfiguration : IEntityTypeConfiguration<Operation>
    {
        public void Configure(EntityTypeBuilder<Operation> builder)
        {
            builder.HasKey(o => o.OperationId);

            builder.Property(o => o.UnitCost)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(o => o.Quantity)
                .IsRequired();

            builder.Property(o => o.Type)
                .IsRequired();
        }
    }
}