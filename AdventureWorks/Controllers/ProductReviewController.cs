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
    public class ProductReviewController : ControllerBase
    {
        private readonly AdventureWorksContext _context;
        private readonly IMapper _mapper;

        public ProductReviewController(AdventureWorksContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ✅ GET ALL with filtering and pagination
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? productId = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.ProductReviews.Include(r => r.Product).AsQueryable();

            if (productId.HasValue)
                query = query.Where(r => r.ProductId == productId.Value);

            var total = await query.CountAsync();
            var data = await query.OrderByDescending(r => r.ReviewDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new { total, data });
        }

        // ✅ GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _context.ProductReviews.FindAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        // ✅ ADD
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductReviewDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = _mapper.Map<ProductReview>(dto);
            await _context.ProductReviews.AddAsync(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = entity.ProductReviewId }, entity);
        }

        // ✅ UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductReviewDTO dto)
        {
            if (id != dto.ProductReviewId) return BadRequest("ID mismatch.");

            var entity = await _context.ProductReviews.FindAsync(id);
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
            var entity = await _context.ProductReviews.FindAsync(id);
            if (entity == null) return NotFound();

            _context.ProductReviews.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}