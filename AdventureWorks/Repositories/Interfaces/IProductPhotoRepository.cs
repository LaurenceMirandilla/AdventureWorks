using AdventureWorks.Model.Domain.Production;

namespace AdventureWorks.Repositories.Interfaces
{
    public interface IProductPhotoRepository
    {
        Task<IEnumerable<ProductPhoto>> GetAllAsync();
        Task<ProductPhoto?> GetByIdAsync(int id);
        Task AddAsync(ProductPhoto entity);
        Task UpdateAsync(ProductPhoto entity);
        Task DeleteAsync(int id);
    }
}
