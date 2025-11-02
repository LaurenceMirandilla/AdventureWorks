using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorks.Model.Domain.Production;
using AdventureWorks.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Repositories.Implementations
{
    public class UnitMeasureRepository : IUnitMeasureRepository
    {
        private readonly AdventureWorksContext _context;

        public UnitMeasureRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UnitMeasure>> GetAllAsync()
        {
            return await _context.UnitMeasures
                .OrderBy(u => u.Name)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<UnitMeasure?> GetByCodeAsync(string code)
        {
            return await _context.UnitMeasures
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UnitMeasureCode == code);
        }

        public async Task AddAsync(UnitMeasure entity)
        {
            await _context.UnitMeasures.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UnitMeasure entity)
        {
            _context.UnitMeasures.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string code)
        {
            var entity = await _context.UnitMeasures.FindAsync(code);
            if (entity != null)
            {
                _context.UnitMeasures.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<UnitMeasure?> GetByIdAsync(string id)
        {
            var entity = id.ToString();
            return await _context.UnitMeasures
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UnitMeasureCode == entity);
        }
    }
     
}

