
using Asp.Versioning;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagmen.Abstractions.Consts;
using SchoolManagmen.Authentication.Filters;
using SchoolManagmen.Contracts.User;
using SchoolManagmen.Service;

namespace SchoolManagmen.Controllers
{
    [ApiVersion(1)]
    [ApiVersion(2)]
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting(RateLimiters.Concurrency)]

    public class UsersController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet("")]
        [HasPermission(Permissions.GetUsers)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _userService.GetAllAsync(cancellationToken));
        }
        [HttpGet("{id}")]
        [HasPermission(Permissions.GetUsers)]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var result = await _userService.GetAsync(id);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPost("")]
        [HasPermission(Permissions.AddUsers)]
        public async Task<IActionResult> Add([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _userService.AddAsync(request, cancellationToken);

            return result.IsSuccess ? CreatedAtAction(nameof(Get), new { result.Value.Id }, result.Value) : result.ToProblem();
        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.UpdateUsers)]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _userService.UpdateAsync(id, request, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

    }
}
