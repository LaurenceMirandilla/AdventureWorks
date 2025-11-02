using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureWorks.Model.Domain.Production;

namespace AdventureWorks.Repositories.Interfaces
{
    public interface ITransactionHistoryRepository
    {
        Task<IEnumerable<TransactionHistory>> GetAllAsync();
        Task<IEnumerable<TransactionHistory>> GetByProductIdAsync(int productId);
        Task<TransactionHistory?> GetByIdAsync(int transactionId);
        Task<IEnumerable<TransactionHistory>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task AddAsync(TransactionHistory entity);
        Task UpdateAsync(TransactionHistory entity);
        Task DeleteAsync(int transactionId);
    }
}
