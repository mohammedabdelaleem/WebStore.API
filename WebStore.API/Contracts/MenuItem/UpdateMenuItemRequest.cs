namespace WebStore.API.Contracts.MenuItem;

public record UpdateMenuItemRequest
(
	 int Id,
	 string Name,
	 string Description,
	 string SpecialTag,
	 string Category,
	 double Price,
	 IFormFile Image
);