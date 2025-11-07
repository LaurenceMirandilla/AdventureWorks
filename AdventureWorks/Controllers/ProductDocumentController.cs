using AdventureWorks;
using AdventureWorks.DTO;
using AdventureWorks.Model.Domain.Production;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
namespace AdventureWorks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDocumentController : ControllerBase
    {
        private readonly AdventureWorksContext _context;
        private readonly IMapper _mapper;

        public ProductDocumentController(AdventureWorksContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var total = await _context.ProductDocuments.CountAsync();
            var data = await _context.ProductDocuments
                .Include(d => d.Product)
                .Include(d => d.DocumentNodeNavigation)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return Ok(new { total, data });
        }
            
        [HttpGet("{id}")] public async Task<IActionResult> GetByKeyAsync(int id) 
        { var entity = await _context.ProductInventories .Include(p => p.Product)
        .FirstOrDefaultAsync(p => p.ProductInventoryId == id); if (entity == null)
                return NotFound(); return Ok(entity); }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductDocumentDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = _mapper.Map<ProductDocument>(dto);
            _context.ProductDocuments.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByKeyAsync), new { productId = entity.ProductId, DocumentNode = entity.DocumentNode }, entity);
        }

        [HttpPut("{ProductInventoryid}/{DocumentNode}")]
        public async Task<IActionResult> Update(int ProductInventoryid, string documentNode, [FromBody] ProductDocumentDTO dto)
        {
            if (ProductInventoryid != dto.ProductId || documentNode != dto.DocumentNode)
                return BadRequest("ID mismatch.");

            var entity = await _context.ProductDocuments
                .FirstOrDefaultAsync(pd => pd.ProductId == ProductInventoryid && pd.DocumentNode == documentNode);

            if (entity == null) return NotFound();

            _mapper.Map(dto, entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{ProductInventoryid}/{DocumentNode}")]
        public async Task<IActionResult> Delete(int ProductInventoryid, string documentNode)
        {
            var entity = await _context.ProductDocuments    
                .FirstOrDefaultAsync(pd => pd.ProductId == ProductInventoryid && pd.DocumentNode == documentNode);

            if (entity == null) return NotFound();

            _context.ProductDocuments.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}