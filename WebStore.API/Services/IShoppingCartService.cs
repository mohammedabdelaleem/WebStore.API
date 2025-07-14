using WebStore.API.Contracts.ShoppingCart;

namespace WebStore.API.Services;

public interface IShoppingCartService
{
	 Task<Result> AddOrUpdateItemInCart(CreateShoppingCartRequest request, CancellationToken cancellationToken = default);
	Task<Result<ShoppingCart>> GetShoppingCart(string userId, CancellationToken cancellationToken = default);
}
