using AdventureWorks;
using AdventureWorks.DTO;
using AdventureWorks.Model.Domain.Production;
using AdventureWorks.Validation;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly AdventureWorksContext _context;
        private readonly IMapper _mapper;
        private readonly ProductValidator _validator;

        public ProductController(AdventureWorksContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _validator = new ProductValidator();
        }

        // ✅ GET ALL (filter, sort, pagination)
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryParameters query)
        {
            var products = _context.Products.AsQueryable();

            // 🔍 Filtering
            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                products = products.Where(p => p.Name.Contains(query.Search) || p.ProductNumber.Contains(query.Search));
            }

            // ↕️ Sorting
            products = query.SortBy?.ToLower() switch
            {
                "name" => query.Descending ? products.OrderByDescending(p => p.Name) : products.OrderBy(p => p.Name),
                "price" => query.Descending ? products.OrderByDescending(p => p.ListPrice) : products.OrderBy(p => p.ListPrice),
                _ => products.OrderBy(p => p.ProductId)
            };

            // 📄 Pagination
            var totalItems = await products.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)query.PageSize);
            var data = await products
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
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
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            var entity = await _context.Products.FindAsync(id);
            if (entity == null) return NotFound();

            return Ok(_mapper.Map<ProductDTO>(entity));
        }

        // ✅ ADD
        [HttpPost]
        public async Task<IActionResult> Add(ProductDTO dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            var entity = _mapper.Map<Product>(dto);
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<ProductDTO>(entity);
            return CreatedAtAction(nameof(GetById), new { id = result.ProductId }, result);
        }

        // ✅ UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductDTO dto)
        {
            if (id != dto.ProductId)
                return BadRequest("ID mismatch.");

            var entity = await _context.Products.FindAsync(id);
            if (entity == null)
                return NotFound();

            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            _mapper.Map(dto, entity);
            _context.Products.Update(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Products.FindAsync(id);
            if (entity == null) return NotFound();

            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}


