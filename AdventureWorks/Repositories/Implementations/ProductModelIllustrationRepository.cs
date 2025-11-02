using AdventureWorks.Model.Domain.Production;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Repositories.Interfaces
{
    public class ProductModelIllustrationRepository : IProductModelIllustrationRepository
    {
        private readonly AdventureWorksContext _context;

        public ProductModelIllustrationRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductModelIllustration>> GetAllAsync()
        {
            return await _context.ProductModelIllustrations
                .Include(pmi => pmi.ProductModel)
                .Include(pmi => pmi.Illustration)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductModelIllustration>> GetByProductModelIdAsync(int modelId)
        {
            return await _context.ProductModelIllustrations
                .Where(pmi => pmi.ProductModelId == modelId)
                .Include(pmi => pmi.Illustration)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductModelIllustration?> GetByKeyAsync(int modelId, int illustrationId)
        {
            return await _context.ProductModelIllustrations
                .Include(pmi => pmi.Illustration)
                .FirstOrDefaultAsync(pmi => pmi.ProductModelId == modelId && pmi.IllustrationId == illustrationId);
        }

        public async Task AddAsync(ProductModelIllustration entity)
        {
            await _context.ProductModelIllustrations.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductModelIllustration entity)
        {
            _context.ProductModelIllustrations.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int modelId, int illustrationId)
        {
            var entity = await GetByKeyAsync(modelId, illustrationId);
            if (entity != null)
            {
                _context.ProductModelIllustrations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

