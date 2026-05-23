using ForgeERP.Catalog.Domain.BOM;
using ForgeERP.Catalog.Domain.Item;
using ForgeERP.Catalog.Domain.Routing;
using ForgeERP.SharedKernel;
using Microsoft.EntityFrameworkCore;


namespace ForgeERP.Catalog.Infrastructure.Persistence
{
    public class CatalogDbContext : DbContext, IUnitOfWork
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

        public DbSet<Item> Items { get; init; }
        public DbSet<Bom> Boms { get; init; }
     



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }

}
