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
    public class WorkOrderRoutingController : ControllerBase
    {
        private readonly IWorkOrderRoutingRepository _repository;
        private readonly IMapper _mapper;

        public WorkOrderRoutingController(IWorkOrderRoutingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int? workOrderId = null, string? sortBy = "ScheduledStartDate", bool isDescending = false)
        {
            var query = await _repository.GetAllAsync();

            if (workOrderId.HasValue)
                query = query.Where(w => w.WorkOrderId == workOrderId.Value);

            query = isDescending
                ? query.OrderByDescending(w => EF.Property<object>(w, sortBy))
                : query.OrderBy(w => EF.Property<object>(w, sortBy));

            return Ok(query);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int workOrderId, short operationSequence)
        {
            var entity = await _repository.GetByIdAsync(workOrderId, operationSequence);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] WorkOrderRoutingDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var entity = _mapper.Map<WorkOrderRouting>(dto);
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.WorkOrderId }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int workOrderId, short operationSequence, [FromBody] WorkOrderRoutingDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var existing = await _repository.GetByIdAsync(workOrderId, operationSequence);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            await _repository.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int workOrderId, short operationSequence)
        {
            await _repository.DeleteAsync(workOrderId,operationSequence);
            return NoContent();
        }
    }
}

