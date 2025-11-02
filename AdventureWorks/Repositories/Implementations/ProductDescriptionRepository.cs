using AdventureWorks.Model.Domain.Production;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Repositories.Interfaces
{
    public class ProductDescriptionRepository : IProductDescriptionRepository
    {
        private readonly AdventureWorksContext _context;

        public ProductDescriptionRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDescription>> GetAllAsync()
        {
            return await _context.ProductDescriptions
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductDescription?> GetByIdAsync(int id)
        {
            return await _context.ProductDescriptions
                .FirstOrDefaultAsync(d => d.ProductDescriptionId == id);
        }

        public async Task<IEnumerable<ProductDescription>> SearchByTextAsync(string keyword)
        {
            return await _context.ProductDescriptions
                .Where(d => EF.Functions.Like(d.Description, $"%{keyword}%"))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(ProductDescription entity)
        {
            await _context.ProductDescriptions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductDescription entity)
        {
            _context.ProductDescriptions.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ProductDescriptions.FindAsync(id);
            if (entity != null)
            {
                _context.ProductDescriptions.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

