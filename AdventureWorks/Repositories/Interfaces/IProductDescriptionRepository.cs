using AdventureWorks.Model.Domain.Production;

namespace AdventureWorks.Repositories.Interfaces
{
    public interface IProductDescriptionRepository
    {
        Task<IEnumerable<ProductDescription>> GetAllAsync();
        Task<ProductDescription?> GetByIdAsync(int id);
        Task<IEnumerable<ProductDescription>> SearchByTextAsync(string keyword);
        Task AddAsync(ProductDescription entity);
        Task UpdateAsync(ProductDescription entity);
        Task DeleteAsync(int id);
    }
}
