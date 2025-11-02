using AdventureWorks.Model.Domain.Production;

namespace AdventureWorks.Repositories.Interfaces
{
    public interface IProductListPriceHistoryRepository
    {
        Task<IEnumerable<ProductListPriceHistory>> GetAllAsync();
        Task<IEnumerable<ProductListPriceHistory>> GetByProductIdAsync(int productId);
        Task<ProductListPriceHistory?> GetByKeyAsync(int productId, DateTime startDate);
        Task<IEnumerable<ProductListPriceHistory>> GetActivePricesAsync(DateTime asOfDate);
        Task AddAsync(ProductListPriceHistory entity);
        Task UpdateAsync(ProductListPriceHistory entity);
        Task DeleteAsync(int productId, DateTime startDate);
    }
}
