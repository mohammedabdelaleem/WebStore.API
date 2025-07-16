using WebStore.API.Contracts.Order;

namespace WebStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class OrderController(IOrderService orderService) : ControllerBase
{
	private readonly IOrderService _orderService = orderService;

	[HttpGet]
	public async Task<IActionResult> Index(CancellationToken cancellationToken=default)
	{
		var result = await _orderService.GetAll(cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpGet("userId/{userId:guid}")]
	public async Task<IActionResult> GetAllForUser(string userId,CancellationToken cancellationToken = default)
	{
		var result = await _orderService.GetAllForUser(userId, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpGet("{orderId}")]
	public async Task<IActionResult> Get(int orderId, CancellationToken cancellationToken = default)
	{
		var result = await _orderService.Get(orderId, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpPost]
	public async Task<IActionResult> Add([FromBody] CreateOrderRequset requset, CancellationToken cancellationToken = default)
	{
		var result = await _orderService.Add(requset, cancellationToken);

		return result.IsSuccess ? CreatedAtAction(nameof(Get), new { orderId = result.Value.Id }, result.Value) : result.ToProblem();
	}


	[HttpPut("{id}")]
	public async Task<IActionResult> Update([FromRoute] int id ,[FromBody] UpdateOrderRequset requset, CancellationToken cancellationToken = default)
	{
		var result = await _orderService.Update(id,requset, cancellationToken);

		return result.IsSuccess ? NoContent() : result.ToProblem();
	}
}
