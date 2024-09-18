using Asp.Versioning;
using SchoolManagmen.Abstractions.Consts;
using SchoolManagmen.Authentication.Filters;
using SchoolManagmen.Contracts.Roles;

namespace SchoolManagmen.Controllers;
[ApiVersion(1)]
[ApiVersion(2)]
[Route("api/[controller]")]
[ApiController]
public class RolesController(IRoleService roleService) : ControllerBase
{
    private readonly IRoleService _roleService = roleService;

    [HttpGet("")]
    [HasPermission(Permissions.GetRoles)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var roles = await _roleService.GetAllAsync(cancellationToken);

        return Ok(roles);
    }
    [HttpGet("{id}")]
    [HasPermission(Permissions.GetRoles)]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        var result = await _roleService.GetAsync(id);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }


    [HttpPost("")]
    [HasPermission(Permissions.AddRoles)]
    public async Task<IActionResult> Add([FromBody] RoleRequest request)
    {
        var result = await _roleService.AddAsync(request);

        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { result.Value.Id }, result.Value) : result.ToProblem();
    }


    [HttpPut("{id}")]
    [HasPermission(Permissions.UpdateRoles)]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] RoleRequest request)
    {
        var result = await _roleService.UpdateAsync(id, request);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

   
}