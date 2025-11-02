using AdventureWorks;
using AdventureWorks.DTO;
using AdventureWorks.Model.Domain.Production;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
namespace AdventureWorks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductListPriceHistoryController : ControllerBase
    {
        private readonly AdventureWorksContext _context;
        private readonly IMapper _mapper;

        public ProductListPriceHistoryController(AdventureWorksContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ✅ GET ALL with filtering and pagination
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? productId = null)
        {
            var query = _context.ProductListPriceHistories.Include(p => p.Product).AsQueryable();

            if (productId.HasValue)
                query = query.Where(p => p.ProductId == productId.Value);

            var total = await query.CountAsync();
            var data = await query
                .OrderByDescending(p => p.StartDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new { total, data });
        }

        // ✅ GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _context.ProductListPriceHistories.FindAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        // ✅ ADD
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductListPriceHistoryDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = _mapper.Map<ProductListPriceHistory>(dto);
            await _context.ProductListPriceHistories.AddAsync(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = entity.ProductId }, entity);
        }

        // ✅ UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductListPriceHistoryDTO dto)
        {
            if (id != dto.ProductId) return BadRequest("ID mismatch.");

            var entity = await _context.ProductListPriceHistories.FindAsync(id);
            if (entity == null) return NotFound();

            _mapper.Map(dto, entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.ProductListPriceHistories.FindAsync(id);
            if (entity == null) return NotFound();

            _context.ProductListPriceHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}