using ForgeERP.Catalog.Application.BomUseCases.Interfaces;
using ForgeERP.Catalog.Domain.BOM;
using ForgeERP.Catalog.Domain.Item;
using ForgeERP.Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Infrastructure.Repositories
{
    public class BomRepository :IBomRepository
    {
        private readonly CatalogDbContext _db;

        public BomRepository(CatalogDbContext db)
        {
            _db = db;
        }

        public async Task<Bom?> GetByItemIdAsync(ItemId itemId, CancellationToken ct = default)
        {

            return await _db.Boms.FirstOrDefaultAsync(x => x.ItemId == itemId, ct);
        }


        public async Task<Bom?> GetByIdAsync(BomId id, CancellationToken cancellationToken)
        {
            return await _db.Boms.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task AddAsync(Bom entity, CancellationToken cancellationToken)
        {
             await _db.Boms.AddAsync(entity,cancellationToken);
        }

        public void Update(Bom entity)
        {
            _db.Update(entity);
        }

        public void Delete(Bom entity)
        {
            _db.Remove(entity);
        }

    }
}
