namespace WebStore.API.Contracts.Order;

public class CreateOrderDetailsRequsetValidator : AbstractValidator<CreateOrderDetailsRequset>
{
	public CreateOrderDetailsRequsetValidator()
	{
		RuleFor(x=>x.MenuItemId).NotEmpty();
		RuleFor(x => x.Quantity).NotEmpty();
		RuleFor(x => x.ItemName).NotEmpty();
		RuleFor(x => x.Price).NotEmpty();
	}
}
