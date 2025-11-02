using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureWorks.Model.Domain.Production;

namespace AdventureWorks.Repositories.Interfaces
{
    public interface IScrapReasonRepository
    {
        Task<IEnumerable<ScrapReason>> GetAllAsync();
        Task<ScrapReason?> GetByIdAsync(int id);
        Task AddAsync(ScrapReason entity);
        Task UpdateAsync(ScrapReason entity);
        Task DeleteAsync(int id);
    }
}
