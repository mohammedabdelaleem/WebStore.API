namespace WebStore.API.Errors;


public record ShoppingCartErrors
{

	public static readonly Error NotFound
		= new Error("ShoppingCart.NotFound", "ShoppingCart With Given Id Not Found", StatusCodes.Status404NotFound);


	public static readonly Error ShoppingCartDuplicated
		= new Error("ShoppingCart.Duplicated", "ShoppingCart With The Same Name Is Already Exists", StatusCodes.Status409Conflict);


	public static readonly Error SavingError
		= new Error("ShoppingCart.SavingError", "Error While Savnig ShoppingCart ", StatusCodes.Status500InternalServerError);

	public static readonly Error InvalidInfo
		= new Error("ShoppingCart.InvalidInfo", "Recheck The Info", StatusCodes.Status400BadRequest);


}
