namespace WebStore.API.Contracts.ShoppingCart;

public class CreateShoppingCartRequestValidator : AbstractValidator<CreateShoppingCartRequest>
{
	public CreateShoppingCartRequestValidator()
	{
		RuleFor(x=>x.UserId).NotEmpty();

		RuleFor(x => x.MenuItemId).NotEmpty();

		RuleFor(x => x.UpdatedQuantityBy).NotEmpty();


	}
}
