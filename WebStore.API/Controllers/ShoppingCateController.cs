using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.API.Controllerts;
[Route("api/[controller]")]
[ApiController]
public class ShoppingCateController(IShoppingCartService shoppingCartService) : ControllerBase
{
	private readonly IShoppingCartService _shoppingCartService = shoppingCartService;

	[HttpPost]
	public async Task<IActionResult> AddOrUpdateItemInCart(string userId, int menuItemId, int updatedQuantityBy, CancellationToken cancellationToken = default)
	{
		var result = await _shoppingCartService.AddOrUpdateItemInCart(userId, menuItemId, updatedQuantityBy, cancellationToken);
	
	   return  result.IsSuccess ? Created() : result.ToProblem();
	}
}
