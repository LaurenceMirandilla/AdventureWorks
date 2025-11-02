using AdventureWorks.Model.Domain.Production;
using AdventureWorks.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventureWorks.Repositories.Implementations
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AdventureWorksContext _context;

        public DocumentRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Document>> GetAllAsync()
        {
            return await _context.Documents.AsNoTracking().ToListAsync();
        }

        public async Task<Document?> GetByIdAsync(string documentNode)
        {
            return await _context.Documents
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.DocumentNode == documentNode);
        }

        public async Task AddAsync(Document entity)
        {
            await _context.Documents.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Document entity)
        {
            _context.Documents.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string documentNode)
        {
            var entity = await _context.Documents.FindAsync(documentNode);
            if (entity != null)
            {
                _context.Documents.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

