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
    public class BillOfMaterialsController : ControllerBase
    {
        private readonly AdventureWorksContext _context;
        private readonly IMapper _mapper;

        public BillOfMaterialsController(AdventureWorksContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? productId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.BillOfMaterials.AsQueryable();
            if (productId.HasValue)
                query = query.Where(b => b.ProductAssemblyId == productId.Value);

            var total = await query.CountAsync();
            var data = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return Ok(new { total, data });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var record = await _context.BillOfMaterials.FindAsync(id);
            return record == null ? NotFound() : Ok(record);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BillOfMaterialsDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var entity = _mapper.Map<BillOfMaterials>(dto);
            _context.BillOfMaterials.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = entity.BillOfMaterialsId }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BillOfMaterialsDTO dto)
        {
            var entity = await _context.BillOfMaterials.FindAsync(id);
            if (entity == null) return NotFound();
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.BillOfMaterials.FindAsync(id);
            if (entity == null) return NotFound();
            _context.BillOfMaterials.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
