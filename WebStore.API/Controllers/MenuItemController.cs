using WebStore.API.Contracts.File;
using WebStore.API.Contracts.MenuItem;

namespace WebStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MenuItemController(IMenuItemService menuItemService) : ControllerBase
{
	private readonly IMenuItemService _menuItemService = menuItemService;

	[HttpGet]
	public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
	{
		var result = await _menuItemService.GetAll(cancellationToken);
		return result.IsSuccess ?  Ok(result.Value) : result.ToProblem();
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> Get([FromRoute] int id,CancellationToken cancellationToken = default)
	{
		var result = await _menuItemService.Get(id, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpPost]
	public async Task<IActionResult> Add([FromForm] CreateMenuItemRequest request , CancellationToken cancellationToken = default)
	{
		var result = await _menuItemService.Add(request, cancellationToken);
		return result.IsSuccess ? CreatedAtAction(nameof(Get), new {id=result.Value.Id}, result.Value) : result.ToProblem();
	}
}
