using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureWorks.DTO;
using AdventureWorks.Model.Domain.Production;
using AdventureWorks.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductSubcategoryController : ControllerBase
    {
        private readonly IProductSubcategoryRepository _repository;
        private readonly IMapper _mapper;

        public ProductSubcategoryController(IProductSubcategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/ProductSubcategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductSubcategoryDTO>>> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<ProductSubcategoryDTO>>(entities);
            return Ok(dtos);
        }

        // GET: api/ProductSubcategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductSubcategoryDTO>> GetById(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(_mapper.Map<ProductSubcategoryDTO>(entity));
        }

        // GET: api/ProductSubcategory/category/3
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductSubcategoryDTO>>> GetByCategory(int categoryId)
        {
            var entities = await _repository.GetByCategoryAsync(categoryId);
            var dtos = _mapper.Map<IEnumerable<ProductSubcategoryDTO>>(entities);
            return Ok(dtos);
        }

        // POST: api/ProductSubcategory
        [HttpPost]
        public async Task<ActionResult<ProductSubcategoryDTO>> Add(ProductSubcategoryDTO dto)
        {
            var entity = _mapper.Map<ProductSubcategory>(dto);
            await _repository.AddAsync(entity);
            var resultDto = _mapper.Map<ProductSubcategoryDTO>(entity);
            return CreatedAtAction(nameof(GetById), new { id = resultDto.ProductSubcategoryId }, resultDto);
        }

        // PUT: api/ProductSubcategory/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ProductSubcategoryDTO dto)
        {
            if (id != dto.ProductSubcategoryId) return BadRequest("ID mismatch");
            var entity = _mapper.Map<ProductSubcategory>(dto);
            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        // DELETE: api/ProductSubcategory/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
