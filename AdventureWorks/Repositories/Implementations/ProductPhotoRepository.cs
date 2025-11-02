using AdventureWorks.Model.Domain.Production;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Repositories.Interfaces
{
    public class ProductPhotoRepository : IProductPhotoRepository
    {
        private readonly AdventureWorksContext _context;

        public ProductPhotoRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductPhoto>> GetAllAsync()
        {
            return await _context.ProductPhotos
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductPhoto?> GetByIdAsync(int id)
        {
            return await _context.ProductPhotos
                .FirstOrDefaultAsync(pp => pp.ProductPhotoId == id);
        }

        public async Task AddAsync(ProductPhoto entity)
        {
            await _context.ProductPhotos.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductPhoto entity)
        {
            _context.ProductPhotos.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ProductPhotos.FindAsync(id);
            if (entity != null)
            {
                _context.ProductPhotos.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
