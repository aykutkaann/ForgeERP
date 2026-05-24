using ForgeERP.Inventory.Application;
using ForgeERP.Inventory.Domain.Stock;
using ForgeERP.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Inventory.Infrastructure.Persistence
{
    public class InventoryDbContext :DbContext , IInventoryUnitOfWork
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

        public DbSet<StockBalance> StockBalances { get; init; }
        public DbSet<StockMovement> StockMovements { get; init; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
