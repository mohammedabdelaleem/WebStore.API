namespace WebStore.API.Contracts.Order;

public class UpateOrderRequsetValidator : AbstractValidator<UpdateOrderRequset>
{
	private readonly AppDbContext _context;

	public UpateOrderRequsetValidator(AppDbContext context)
	{
		_context = context;


		RuleFor(x => x.PickupName).NotEmpty().MaximumLength(225);
		RuleFor(x => x.PickupPhoneNumber).NotEmpty()
			 .Matches(RegexPatterns.Number)
			 .WithMessage("Only numbers are allowed.")
			 .MaximumLength(20);


		RuleFor(x => x.PickupEmail).NotEmpty().EmailAddress().MaximumLength(225);
		RuleFor(x => x.StripePaymentIntentId).NotEmpty();


	}
}
