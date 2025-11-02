using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureWorks.Model.Domain.Production;

namespace AdventureWorks.Repositories.Interfaces
{
    public interface IIllustrationRepository
    {
        Task<IEnumerable<Illustration>> GetAllAsync();
        Task<Illustration?> GetByIdAsync(int id);
        Task AddAsync(Illustration entity);
        Task UpdateAsync(Illustration entity);
        Task DeleteAsync(int id);
    }
}
