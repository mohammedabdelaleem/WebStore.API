namespace WebStore.API.Errors;


public record OrderErrors
{

	public static readonly Error NotFound
		= new Error("Order.NotFound", "Order With Given Id Not Found", StatusCodes.Status404NotFound);

	public static readonly Error Empty
			= new Error("Order.Empty", "No Orders Are Found", StatusCodes.Status404NotFound);

	public static readonly Error SavingError
		= new Error("Order.SavingError", "Error While Savnig Order ", StatusCodes.Status500InternalServerError);

	public static readonly Error InvalidInfo
		= new Error("Order.InvalidInfo", "Recheck The Info", StatusCodes.Status400BadRequest);

}
