namespace WebStore.API.Contracts.Order;

public class CreateOrderDetailsRequsetValidator: AbstractValidator<CreateOrderDetailsRequset>
{
	private readonly AppDbContext _context ;

	public CreateOrderDetailsRequsetValidator(AppDbContext context)
	{
		_context = context;

		RuleFor(x=>x.MenuItemId).NotEmpty()
			.Must(ValidateMenuItemId)
			.WithMessage("Invalid Menu Item Id");

		RuleFor(x => x.Quantity).NotEmpty();
		RuleFor(x => x.ItemName).NotEmpty();
		RuleFor(x => x.Price).NotEmpty();
	}

	private bool ValidateMenuItemId(int menuItemId)
	=> _context.MenuItems.Any(x=>x.Id == menuItemId);
}
