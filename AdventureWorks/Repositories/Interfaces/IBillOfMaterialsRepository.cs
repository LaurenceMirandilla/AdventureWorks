using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureWorks.Model.Domain.Production;

namespace AdventureWorks.Repositories.Interfaces
{
    public interface IBillOfMaterialsRepository
    {
        Task<IEnumerable<BillOfMaterials>> GetAllAsync();
        Task<BillOfMaterials?> GetByIdAsync(int id);
        Task<IEnumerable<BillOfMaterials>> GetByProductAsync(int productAssemblyId);
        Task AddAsync(BillOfMaterials entity);
        Task UpdateAsync(BillOfMaterials entity);
        Task DeleteAsync(int id);
    }
}
