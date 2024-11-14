using MediatR;
using Microsoft.AspNetCore.Mvc;
using Usermanagement.Application.Command.UserDetails;
using Usermanagement.Application.Dtos;

namespace Usermanagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="createUserDto"></param>
        /// <returns></returns>
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            CreateUserCommand command = new CreateUserCommand(createUserDto);
            return Ok(await _mediator.Send(command));
        }
        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {

            return Ok();
        }
    }
}
