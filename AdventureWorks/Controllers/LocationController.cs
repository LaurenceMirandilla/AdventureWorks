using AdventureWorks;
using AdventureWorks.DTO;
using AdventureWorks.Model.Domain.Production;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly AdventureWorksContext _context;
    private readonly IMapper _mapper;

    public LocationController(AdventureWorksContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // ✅ GET ALL + Filter + Pagination
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var query = _context.Locations.AsQueryable();

        if (!string.IsNullOrEmpty(search))
            query = query.Where(l => l.Name.Contains(search));

        var total = await query.CountAsync();
        var data = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return Ok(new { total, data });
    }

    // ✅ GET BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(short id)
    {
        var location = await _context.Locations.FindAsync(id);
        return location == null ? NotFound() : Ok(location);
    }

    // ✅ CREATE
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] LocationDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = _mapper.Map<Location>(dto);
        _context.Locations.Add(entity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = entity.LocationId }, entity);
    }

    // ✅ UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(short id, [FromBody] LocationDTO dto)
    {
        var entity = await _context.Locations.FindAsync(id);
        if (entity == null) return NotFound();
        _mapper.Map(dto, entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // ✅ DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(short id)
    {
        var entity = await _context.Locations.FindAsync(id);
        if (entity == null) return NotFound();
        _context.Locations.Remove(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

