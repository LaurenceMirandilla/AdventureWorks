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
    public class ProductModelProductDescriptionCultureController : ControllerBase
    {
        private readonly AdventureWorksContext _context;
        private readonly IMapper _mapper;

        public ProductModelProductDescriptionCultureController(AdventureWorksContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ✅ GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? cultureId = null)
        {
            var query = _context.ProductModelProductDescriptionCultures.AsQueryable();
            if (!string.IsNullOrEmpty(cultureId))
                query = query.Where(p => p.CultureId == cultureId);

            return Ok(await query.ToListAsync());
        }

        // ✅ GET BY COMPOSITE KEY
        [HttpGet("{productModelId}/{productDescriptionId}/{cultureId}")]
        public async Task<IActionResult> Get(int productModelId, int productDescriptionId, string cultureId)
        {
            var entity = await _context.ProductModelProductDescriptionCultures
                .FindAsync(productModelId, productDescriptionId, cultureId);

            if (entity == null) return NotFound();
            return Ok(entity);
        }

        // ✅ ADD
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductModelProductDescriptionCultureDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = _mapper.Map<ProductModelProductDescriptionCulture>(dto);
            await _context.ProductModelProductDescriptionCultures.AddAsync(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { dto.ProductModelId, dto.ProductDescriptionId, dto.CultureId }, entity);
        }

        // ✅ DELETE
        [HttpDelete("{productModelId}/{productDescriptionId}/{cultureId}")]
        public async Task<IActionResult> Delete(int productModelId, int productDescriptionId, string cultureId)
        {
            var entity = await _context.ProductModelProductDescriptionCultures.FindAsync(productModelId, productDescriptionId, cultureId);
            if (entity == null) return NotFound();

            _context.ProductModelProductDescriptionCultures.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
