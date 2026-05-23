using ForgeERP.Catalog.Domain.BOM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Infrastructure.Persistence.Configurations
{
    public class BomConfiguration: IEntityTypeConfiguration<Bom>
    {
        public void Configure(EntityTypeBuilder<Bom> builder)
        {
            builder.ToTable("Boms", "catalog");

            var bomIdConverter = new ValueConverter<BomId, Guid>(id => id.Value, value => new BomId(value));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(bomIdConverter).ValueGeneratedNever();

            builder.Property(x => x.ItemId).IsRequired();
            builder.HasIndex(x => x.ItemId).IsUnique();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            builder.HasMany(x => x.BomLines)
                .WithOne()
                .HasForeignKey(x => x.BomId);

            builder.Navigation(x => x.BomLines)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
