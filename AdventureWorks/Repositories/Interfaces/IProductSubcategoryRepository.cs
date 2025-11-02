using AdventureWorks.Model.Domain.Production;

namespace AdventureWorks.Repositories.Interfaces
{
    public interface IProductSubcategoryRepository
    {
        Task<IEnumerable<ProductSubcategory>> GetAllAsync();
        Task<ProductSubcategory> GetByIdAsync(int id);
        Task<IEnumerable<ProductSubcategory>> GetByCategoryAsync(int categoryId);
        Task AddAsync(ProductSubcategory subcategory);
        Task UpdateAsync(ProductSubcategory subcategory);
        Task DeleteAsync(int id);
    }
}
