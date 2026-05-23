using ForgeERP.Catalog.Domain.BOM;
using ForgeERP.Catalog.Domain.Item;
using ForgeERP.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Application.BomUseCases.Interfaces
{
    public interface IBomRepository : IRepository<Bom,BomId>
    {
        Task<Bom?> GetByItemIdAsync(ItemId itemId, CancellationToken ct =default);
    }
}
