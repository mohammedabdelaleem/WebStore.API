using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.API.Contracts.ShoppingCart;

namespace WebStore.API.Controllerts;
[Route("api/[controller]")]
[ApiController]
public class ShoppingCateController(IShoppingCartService shoppingCartService) : ControllerBase
{
	private readonly IShoppingCartService _shoppingCartService = shoppingCartService;

	[HttpPost]
	public async Task<IActionResult> AddOrUpdateItemInCart(CreateShoppingCartRequest request, CancellationToken cancellationToken = default)
	{
		var result = await _shoppingCartService.AddOrUpdateItemInCart(request, cancellationToken);
	
	   return  result.IsSuccess ? Created() : result.ToProblem();
	}
}
