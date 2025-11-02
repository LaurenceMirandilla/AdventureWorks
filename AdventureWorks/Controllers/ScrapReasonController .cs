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
    public class ScrapReasonController : ControllerBase
    {
        private readonly AdventureWorksContext _context;
        private readonly IMapper _mapper;

        public ScrapReasonController(AdventureWorksContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET ALL + Filter + Sort + Pagination
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search, [FromQuery] string? sort, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.ScrapReasons.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(s => s.Name.Contains(search));

            query = sort?.ToLower() switch
            {
                "name_desc" => query.OrderByDescending(s => s.Name),
                _ => query.OrderBy(s => s.Name)
            };

            var totalCount = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(new { totalCount, items });
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var scrap = await _context.ScrapReasons.FindAsync(id);
            return scrap == null ? NotFound() : Ok(scrap);
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ScrapReasonDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var scrap = _mapper.Map<ScrapReason>(dto);
            _context.ScrapReasons.Add(scrap);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = scrap.ScrapReasonId }, scrap);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ScrapReasonDTO dto)
        {
            var scrap = await _context.ScrapReasons.FindAsync(id);
            if (scrap == null) return NotFound();
            _mapper.Map(dto, scrap);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var scrap = await _context.ScrapReasons.FindAsync(id);
            if (scrap == null) return NotFound();
            _context.ScrapReasons.Remove(scrap);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
