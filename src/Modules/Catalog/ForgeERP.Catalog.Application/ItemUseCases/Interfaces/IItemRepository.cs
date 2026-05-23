using ForgeERP.Catalog.Domain.Item;
using ForgeERP.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Application.ItemUseCases.Interfaces
{
    public interface IItemRepository :IRepository<Item, ItemId>
    {
        Task<Item?> GetByCodeAsync(string itemCode, CancellationToken ct = default);
        Task<List<Item>> GetAllAsync(CancellationToken ct = default);

    }
}
