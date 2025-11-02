using AdventureWorks.Model.Domain.Production;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Repositories.Interfaces
{
    public class ProductListPriceHistoryRepository : IProductListPriceHistoryRepository
    {
        private readonly AdventureWorksContext _context;

        public ProductListPriceHistoryRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductListPriceHistory>> GetAllAsync()
        {
            return await _context.ProductListPriceHistories
                .Include(plph => plph.Product)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductListPriceHistory>> GetByProductIdAsync(int productId)
        {
            return await _context.ProductListPriceHistories
                .Where(plph => plph.ProductId == productId)
                .OrderByDescending(plph => plph.StartDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductListPriceHistory?> GetByKeyAsync(int productId, DateTime startDate)
        {
            return await _context.ProductListPriceHistories
                .FirstOrDefaultAsync(plph => plph.ProductId == productId && plph.StartDate == startDate);
        }

        public async Task<IEnumerable<ProductListPriceHistory>> GetActivePricesAsync(DateTime asOfDate)
        {
            return await _context.ProductListPriceHistories
                .Where(plph => plph.StartDate <= asOfDate &&
                               (plph.EndDate == null || plph.EndDate >= asOfDate))
                .Include(plph => plph.Product)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(ProductListPriceHistory entity)
        {
            await _context.ProductListPriceHistories.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductListPriceHistory entity)
        {
            _context.ProductListPriceHistories.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int productId, DateTime startDate)
        {
            var entity = await GetByKeyAsync(productId, startDate);
            if (entity != null)
            {
                _context.ProductListPriceHistories.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
