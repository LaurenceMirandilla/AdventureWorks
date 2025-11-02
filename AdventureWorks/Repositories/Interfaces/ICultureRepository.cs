using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureWorks.Model.Domain.Production;

namespace AdventureWorks.Repositories.Interfaces
{
    public interface ICultureRepository
    {
        Task<IEnumerable<Culture>> GetAllAsync();
        Task<Culture?> GetByIdAsync(string id);
        Task AddAsync(Culture entity);
        Task UpdateAsync(Culture entity);
        Task DeleteAsync(string id);
    }
}
