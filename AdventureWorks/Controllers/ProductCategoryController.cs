using AdventureWorks;
using AdventureWorks.DTO;
using AdventureWorks.Model.Domain.Production;
using AdventureWorks.Validation;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly AdventureWorksContext _context;
        private readonly IMapper _mapper;
        private readonly ProductCategoryValidator _validator;

        public ProductCategoryController(AdventureWorksContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _validator = new ProductCategoryValidator();
        }

        // ✅ GET BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategoryDTO>> GetById(int id)
        {
            var entity = await _context.ProductCategories.FindAsync(id);
            if (entity == null) return NotFound();

            return Ok(_mapper.Map<ProductCategoryDTO>(entity));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search,[FromQuery] string? sortBy,
        [FromQuery] bool descending = false, [FromQuery] int page = 1,[FromQuery] int pageSize = 10)
        {
            var categories = _context.ProductCategories.AsQueryable();

            // 🔍 Filtering
            if (!string.IsNullOrWhiteSpace(search))
                categories = categories.Where(c => c.Name.Contains(search));

            // ↕️ Sorting
            categories = sortBy?.ToLower() switch
            {
                "name" => descending ? categories.OrderByDescending(c => c.Name) : categories.OrderBy(c => c.Name),
                _ => categories.OrderBy(c => c.ProductCategoryId)
            };

            // 📄 Pagination
            var totalItems = await categories.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var data = await categories
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<ProductCategoryDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(new
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Data = data
            });
        }










        // ✅ ADD
        [HttpPost]
        public async Task<IActionResult> Add(ProductCategoryDTO dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            var entity = _mapper.Map<ProductCategory>(dto);
            await _context.ProductCategories.AddAsync(entity);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<ProductCategoryDTO>(entity);
            return CreatedAtAction(nameof(GetById), new { id = result.ProductCategoryId }, result);
        }

        // ✅ UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductCategoryDTO dto)
        {
            if (id != dto.ProductCategoryId)
                return BadRequest("ID mismatch.");

            var entity = await _context.ProductCategories.FindAsync(id);
            if (entity == null)
                return NotFound();

            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            _mapper.Map(dto, entity);
            _context.ProductCategories.Update(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.ProductCategories.FindAsync(id);
            if (entity == null) return NotFound();

            _context.ProductCategories.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
