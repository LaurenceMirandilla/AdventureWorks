using AdventureWorks.Model.Domain.Production;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Repositories.Interfaces
{
    public class ProductRepository : IProductRepository
    {
        private readonly AdventureWorksContext _context;

        public ProductRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Set<Product>()
                .Include(p => p.ProductModel)
                .Include(p => p.ProductSubcategoryId)
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Set<Product>()
                .Include(p => p.ProductModel)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Set<Product>()
                .Where(p => p.ProductSubcategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByModelAsync(int modelId)
        {
            return await _context.Set<Product>()
                .Where(p => p.ProductModelId == modelId)
                .ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Set<Product>().AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Set<Product>().Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Set<Product>().FindAsync(id);
            if (product != null)
            {
                _context.Set<Product>().Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}

