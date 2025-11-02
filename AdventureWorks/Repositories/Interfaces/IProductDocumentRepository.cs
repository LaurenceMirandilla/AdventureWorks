using AdventureWorks.Model.Domain.Production;

namespace AdventureWorks.Repositories.Interfaces
{
    public interface IProductDocumentRepository
    {
        Task<IEnumerable<ProductDocument>> GetAllAsync();
        Task<IEnumerable<ProductDocument>> GetByProductIdAsync(int productId);
        Task<ProductDocument?> GetByKeyAsync(int productId, string documentNode);
        Task AddAsync(ProductDocument entity);
        Task UpdateAsync(ProductDocument entity);
        Task DeleteAsync(int productId, string documentNode);
    }
}
