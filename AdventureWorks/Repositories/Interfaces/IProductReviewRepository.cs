using AdventureWorks.Model.Domain.Production;

namespace AdventureWorks.Repositories.Interfaces
{
    public interface IProductReviewRepository
    {
        Task<IEnumerable<ProductReview>> GetAllAsync();
        Task<IEnumerable<ProductReview>> GetByProductIdAsync(int productId);
        Task<ProductReview?> GetByIdAsync(int id);
        Task<IEnumerable<ProductReview>> GetRecentReviewsAsync(int productId, int count);
        Task AddAsync(ProductReview entity);
        Task UpdateAsync(ProductReview entity);
        Task DeleteAsync(int id);
    }
}

