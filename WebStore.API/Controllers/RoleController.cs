﻿
namespace WebStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiVersion(1)]
[ApiVersion(2)]
public class RoleController(IRoleService roleService) : ControllerBase
{
	private readonly IRoleService _roleService = roleService;

	[HttpGet]
	[HasPermission(Permissions.GetRoles)]
	public async Task<IActionResult> GetAll([FromQuery] bool includeDisabled, CancellationToken cancellationToken = default)
	{

		return Ok(await _roleService.GetAllAsync(includeDisabled, cancellationToken));
	}

	[HttpGet("{id}")]
	[HasPermission(Permissions.GetRoles)]
	public async Task<IActionResult> Get([FromRoute] string id)
	{
		var result = await _roleService.GetAsync(id);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpPost]
	[HasPermission(Permissions.AddRole)]
	public async Task<IActionResult> Add([FromBody] RoleRequest request, CancellationToken cancellationToken = default)
	{
		var result = await _roleService.AddAsync(request, cancellationToken);
		return result.IsSuccess ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value) : result.ToProblem();
	}

	[HttpPut("{id}")]
	[HasPermission(Permissions.UpdateRole)]
	public async Task<IActionResult> Update([FromRoute] string id, [FromBody] RoleRequest request, CancellationToken cancellationToken = default)
	{
		var result = await _roleService.UpdateAsync(id, request, cancellationToken);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}


	[HttpPut("{id}/toggle-status")]
	[HasPermission(Permissions.UpdateRole)]
	public async Task<IActionResult> ToggleStatus([FromRoute] string id, CancellationToken cancellationToken = default)
	{
		var result = await _roleService.ToggleStatusAsync(id, cancellationToken);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}
}
