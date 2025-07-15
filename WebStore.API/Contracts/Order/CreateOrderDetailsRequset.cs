namespace WebStore.API.Contracts.Order;

public record CreateOrderDetailsRequset
	(
	 int MenuItemId,
     int Quantity,
	 string ItemName,
	 double Price
	);