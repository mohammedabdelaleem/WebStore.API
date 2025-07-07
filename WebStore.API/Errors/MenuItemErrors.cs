namespace WebStore.API.Errors;


public record MenuItemErrors
{

	public static readonly Error NotFound
		= new Error("MenuItem.NotFound", "MenuItem With Given Id Not Found", StatusCodes.Status404NotFound);


	public static readonly Error MenuItemDuplicated
		= new Error("MenuItem.Duplicated", "MenuItem With The Same Name Is Already Exists", StatusCodes.Status409Conflict);


	public static readonly Error SavingError
		= new Error("MenuItem.SavingError", "Error While Savnig MenuItem ", StatusCodes.Status500InternalServerError);



}
