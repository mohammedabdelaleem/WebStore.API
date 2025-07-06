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



}
