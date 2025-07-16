using Stripe;

namespace WebStore.API.Services;

public class PaymentService(
	AppDbContext context,
	IConfiguration configuration
	) : IPaymentService
{
	private readonly AppDbContext _context = context;
	private readonly IConfiguration _configuration = configuration;

	public async Task<Result<ShoppingCart>> MakePaymentAsync (string userId , CancellationToken cancellationToken = default)
	{
		var shoppingCart = await _context.ShoppingCarts
						.Include(s=>s.CartItems)
						.ThenInclude(c=>c.MenuItem)
						.FirstOrDefaultAsync(x=>x.UserId == userId , cancellationToken);


		if (shoppingCart == null || shoppingCart.CartItems == null || shoppingCart.CartItems.Count() == 0)
			return Result.Failure<ShoppingCart>(ShoppingCartErrors.NotFound);

		#region Create Payment Intent 
		StripeConfiguration.ApiKey = _configuration.GetValue<string>("StripeSettings:SecretKey");
		shoppingCart.CartTotal = shoppingCart.CartItems.Sum(x=>x.Quantity * x.MenuItem.Price);

		var options = new PaymentIntentCreateOptions
		{
			Amount = (int)(shoppingCart.CartTotal *100),
			Currency = "usd",
			AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
			{
				Enabled = true,
			},
		};
		var service = new PaymentIntentService();
		PaymentIntent response = await service.CreateAsync(options, cancellationToken:cancellationToken);

		shoppingCart.StripePaymentIntentId = response.Id;
		shoppingCart.ClientSecret = response.ClientSecret;
		#endregion

		return Result.Success(shoppingCart);

	}
}
