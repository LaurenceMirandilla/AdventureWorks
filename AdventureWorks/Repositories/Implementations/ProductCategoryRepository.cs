using AdventureWorks.Model.Domain.Production;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Repositories.Interfaces
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly AdventureWorksContext _context;

        public ProductCategoryRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            return await _context.Set<ProductCategory>()
                .Include(pc => pc.ProductSubcategories)
                .ToListAsync();
        }

        public async Task<ProductCategory> GetByIdAsync(int id)
        {
            return await _context.Set<ProductCategory>()
                .Include(pc => pc.ProductSubcategories)
                .FirstOrDefaultAsync(pc => pc.ProductCategoryId == id);
        }

        public async Task AddAsync(ProductCategory category)
        {
            await _context.Set<ProductCategory>().AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductCategory category)
        {
            _context.Set<ProductCategory>().Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Set<ProductCategory>().FindAsync(id);
            if (category != null)
            {
                _context.Set<ProductCategory>().Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
