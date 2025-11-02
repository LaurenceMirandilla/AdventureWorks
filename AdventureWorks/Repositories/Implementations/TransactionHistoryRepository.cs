using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorks.Model.Domain.Production;
using AdventureWorks.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Repositories.Implementations
{
    public class TransactionHistoryRepository : ITransactionHistoryRepository
    {
        private readonly AdventureWorksContext _context;

        public TransactionHistoryRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransactionHistory>> GetAllAsync()
        {
            return await _context.TransactionHistories
                .Include(t => t.Product)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<TransactionHistory>> GetByProductIdAsync(int productId)
        {
            return await _context.TransactionHistories
                .Where(t => t.ProductId == productId)
                .OrderByDescending(t => t.TransactionDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TransactionHistory?> GetByIdAsync(int transactionId)
        {
            return await _context.TransactionHistories
                .Include(t => t.Product)
                .FirstOrDefaultAsync(t => t.TransactionId == transactionId);
        }

        public async Task<IEnumerable<TransactionHistory>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.TransactionHistories
                .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
                .OrderBy(t => t.TransactionDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(TransactionHistory entity)
        {
            await _context.TransactionHistories.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TransactionHistory entity)
        {
            _context.TransactionHistories.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int transactionId)
        {
            var entity = await _context.TransactionHistories.FindAsync(transactionId);
            if (entity != null)
            {
                _context.TransactionHistories.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

