using AdventureWorks.DTO;
using AdventureWorks.Model.Domain.Production;
using AdventureWorks.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitMeasureController : ControllerBase
    {
        private readonly IUnitMeasureRepository _repository;
        private readonly IMapper _mapper;

        public UnitMeasureController(IUnitMeasureRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? filter = null, string sortBy = "Name", bool isDescending = false)
        {
            var query = await _repository.GetAllAsync();

            if (!string.IsNullOrEmpty(filter))
                query = query.Where(u => u.Name.Contains(filter));

            query = isDescending
                ? query.OrderByDescending(u => EF.Property<object>(u, sortBy))
                : query.OrderBy(u => EF.Property<object>(u, sortBy));

            return Ok(query);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UnitMeasureDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = _mapper.Map<UnitMeasure>(dto);
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.UnitMeasureCode }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UnitMeasureDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            await _repository.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
