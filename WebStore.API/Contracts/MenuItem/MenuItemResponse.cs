namespace WebStore.API.Contracts.MenuItem;

public record MenuItemResponse
(
	 int Id,
	 string Name,
	 string Description,
	 string SpecialTag,
	 string Category,
	 double Price
	);
