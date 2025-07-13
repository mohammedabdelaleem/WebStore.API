namespace WebStore.API.Services;

public interface IShoppingCartService
{
	public Task<Result> AddOrUpdateItemInCart(string userId, int menuItemId, int updatedQuantityBy, CancellationToken cancellationToken = default);

}
