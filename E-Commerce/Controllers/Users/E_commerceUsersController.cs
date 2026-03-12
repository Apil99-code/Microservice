using E_commerce.Dtos.users;
using E_commerce.Features.Products.Command.Users;
using E_commerce.Features.Products.Quaries.Users;
using E_commerce.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class E_commerceUsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public E_commerceUsersController(IUserInterface userService, IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDtos createUserDtos)
        {
            var command = new CreateUsersCommand
            {
                Name = createUserDtos.Name,
                Email = createUserDtos.Email,
                Password = createUserDtos.Password,
                Role = createUserDtos.Role
            };

            await _mediator.Send(command);
            return Ok(new { Message = "User created successfully." });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var command = new DeleteUsersCommand { Id = id };
            await _mediator.Send(command);
            return Ok(new { Message = "User deleted successfully." });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] CreateUserDtos createUserDtos)
        {
            var command = new UpdateUsersCommand
            {
                Id = id,
                Name = createUserDtos.Name,
                Email = createUserDtos.Email,
                Password = createUserDtos.Password,
                Role = createUserDtos.Role
            };

            await _mediator.Send(command);
            return Ok(new { Message = "User updated successfully." });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var query = new GetUserByIDQuaries { Id = id };
            var user = await _mediator.Send(query);
            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new GetUserQuaries();
            var users = await _mediator.Send(query);
            return Ok(users);
        }

    }
}
