using AdventureWorks.Model.Domain.Production;

namespace AdventureWorks.Repositories.Interfaces
{
    public interface IProductModelRepository
    {
        Task<IEnumerable<ProductModel>> GetAllAsync();
        Task<ProductModel?> GetByIdAsync(int id);
        Task<IEnumerable<ProductModel>> SearchByNameAsync(string keyword);
        Task AddAsync(ProductModel entity);
        Task UpdateAsync(ProductModel entity);
        Task DeleteAsync(int id);
    }
}
