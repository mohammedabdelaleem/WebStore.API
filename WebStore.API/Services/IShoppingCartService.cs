using WebStore.API.Contracts.ShoppingCart;

namespace WebStore.API.Services;

public interface IShoppingCartService
{
	public Task<Result> AddOrUpdateItemInCart(CreateShoppingCartRequest request, CancellationToken cancellationToken = default);

}
