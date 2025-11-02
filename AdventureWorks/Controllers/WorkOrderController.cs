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
    public class WorkOrderController : ControllerBase
    {
        private readonly IWorkOrderRepository _repository;
        private readonly IMapper _mapper;

        public WorkOrderController(IWorkOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 20)
        {
            var query = await _repository.GetAllAsync();
            var paged = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return Ok(paged);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] WorkOrderDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var entity = _mapper.Map<WorkOrder>(dto);
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.WorkOrderId }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] WorkOrderDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            await _repository.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}

