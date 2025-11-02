using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureWorks.Model.Domain.Production;

namespace AdventureWorks.Repositories.Interfaces
{
    public interface IWorkOrderRepository
    {
        Task<IEnumerable<WorkOrder>> GetAllAsync();
        Task<WorkOrder?> GetByIdAsync(int id);
        Task<IEnumerable<WorkOrder>> GetByProductIdAsync(int productId);
        Task AddAsync(WorkOrder entity);
        Task UpdateAsync(WorkOrder entity);
        Task DeleteAsync(int id);
    }
}
