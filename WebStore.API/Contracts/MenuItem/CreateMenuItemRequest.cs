namespace WebStore.API.Contracts.MenuItem;

public record CreateMenuItemRequest(
	 string Name ,
	 string Description ,
	 string SpecialTag ,
	 string Category ,
	 double Price ,
	 IFormFile Image
	);