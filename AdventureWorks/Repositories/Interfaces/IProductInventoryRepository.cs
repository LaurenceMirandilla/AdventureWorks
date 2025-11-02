using AdventureWorks.Model.Domain.Production;

namespace AdventureWorks.Repositories.Interfaces
{
    public interface IProductInventoryRepository
    {
        Task<IEnumerable<ProductInventory>> GetAllAsync();
        Task<IEnumerable<ProductInventory>> GetByProductIdAsync(int productId);
        Task<IEnumerable<ProductInventory>> GetByLocationIdAsync(int locationId);
        Task<ProductInventory?> GetByKeyAsync(int productId, short locationId);
        Task AddAsync(ProductInventory entity);
        Task UpdateAsync(ProductInventory entity);
        Task DeleteAsync(int productId, short locationId);
    }
}
