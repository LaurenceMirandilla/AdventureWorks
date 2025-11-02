using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorks.Model.Domain.Production;
using AdventureWorks.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Repositories.Implementations
{
    public class TransactionHistoryArchiveRepository : ITransactionHistoryArchiveRepository
    {
        private readonly AdventureWorksContext _context;

        public TransactionHistoryArchiveRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransactionHistoryArchive>> GetAllAsync()
        {
            return await _context.TransactionHistoryArchives
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TransactionHistoryArchive?> GetByIdAsync(int transactionId)
        {
            return await _context.TransactionHistoryArchives
                .FirstOrDefaultAsync(t => t.TransactionId == transactionId);
        }

        public async Task<IEnumerable<TransactionHistoryArchive>> GetByProductIdAsync(int productId)
        {
            return await _context.TransactionHistoryArchives
                .Where(t => t.ProductId == productId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(TransactionHistoryArchive entity)
        {
            await _context.TransactionHistoryArchives.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int transactionId)
        {
            var entity = await _context.TransactionHistoryArchives.FindAsync(transactionId);
            if (entity != null)
            {
                _context.TransactionHistoryArchives.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
