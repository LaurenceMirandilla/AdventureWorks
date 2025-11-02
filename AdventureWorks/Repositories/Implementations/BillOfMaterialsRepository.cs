using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorks.Model.Domain.Production;
using AdventureWorks.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace AdventureWorks.Repositories.Implementations

{
    public class BillOfMaterialsRepository : IBillOfMaterialsRepository
    {
        private readonly AdventureWorksContext _context;

        public BillOfMaterialsRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BillOfMaterials>> GetAllAsync()
        {
            return await _context.BillOfMaterials
                .Include(b => b.ProductAssembly)
                .Include(b => b.Component)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<BillOfMaterials?> GetByIdAsync(int id)
        {
            return await _context.BillOfMaterials
                .Include(b => b.Component)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BillOfMaterialsId == id);
        }

        public async Task<IEnumerable<BillOfMaterials>> GetByProductAsync(int productAssemblyId)
        {
            return await _context.BillOfMaterials
                .Where(b => b.ProductAssemblyId == productAssemblyId)
                .Include(b => b.Component)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(BillOfMaterials entity)
        {
            await _context.BillOfMaterials.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BillOfMaterials entity)
        {
            _context.BillOfMaterials.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.BillOfMaterials.FindAsync(id);
            if (entity != null)
            {
                _context.BillOfMaterials.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

