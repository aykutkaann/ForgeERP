using ForgeERP.Inventory.Application.StockUseCases.Interfaces;
using ForgeERP.Inventory.Domain;
using ForgeERP.Inventory.Domain.Stock;
using ForgeERP.Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Inventory.Infrastructure.Repositories
{
    public class StockBalanceRepository :IStockBalanceRepository
    {
        private readonly InventoryDbContext _db;

        public StockBalanceRepository(InventoryDbContext db)
        {
            _db = db;
        }

        public async Task<StockBalance?> GetByItemAndWarehouseAsync(Guid itemId, string warehouse)
        {
            return await _db.StockBalances.FirstOrDefaultAsync(x => x.ItemId == itemId && x.Warehouse == warehouse);
        }

        public async Task<StockBalance?> GetByIdAsync(StockBalanceId id, CancellationToken cancellationToken)
        {
            return await _db.StockBalances.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task AddAsync(StockBalance entity, CancellationToken cancellationToken)
        {
            await _db.StockBalances.AddAsync(entity,cancellationToken);
        }

        public void Update(StockBalance entity)
        {
            _db.StockBalances.Update(entity);
        }

        public void Delete(StockBalance entity)
        {
            _db.StockBalances.Remove(entity);
        }




    }
}
