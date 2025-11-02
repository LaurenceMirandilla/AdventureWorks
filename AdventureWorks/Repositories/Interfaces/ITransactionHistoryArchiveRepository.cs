using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureWorks.Model.Domain.Production;

namespace AdventureWorks.Repositories.Interfaces
{
    public interface ITransactionHistoryArchiveRepository
    {
        Task<IEnumerable<TransactionHistoryArchive>> GetAllAsync();
        Task<TransactionHistoryArchive?> GetByIdAsync(int transactionId);
        Task<IEnumerable<TransactionHistoryArchive>> GetByProductIdAsync(int productId);
        Task AddAsync(TransactionHistoryArchive entity);
        Task DeleteAsync(int transactionId);
    }
}
