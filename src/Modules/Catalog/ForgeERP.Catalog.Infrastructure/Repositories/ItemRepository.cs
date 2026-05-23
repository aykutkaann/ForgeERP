using ForgeERP.Catalog.Application.ItemUseCases.Interfaces;
using ForgeERP.Catalog.Domain.Item;
using ForgeERP.Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace ForgeERP.Catalog.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {

        private readonly CatalogDbContext _db;

        public ItemRepository(CatalogDbContext db)
        {
            _db = db;
        }

        public async Task<Item?> GetByCodeAsync(string itemCode, CancellationToken ct = default)
        {
            return await _db.Items.FirstOrDefaultAsync(x =>x.ItemCode == itemCode, ct);

        
        }

        public async Task<List<Item>> GetAllAsync(CancellationToken ct = default)
        {

            return await _db.Items.ToListAsync(ct);
        }


        public async Task<Item?> GetByIdAsync(ItemId id, CancellationToken cancellationToken)
        {
            var item = await _db.Items.FindAsync(new object[] {id}, cancellationToken);

            return item;
        }

        public async Task AddAsync(Item entity, CancellationToken ct)
        {
            await _db.AddAsync(entity, ct);
        }

        public void Update(Item entity)
        {
            _db.Update(entity);
        }

        public void Delete(Item entity)
        {
            _db.Remove(entity);
        }
    }
}
