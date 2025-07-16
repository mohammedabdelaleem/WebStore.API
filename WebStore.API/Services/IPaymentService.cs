namespace WebStore.API.Services;

public interface IPaymentService
{
	Task<Result<ShoppingCart>> MakePaymentAsync(string userId, CancellationToken cancellationToken = default);
}
