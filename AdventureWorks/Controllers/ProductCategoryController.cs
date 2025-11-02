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

        // ✅ GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryParameters query)
        {
            var categories = _context.ProductCategories.AsQueryable();

            // 🔍 Filtering
            if (!string.IsNullOrWhiteSpace(query.Search))
                categories = categories.Where(c => c.Name.Contains(query.Search));

            // ↕️ Sorting
            categories = query.SortBy?.ToLower() switch
            {
                "name" => query.Descending ? categories.OrderByDescending(c => c.Name) : categories.OrderBy(c => c.Name),
                _ => categories.OrderBy(c => c.ProductCategoryId)
            };

            // 📄 Pagination
            var totalItems = await categories.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)query.PageSize);
            var data = await categories
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ProjectTo<ProductCategoryDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(new
            {
                query.Page,
                query.PageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Data = data
            });
        }

        // ✅ GET BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategoryDTO>> GetById(int id)
        {
            var entity = await _context.ProductCategories.FindAsync(id);
            if (entity == null) return NotFound();

            return Ok(_mapper.Map<ProductCategoryDTO>(entity));
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
