using AdventureWorks.Model.Domain.Production;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Repositories.Interfaces
{
    public class ProductInventoryRepository : IProductInventoryRepository
    {
        private readonly AdventureWorksContext _context;

        public ProductInventoryRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductInventory>> GetAllAsync()
        {
            return await _context.ProductInventories
                .Include(pi => pi.Product)
                .Include(pi => pi.Location)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductInventory>> GetByProductIdAsync(int productId)
        {
            return await _context.ProductInventories
                .Where(pi => pi.ProductId == productId)
                .Include(pi => pi.Location)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductInventory>> GetByLocationIdAsync(int locationId)
        {
            return await _context.ProductInventories
                .Where(pi => pi.LocationId == locationId)
                .Include(pi => pi.Product)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductInventory?> GetByKeyAsync(int productId, short locationId)
        {
            return await _context.ProductInventories
                .Include(pi => pi.Product)
                .Include(pi => pi.Location)
                .FirstOrDefaultAsync(pi => pi.ProductId == productId && pi.LocationId == locationId);
        }

        public async Task AddAsync(ProductInventory entity)
        {
            await _context.ProductInventories.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductInventory entity)
        {
            _context.ProductInventories.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int productId, short locationId)
        {
            var entity = await GetByKeyAsync(productId, locationId);
            if (entity != null)
            {
                _context.ProductInventories.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
