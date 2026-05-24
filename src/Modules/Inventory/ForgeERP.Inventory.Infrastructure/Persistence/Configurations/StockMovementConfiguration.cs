using ForgeERP.Inventory.Domain;
using ForgeERP.Inventory.Domain.Stock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Inventory.Infrastructure.Persistence.Configurations
{

    public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
    {
        public void Configure(EntityTypeBuilder<StockMovement> builder)
        {
            builder.ToTable("StockMovements", "inventory");

            var balanceIdConverter = new ValueConverter<StockMovementId, Guid>(id => id.Value, value => new StockMovementId(value));

            var stockBalanceIdConverter = new ValueConverter<StockBalanceId, Guid>(id => id.Value, value => new StockBalanceId(value));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(balanceIdConverter).ValueGeneratedNever();
            builder.Property(x => x.StockBalanceId).HasConversion(stockBalanceIdConverter).IsRequired();

            builder.Property(x => x.MovementType).IsRequired().HasConversion<string>();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Reason).IsRequired().HasMaxLength(200);

       

        }
    }
}
