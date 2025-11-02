using AdventureWorks;
using AdventureWorks.DTO;
using AdventureWorks.Model.Domain.Production;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CultureController : ControllerBase
{
    private readonly AdventureWorksContext _context;
    private readonly IMapper _mapper;

    public CultureController(AdventureWorksContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search)
    {
        var query = _context.Cultures.AsQueryable();
        if (!string.IsNullOrEmpty(search))
            query = query.Where(c => c.Name.Contains(search));
        return Ok(await query.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var culture = await _context.Cultures.FindAsync(id);
        return culture == null ? NotFound() : Ok(culture);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CultureDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = _mapper.Map<Culture>(dto);
        _context.Cultures.Add(entity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = entity.CultureId }, entity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] CultureDTO dto)
    {
        var culture = await _context.Cultures.FindAsync(id);
        if (culture == null) return NotFound();
        _mapper.Map(dto, culture);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var culture = await _context.Cultures.FindAsync(id);
        if (culture == null) return NotFound();
        _context.Cultures.Remove(culture);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

