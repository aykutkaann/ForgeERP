using ForgeERP.Catalog.Domain.Item;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Infrastructure.Persistence.Configurations
{
    public class ItemConfiguration: IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items", "catalog");

            var itemIdConverter = new ValueConverter<ItemId, Guid>(id => id.Value, value => new ItemId(value));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasConversion(itemIdConverter).ValueGeneratedNever();

            builder.Property(x => x.ItemCode).IsRequired().HasMaxLength(50);
            builder.HasIndex(x => x.ItemCode).IsUnique();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);

            builder.Property(x => x.UnitOfMeasure).IsRequired().HasMaxLength(20);

            builder.Property(x => x.ItemType).HasConversion<string>();
        }
    }
}
