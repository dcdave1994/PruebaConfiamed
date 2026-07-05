using Microsoft.AspNetCore.Mvc;
using PruebaConfiamed.DTOs.Requests;
using PruebaConfiamed.Interfaces;

namespace PruebaConfiamed.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.GetAllAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _service.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            var user = await _service.CreateAsync(request);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }
    }
}