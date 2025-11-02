using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureWorks.Model.Domain.Production;
using AdventureWorks.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Repositories.Implementations
{
    public class CultureRepository : ICultureRepository
    {
        private readonly AdventureWorksContext _context;

        public CultureRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Culture>> GetAllAsync()
        {
            return await _context.Cultures.AsNoTracking().ToListAsync();
        }

        public async Task<Culture?> GetByIdAsync(string id)
        {
            return await _context.Cultures
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CultureId == id);
        }

        public async Task AddAsync(Culture entity)
        {
            await _context.Cultures.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Culture entity)
        {
            _context.Cultures.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _context.Cultures.FindAsync(id);
            if (entity != null)
            {
                _context.Cultures.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
