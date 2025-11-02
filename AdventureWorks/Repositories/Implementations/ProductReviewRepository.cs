using AdventureWorks.Model.Domain.Production;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Repositories.Interfaces
{
    public class ProductReviewRepository : IProductReviewRepository
    {
        private readonly AdventureWorksContext _context;

        public ProductReviewRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductReview>> GetAllAsync()
        {
            return await _context.ProductReviews
                .Include(pr => pr.Product)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductReview>> GetByProductIdAsync(int productId)
        {
            return await _context.ProductReviews
                .Where(pr => pr.ProductId == productId)
                .OrderByDescending(pr => pr.ReviewDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductReview?> GetByIdAsync(int id)
        {
            return await _context.ProductReviews
                .Include(pr => pr.Product)
                .FirstOrDefaultAsync(pr => pr.ProductReviewId == id);
        }

        public async Task<IEnumerable<ProductReview>> GetRecentReviewsAsync(int productId, int count)
        {
            return await _context.ProductReviews
                .Where(pr => pr.ProductId == productId)
                .OrderByDescending(pr => pr.ReviewDate)
                .Take(count)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(ProductReview entity)
        {
            await _context.ProductReviews.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductReview entity)
        {
            _context.ProductReviews.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ProductReviews.FindAsync(id);
            if (entity != null)
            {
                _context.ProductReviews.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

