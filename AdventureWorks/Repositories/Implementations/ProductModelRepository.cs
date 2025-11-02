using AdventureWorks.Model.Domain.Production;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Repositories.Interfaces
{
    public class ProductModelRepository : IProductModelRepository
    {
        private readonly AdventureWorksContext _context;

        public ProductModelRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductModel>> GetAllAsync()
        {
            return await _context.ProductModels
                .Include(pm => pm.ProductModelIllustrations)
                .Include(pm => pm.ProductModelProductDescriptionCultures)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductModel?> GetByIdAsync(int id)
        {
            return await _context.ProductModels
                .Include(pm => pm.ProductModelIllustrations)
                .Include(pm => pm.ProductModelProductDescriptionCultures)
                .FirstOrDefaultAsync(pm => pm.ProductModelId == id);
        }

        public async Task<IEnumerable<ProductModel>> SearchByNameAsync(string keyword)
        {
            return await _context.ProductModels
                .Where(pm => EF.Functions.Like(pm.Name, $"%{keyword}%"))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(ProductModel entity)
        {
            await _context.ProductModels.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductModel entity)
        {
            _context.ProductModels.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ProductModels.FindAsync(id);
            if (entity != null)
            {
                _context.ProductModels.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
