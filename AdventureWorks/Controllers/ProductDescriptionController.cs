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
    public class ProductDescriptionController : ControllerBase
    {
        private readonly AdventureWorksContext _context;
        private readonly IMapper _mapper;

        public ProductDescriptionController(AdventureWorksContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10,
                                                [FromQuery] string? keyword = null)
        {
            var query = _context.ProductDescriptions.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(p => p.Description.Contains(keyword));

            var total = await query.CountAsync();
            var data = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(new { total, data });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _context.ProductDescriptions.FindAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductDescriptionDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = _mapper.Map<ProductDescription>(dto);
            _context.ProductDescriptions.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = entity.ProductDescriptionId }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDescriptionDTO dto)
        {
            if (id != dto.ProductDescriptionId)
                return BadRequest("ID mismatch.");

            var entity = await _context.ProductDescriptions.FindAsync(id);
            if (entity == null) return NotFound();

            _mapper.Map(dto, entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.ProductDescriptions.FindAsync(id);
            if (entity == null) return NotFound();

            _context.ProductDescriptions.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}