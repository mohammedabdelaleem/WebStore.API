namespace WebStore.API.Contracts.ShoppingCart;

public record CreateShoppingCartRequest
(
		string UserId, 
		int MenuItemId,
		int UpdatedQuantityBy
	);