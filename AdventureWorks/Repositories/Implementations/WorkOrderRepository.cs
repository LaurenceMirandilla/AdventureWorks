using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AdventureWorks.Model.Domain.Production;
using AdventureWorks.Repositories.Interfaces;

namespace AdventureWorks.Repositories.Implementations
{
    public class WorkOrderRepository : IWorkOrderRepository
    {
        private readonly AdventureWorksContext _context;

        public WorkOrderRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkOrder>> GetAllAsync()
        {
            return await _context.WorkOrders
                .Include(w => w.Product)
                .Include(w => w.ScrapReason)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<WorkOrder?> GetByIdAsync(int id)
        {
            return await _context.WorkOrders
                .Include(w => w.Product)
                .Include(w => w.ScrapReason)
                .FirstOrDefaultAsync(w => w.WorkOrderId == id);
        }

        public async Task<IEnumerable<WorkOrder>> GetByProductIdAsync(int productId)
        {
            return await _context.WorkOrders
                .Where(w => w.ProductId == productId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(WorkOrder entity)
        {
            await _context.WorkOrders.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WorkOrder entity)
        {
            _context.WorkOrders.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.WorkOrders.FindAsync(id);
            if (entity != null)
            {
                _context.WorkOrders.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

