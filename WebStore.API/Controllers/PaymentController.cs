namespace WebStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController(IPaymentService paymentService) : ControllerBase
{
	private readonly IPaymentService _paymentService = paymentService;

	[HttpPost("{userId}")]
	 public async Task<IActionResult> CreatePayment(string userId, CancellationToken cancellationToken=default)
	  {
		var result = await _paymentService.MakePaymentAsync(userId, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	  }
}
