namespace WebStore.API.Controllerts;

[Route("api/[controller]")]
[ApiController]
public class ShoppingCartController(IShoppingCartService shoppingCartService) : ControllerBase
{
	private readonly IShoppingCartService _shoppingCartService = shoppingCartService;


	[HttpGet("{userId:guid}")]
	public async Task<IActionResult> Get([FromRoute]string userId, CancellationToken cancellationToken=default)
	{
		var result = await _shoppingCartService.GetShoppingCart(userId,cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}	



	[HttpPost]
	public async Task<IActionResult> AddOrUpdateItemInCart(CreateShoppingCartRequest request, CancellationToken cancellationToken = default)
	{
		var result = await _shoppingCartService.AddOrUpdateItemInCart(request, cancellationToken);
	
	   return  result.IsSuccess ? Created() : result.ToProblem();
	}
}
