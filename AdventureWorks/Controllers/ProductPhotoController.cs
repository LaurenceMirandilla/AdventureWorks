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
    public class ProductPhotoController : ControllerBase
    {
        private readonly AdventureWorksContext _context;
        private readonly IMapper _mapper;

        public ProductPhotoController(AdventureWorksContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ✅ GET ALL (filter + pagination)
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool? includeThumb = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.ProductPhotos.AsQueryable();
            if (includeThumb.HasValue && includeThumb.Value)
                query = query.Where(p => p.ThumbNailPhoto != null);

            var total = await query.CountAsync();
            var data = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(new { total, data });
        }

        // ✅ GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _context.ProductPhotos.FindAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        // ✅ ADD
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductPhotoDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = _mapper.Map<ProductPhoto>(dto);
            await _context.ProductPhotos.AddAsync(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = entity.ProductPhotoId }, entity);
        }

        // ✅ UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductPhotoDTO dto)
        {
            if (id != dto.ProductPhotoId) return BadRequest("ID mismatch.");

            var entity = await _context.ProductPhotos.FindAsync(id);
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
            var entity = await _context.ProductPhotos.FindAsync(id);
            if (entity == null) return NotFound();

            _context.ProductPhotos.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}