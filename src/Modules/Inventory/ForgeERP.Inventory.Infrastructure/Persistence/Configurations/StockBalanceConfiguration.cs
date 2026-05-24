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
    public class StockBalanceConfiguration :IEntityTypeConfiguration<StockBalance>
    {
        public void Configure(EntityTypeBuilder<StockBalance> builder)
        {
            builder.ToTable("StockBalances", "inventory");

            var balanceIdConverter = new ValueConverter<StockBalanceId, Guid>(id => id.Value, value => new StockBalanceId(value));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(balanceIdConverter).ValueGeneratedNever();

            builder.Property(x => x.ItemId).IsRequired();
            builder.Property(x => x.Warehouse).IsRequired().HasMaxLength(100);
            builder.Property(x => x.QuantityOnHand).IsRequired();
            builder.Property(x => x.QuantityReserved).IsRequired();

            builder.Ignore(x => x.QuantityAvailable);

            builder.HasMany(x => x.StockMovements)
                .WithOne()
                .HasForeignKey(x => x.StockBalanceId);

            builder.Navigation(x => x.StockMovements)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
