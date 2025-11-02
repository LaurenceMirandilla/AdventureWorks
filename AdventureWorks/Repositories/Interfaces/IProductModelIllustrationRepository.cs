using AdventureWorks.Model.Domain.Production;

namespace AdventureWorks.Repositories.Interfaces
{
    public interface IProductModelIllustrationRepository
    {
        Task<IEnumerable<ProductModelIllustration>> GetAllAsync();
        Task<IEnumerable<ProductModelIllustration>> GetByProductModelIdAsync(int modelId);
        Task<ProductModelIllustration?> GetByKeyAsync(int modelId, int illustrationId);
        Task AddAsync(ProductModelIllustration entity);
        Task UpdateAsync(ProductModelIllustration entity);
        Task DeleteAsync(int modelId, int illustrationId);
    }
}
