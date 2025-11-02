using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AdventureWorks.Model.Domain.Production;
using AdventureWorks.Repositories.Interfaces;

namespace AdventureWorks.Repositories.Implementations
{
    public class WorkOrderRoutingRepository : IWorkOrderRoutingRepository
    {
        private readonly AdventureWorksContext _context;

        public WorkOrderRoutingRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkOrderRouting>> GetAllAsync()
        {
            return await _context.WorkOrderRoutings
                .Include(r => r.WorkOrder)
                .Include(r => r.Location)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<WorkOrderRouting?> GetByIdAsync(int workOrderId, short operationSequence)
        {
            return await _context.WorkOrderRoutings
                .Include(r => r.WorkOrder)
                .Include(r => r.Location)
                .FirstOrDefaultAsync(r => r.WorkOrderId == workOrderId && r.OperationSequence == operationSequence);
        }

        public async Task<IEnumerable<WorkOrderRouting>> GetByWorkOrderIdAsync(int workOrderId)
        {
            return await _context.WorkOrderRoutings
                .Where(r => r.WorkOrderId == workOrderId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(WorkOrderRouting entity)
        {
            await _context.WorkOrderRoutings.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WorkOrderRouting entity)
        {
            _context.WorkOrderRoutings.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int workOrderId, short operationSequence)
        {
            var entity = await _context.WorkOrderRoutings
                .FirstOrDefaultAsync(r => r.WorkOrderId == workOrderId && r.OperationSequence == operationSequence);

            if (entity != null)
            {
                _context.WorkOrderRoutings.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
