using AdventureWorks.Model.Domain.Production;
using Microsoft.SqlServer.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventureWorks.Repositories.Interfaces
{
    public interface IDocumentRepository
    {
        Task<IEnumerable<Document>> GetAllAsync();
        Task<Document?> GetByIdAsync(string documentNode);
        Task AddAsync(Document entity);
        Task UpdateAsync(Document entity);
        Task DeleteAsync(string documentNode);
    }
}
