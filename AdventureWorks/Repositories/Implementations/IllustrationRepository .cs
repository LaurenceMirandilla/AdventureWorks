using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureWorks.Model.Domain.Production;
using AdventureWorks.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Repositories.Implementations
{
    public class IllustrationRepository : IIllustrationRepository
    {
        private readonly AdventureWorksContext _context;

        public IllustrationRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Illustration>> GetAllAsync()
        {
            return await _context.Illustrations.AsNoTracking().ToListAsync();
        }

        public async Task<Illustration?> GetByIdAsync(int id)
        {
            return await _context.Illustrations
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.IllustrationId == id);
        }

        public async Task AddAsync(Illustration entity)
        {
            await _context.Illustrations.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Illustration entity)
        {
            _context.Illustrations.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Illustrations.FindAsync(id);
            if (entity != null)
            {
                _context.Illustrations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

