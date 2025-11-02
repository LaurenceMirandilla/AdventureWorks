using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureWorks.Model.Domain.Production;
using AdventureWorks.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Repositories.Implementations
{
    public class ScrapReasonRepository : IScrapReasonRepository
    {
        private readonly AdventureWorksContext _context;

        public ScrapReasonRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ScrapReason>> GetAllAsync()
        {
            return await _context.ScrapReasons
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ScrapReason?> GetByIdAsync(int id)
        {
            return await _context.ScrapReasons
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.ScrapReasonId == id);
        }

        public async Task AddAsync(ScrapReason entity)
        {
            await _context.ScrapReasons.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ScrapReason entity)
        {
            _context.ScrapReasons.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ScrapReasons.FindAsync(id);
            if (entity != null)
            {
                _context.ScrapReasons.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

