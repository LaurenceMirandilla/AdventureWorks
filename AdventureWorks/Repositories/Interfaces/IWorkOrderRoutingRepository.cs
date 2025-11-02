using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureWorks.Model.Domain.Production;

namespace AdventureWorks.Repositories.Interfaces
{
    public interface IWorkOrderRoutingRepository
    {
        Task<IEnumerable<WorkOrderRouting>> GetAllAsync();
        Task<WorkOrderRouting?> GetByIdAsync(int workOrderId, short operationSequence);
        Task<IEnumerable<WorkOrderRouting>> GetByWorkOrderIdAsync(int workOrderId);
        Task AddAsync(WorkOrderRouting entity);
        Task UpdateAsync(WorkOrderRouting entity);
        Task DeleteAsync(int workOrderId, short operationSequence);
    }
}
