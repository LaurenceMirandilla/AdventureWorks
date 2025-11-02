using AdventureWorks.Model.Domain.Production;

namespace AdventureWorks.Repositories.Interfaces
{
    public interface IProductModelProductDescriptionCultureRepository
    {
        Task<IEnumerable<ProductModelProductDescriptionCulture>> GetAllAsync();
        Task<IEnumerable<ProductModelProductDescriptionCulture>> GetByModelIdAsync(int modelId);
        Task<ProductModelProductDescriptionCulture?> GetByKeyAsync(int modelId, int descriptionId, string cultureId);
        Task AddAsync(ProductModelProductDescriptionCulture entity);
        Task UpdateAsync(ProductModelProductDescriptionCulture entity);
        Task DeleteAsync(int modelId, int descriptionId, string cultureId);
    }
}
