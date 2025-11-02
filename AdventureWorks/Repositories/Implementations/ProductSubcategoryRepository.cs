using AdventureWorks.Model.Domain.Production;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Repositories.Interfaces
{
    public class ProductSubcategoryRepository : IProductSubcategoryRepository
    {
        private readonly DbContext _context;

        public ProductSubcategoryRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductSubcategory>> GetAllAsync()
        {
            return await _context.Set<ProductSubcategory>()
                .Include(ps => ps.Products)
                .ToListAsync();
        }

        public async Task<ProductSubcategory> GetByIdAsync(int id)
        {
            return await _context.Set<ProductSubcategory>()
                .Include(ps => ps.Products)
                .FirstOrDefaultAsync(ps => ps.ProductSubcategoryId == id);
        }

        public async Task<IEnumerable<ProductSubcategory>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Set<ProductSubcategory>()
                .Where(ps => ps.ProductCategoryId == categoryId)
                .ToListAsync();
        }

        public async Task AddAsync(ProductSubcategory subcategory)
        {
            await _context.Set<ProductSubcategory>().AddAsync(subcategory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductSubcategory subcategory)
        {
            _context.Set<ProductSubcategory>().Update(subcategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var subcategory = await _context.Set<ProductSubcategory>().FindAsync(id);
            if (subcategory != null)
            {
                _context.Set<ProductSubcategory>().Remove(subcategory);
                await _context.SaveChangesAsync();
            }
        }
    }
}
