using AdventureWorks;
using AdventureWorks.DTO;
using AdventureWorks.Model.Domain.Production;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace AdventureWorks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInventoryController : ControllerBase
    {
        private readonly AdventureWorksContext _context;
        private readonly IMapper _mapper;

        public ProductInventoryController(AdventureWorksContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ✅ GET ALL (with filtering, sorting, pagination)
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? location = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] string direction = "asc")
        {
            var query = _context.ProductInventories.Include(p => p.Product).AsQueryable();

            if (!string.IsNullOrEmpty(location))
                query = query.Where(pi => pi.LocationId.ToString() == location);

            if (!string.IsNullOrEmpty(sortBy))
            {
                query = direction == "desc"
                    ? query.OrderByDescending(e => EF.Property<object>(e, sortBy))
                    : query.OrderBy(e => EF.Property<object>(e, sortBy));
            }

            var total = await query.CountAsync();
            var data = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(new { total, data });
        }

        // ✅ GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _context.ProductInventories
                .Include(p => p.Product)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (entity == null) return NotFound();
            return Ok(entity);
        }

        // ✅ ADD
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductInventoryDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = _mapper.Map<ProductInventory>(dto);
            await _context.ProductInventories.AddAsync(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = entity.ProductId }, entity);
        }

        // ✅ UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductInventoryDTO dto)
        {
            if (id != dto.ProductId) return BadRequest("ID mismatch.");

            var entity = await _context.ProductInventories.FindAsync(id);
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
            var entity = await _context.ProductInventories.FindAsync(id);
            if (entity == null) return NotFound();

            _context.ProductInventories.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
