using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AdventureWorks.Repositories.Interfaces;

namespace AdventureWorks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionHistoryArchiveController : ControllerBase
    {
        private readonly ITransactionHistoryArchiveRepository _repository;

        public TransactionHistoryArchiveController(ITransactionHistoryArchiveRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            int pageNumber = 1,
            int pageSize = 20,
            string? sortBy = "TransactionDate",
            bool isDescending = true)
        {
            var query = await _repository.GetAllAsync();

            query = isDescending
                ? query.OrderByDescending(e => EF.Property<object>(e, sortBy))
                : query.OrderBy(e => EF.Property<object>(e, sortBy));

            var paged = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return Ok(paged);
        }
    }
}

//Not working