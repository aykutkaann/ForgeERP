using ForgeERP.Inventory.Domain;
using ForgeERP.Inventory.Domain.Stock;
using ForgeERP.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Inventory.Application.StockUseCases.Interfaces
{
    public interface IStockBalanceRepository : IRepository<StockBalance,StockBalanceId>
    {
        Task<StockBalance?> GetByItemAndWarehouseAsync(Guid itemId, string warehouse);
    }
}
