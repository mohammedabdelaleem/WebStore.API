namespace WebStore.API.Contracts.Order;

public class CreateOrderRequsetValidator : AbstractValidator<CreateOrderRequset>
{
	private readonly AppDbContext _context;

	public CreateOrderRequsetValidator(AppDbContext context)
	{
		_context = context;


		RuleFor(x => x.PickupName).NotEmpty().MaximumLength(225);
		RuleFor(x => x.PickupPhoneNumber).NotEmpty()
			 .Matches(RegexPatterns.Number)
			 .WithMessage("Only numbers are allowed.")
			 .MaximumLength(20);


		RuleFor(x => x.PickupEmail).NotEmpty().EmailAddress().MaximumLength(225);
		//RuleFor(x => x.Status).NotEmpty();
		RuleFor(x => x.UserId).NotEmpty();
		RuleFor(x => x.StripePaymentIntentId).NotEmpty();
		RuleFor(x => x.TotalItems).NotEmpty();
		RuleFor(x => x.OrderTotal).NotEmpty();


		RuleFor(x => x.OrderDetailsCreateDTO)
			.NotEmpty();

		RuleFor(x => x.OrderDetailsCreateDTO)
			.Must(o=>o.Count()>=1)
			.WithMessage("At Least One Order Details")
			.When(x => x.OrderDetailsCreateDTO is not null);


		RuleFor(x=>x.OrderDetailsCreateDTO)
			.Must(o=> o.Count() == o.DistinctBy(x=>x.ItemName).Count() )
			.WithMessage("Only Distinct Order Details....")
			.When(x=> x.OrderDetailsCreateDTO is not null);

		RuleForEach(x=>x.OrderDetailsCreateDTO)
			.SetInheritanceValidator(x=>x.Add(new CreateOrderDetailsRequsetValidator(_context)));
	}
}
