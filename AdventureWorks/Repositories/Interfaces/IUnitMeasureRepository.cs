using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureWorks.Model.Domain.Production;

namespace AdventureWorks.Repositories.Interfaces
{
    public interface IUnitMeasureRepository
    {
        Task<IEnumerable<UnitMeasure>> GetAllAsync();
        Task<UnitMeasure?> GetByIdAsync(string id);
        Task<UnitMeasure?> GetByCodeAsync(string code);
        Task AddAsync(UnitMeasure entity);
        Task UpdateAsync(UnitMeasure entity);
        Task DeleteAsync(string code);
    }
}
