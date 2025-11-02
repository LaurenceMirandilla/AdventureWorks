using AdventureWorks;
using AdventureWorks.DTO;
using AdventureWorks.Model.Domain.Production;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace AdventureWorks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductCostHistoryController : ControllerBase
    {
        private readonly AdventureWorksContext _context;
        private readonly IMapper _mapper;

        public ProductCostHistoryController(AdventureWorksContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ProductCostHistory
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10,
                                                [FromQuery] string? sortBy = null, [FromQuery] string? direction = "asc")
        {
            var query = _context.ProductCostHistories.AsQueryable();

            // Sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                query = direction == "desc"
                    ? query.OrderByDescending(e => EF.Property<object>(e, sortBy))
                    : query.OrderBy(e => EF.Property<object>(e, sortBy));
            }

            // Pagination
            var total = await query.CountAsync();
            var data = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(new { total, data });
        }

        // GET: api/ProductCostHistory/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _context.ProductCostHistories.FindAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        // POST: api/ProductCostHistory
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductCostHistoryDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = _mapper.Map<ProductCostHistory>(dto);
            _context.ProductCostHistories.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = entity.ProductId }, entity);
        }

        // PUT: api/ProductCostHistory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductCostHistoryDTO dto)
        {
            if (id != dto.ProductId)
                return BadRequest("ID mismatch.");

            var entity = await _context.ProductCostHistories.FindAsync(id);
            if (entity == null) return NotFound();

            _mapper.Map(dto, entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/ProductCostHistory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.ProductCostHistories.FindAsync(id);
            if (entity == null) return NotFound();

            _context.ProductCostHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
