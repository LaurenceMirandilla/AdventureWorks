using AdventureWorks.Model.Domain.Production;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Repositories.Interfaces
{
    public class ProductDocumentRepository : IProductDocumentRepository
    {
        private readonly AdventureWorksContext _context;

        public ProductDocumentRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDocument>> GetAllAsync()
        {
            return await _context.ProductDocuments
                .Include(pd => pd.DocumentNodeNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductDocument>> GetByProductIdAsync(int productId)
        {
            return await _context.ProductDocuments
                .Include(pd => pd.DocumentNodeNavigation)
                .Where(pd => pd.ProductId == productId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductDocument?> GetByKeyAsync(int productId, string documentNode)
        {
            return await _context.ProductDocuments
                .Include(pd => pd.DocumentNodeNavigation)
                .FirstOrDefaultAsync(pd => pd.ProductId == productId && pd.DocumentNode == documentNode);
        }

        public async Task AddAsync(ProductDocument entity)
        {
            await _context.ProductDocuments.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductDocument entity)
        {
            _context.ProductDocuments.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int productId, string documentNode)
        {
            var entity = await GetByKeyAsync(productId, documentNode);
            if (entity != null)
            {
                _context.ProductDocuments.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
