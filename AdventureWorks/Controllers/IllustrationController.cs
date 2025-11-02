using AdventureWorks;
using AdventureWorks.DTO;
using AdventureWorks.Model.Domain.Production;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class IllustrationController : ControllerBase
{
    private readonly AdventureWorksContext _context;
    private readonly IMapper _mapper;

    public IllustrationController(AdventureWorksContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // ✅ GET ALL
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        var query = _context.Illustrations.AsQueryable();

        if (startDate.HasValue && endDate.HasValue)
            query = query.Where(i => i.ModifiedDate >= startDate.Value && i.ModifiedDate <= endDate.Value);

        var items = await query.ToListAsync();
        return Ok(items);
    }

    // ✅ GET BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var entity = await _context.Illustrations.FindAsync(id);
        return entity == null ? NotFound() : Ok(entity);
    }

    // ✅ CREATE
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] IllustrationDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = _mapper.Map<Illustration>(dto);
        _context.Illustrations.Add(entity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = entity.IllustrationId }, entity);
    }

    // ✅ UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] IllustrationDTO dto)
    {
        var entity = await _context.Illustrations.FindAsync(id);
        if (entity == null) return NotFound();
        _mapper.Map(dto, entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // ✅ DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Illustrations.FindAsync(id);
        if (entity == null) return NotFound();
        _context.Illustrations.Remove(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

