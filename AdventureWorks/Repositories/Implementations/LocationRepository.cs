using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureWorks.Model.Domain.Production;
using AdventureWorks.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Repositories.Interfaces
{
    public class LocationRepository : ILocationRepository
    {
        private readonly AdventureWorksContext _context;

        public LocationRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            return await _context.Locations.AsNoTracking().ToListAsync();
        }

        public async Task<Location?> GetByIdAsync(int id)
        {
            return await _context.Locations
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.LocationId == id);
        }

        public async Task AddAsync(Location entity)
        {
            await _context.Locations.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Location entity)
        {
            _context.Locations.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Locations.FindAsync(id);
            if (entity != null)
            {
                _context.Locations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
