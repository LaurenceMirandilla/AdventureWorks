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
    public class ProductModelIllustrationController : ControllerBase
    {
        private readonly AdventureWorksContext _context;
        private readonly IMapper _mapper;

        public ProductModelIllustrationController(AdventureWorksContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ✅ GET ALL (filter + pagination)
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? productModelId = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.ProductModelIllustrations.Include(p => p.ProductModel).AsQueryable();

            if (productModelId.HasValue)
                query = query.Where(p => p.ProductModelId == productModelId.Value);

            var total = await query.CountAsync();
            var data = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(new { total, data });
        }

        // ✅ GET BY ID (Composite key)
        [HttpGet("{productModelId}/{illustrationId}")]
        public async Task<IActionResult> Get(int productModelId, int illustrationId)
        {
            var entity = await _context.ProductModelIllustrations
                .FirstOrDefaultAsync(p => p.ProductModelId == productModelId && p.IllustrationId == illustrationId);
            if (entity == null) return NotFound();

            return Ok(entity);
        }

        // ✅ ADD
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductModelIllustrationDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = _mapper.Map<ProductModelIllustration>(dto);
            await _context.ProductModelIllustrations.AddAsync(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { productModelId = entity.ProductModelId, illustrationId = entity.IllustrationId }, entity);
        }

        // ✅ UPDATE
        [HttpPut("{productModelId}/{illustrationId}")]
        public async Task<IActionResult> Update(int productModelId, int illustrationId, [FromBody] ProductModelIllustrationDTO dto)
        {
            if (productModelId != dto.ProductModelId || illustrationId != dto.IllustrationId)
                return BadRequest("Composite key mismatch.");

            var entity = await _context.ProductModelIllustrations.FindAsync(productModelId, illustrationId);
            if (entity == null) return NotFound();

            _mapper.Map(dto, entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ DELETE
        [HttpDelete("{productModelId}/{illustrationId}")]
        public async Task<IActionResult> Delete(int productModelId, int illustrationId)
        {
            var entity = await _context.ProductModelIllustrations.FindAsync(productModelId, illustrationId);
            if (entity == null) return NotFound();

            _context.ProductModelIllustrations.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}