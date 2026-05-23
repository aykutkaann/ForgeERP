using ForgeERP.Catalog.Domain.BOM;
using ForgeERP.Catalog.Domain.Item;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Infrastructure.Persistence.Configurations
{
    public class BomLineConfiguration : IEntityTypeConfiguration<BomLine>
    {
        public void Configure(EntityTypeBuilder<BomLine> builder)
        {
            builder.ToTable("BomLines", "catalog");

            builder.HasKey(x => x.Id);

            var bomLineIdConverter = new ValueConverter<BomLineId, Guid>(id => id.Value, value => new BomLineId(value));
            var bomIdConverter = new ValueConverter<BomId, Guid>(id => id.Value, value => new BomId(value));
            var itemIdConverter = new ValueConverter<ItemId, Guid>(id => id.Value, value => new ItemId(value));

            builder.Property(x => x.Id).HasConversion(bomLineIdConverter).ValueGeneratedNever();


            builder.Property(x => x.BomId).HasConversion(bomIdConverter).IsRequired();
            builder.HasIndex(x => x.BomId);

            builder.Property(x => x.ComponentItemId).HasConversion(itemIdConverter).IsRequired();
            builder.HasIndex(x => x.ComponentItemId);

            builder.Property(x => x.Quantity).IsRequired();

            builder.Property(x => x.Position).IsRequired();



        }
    }
}
