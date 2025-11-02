using AdventureWorks;
using AdventureWorks.DTO;
using AdventureWorks.Model.Domain.Production;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AdventureWorks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly AdventureWorksContext _context;
        private readonly IMapper _mapper;

        public DocumentController(AdventureWorksContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ✅ GET ALL + Filter + Sort + Pagination
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? sort,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = _context.Documents.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(d => d.Title.Contains(search) || d.FileName.Contains(search));

            query = sort?.ToLower() switch
            {
                "title_desc" => query.OrderByDescending(d => d.Title),
                "modified" => query.OrderByDescending(d => d.ModifiedDate),
                _ => query.OrderBy(d => d.Title)
            };

            var total = await query.CountAsync();
            var data = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(new { total, data });
        }

        // ✅ GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var document = await _context.Documents.FindAsync(id);
            return document == null ? NotFound() : Ok(document);
        }

        // ✅ CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DocumentDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var entity = _mapper.Map<Document>(dto);
            _context.Documents.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = entity.DocumentNode }, entity);
        }

        // ✅ UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] DocumentDTO dto)
        {
            var entity = await _context.Documents.FindAsync(id);
            if (entity == null) return NotFound();
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ✅ DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _context.Documents.FindAsync(id);
            if (entity == null) return NotFound();
            _context.Documents.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
