using AdventureWorks.Model.Domain.Production;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Repositories.Interfaces
{
    public class ProductModelProductDescriptionCultureRepository : IProductModelProductDescriptionCultureRepository
    {
        private readonly AdventureWorksContext _context;

        public ProductModelProductDescriptionCultureRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductModelProductDescriptionCulture>> GetAllAsync()
        {
            return await _context.ProductModelProductDescriptionCultures
                .Include(p => p.ProductModel)
                .Include(p => p.ProductDescription)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductModelProductDescriptionCulture>> GetByModelIdAsync(int modelId)
        {
            return await _context.ProductModelProductDescriptionCultures
                .Where(p => p.ProductModelId == modelId)
                .Include(p => p.ProductDescription)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductModelProductDescriptionCulture?> GetByKeyAsync(int modelId, int descriptionId, string cultureId)
        {
            return await _context.ProductModelProductDescriptionCultures
                .Include(p => p.ProductDescription)
                .FirstOrDefaultAsync(p => p.ProductModelId == modelId &&
                                          p.ProductDescriptionId == descriptionId &&
                                          p.CultureId == cultureId);
        }

        public async Task AddAsync(ProductModelProductDescriptionCulture entity)
        {
            await _context.ProductModelProductDescriptionCultures.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductModelProductDescriptionCulture entity)
        {
            _context.ProductModelProductDescriptionCultures.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int modelId, int descriptionId, string cultureId)
        {
            var entity = await GetByKeyAsync(modelId, descriptionId, cultureId);
            if (entity != null)
            {
                _context.ProductModelProductDescriptionCultures.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
