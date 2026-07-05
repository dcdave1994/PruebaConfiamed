using Microsoft.AspNetCore.Mvc;
using PruebaConfiamed.DTOs.Requests;
using PruebaConfiamed.Interfaces;

namespace PruebaConfiamed.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkItemsController : ControllerBase
    {
        private readonly IWorkItemService _service;

        public WorkItemsController(IWorkItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var workItems = await _service.GetAllAsync();
            return Ok(workItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var workItem = await _service.GetByIdAsync(id);

            if (workItem == null)
                return NotFound();

            return Ok(workItem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWorkItemRequest request)
        {
            var workItem = await _service.CreateAsync(request);

            return CreatedAtAction(nameof(GetById), new { id = workItem.Id }, workItem);
        }
    }
}