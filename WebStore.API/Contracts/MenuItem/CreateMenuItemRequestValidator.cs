using WebStore.API.Contracts.CommonValidators;
using WebStore.API.Settings;

namespace WebStore.API.Contracts.MenuItem;

public class CreateMenuItemRequestValidator : AbstractValidator<CreateMenuItemRequest>
{
	public CreateMenuItemRequestValidator()
	{
		RuleFor(x=>x.Name)
			.NotEmpty()
			.Length(3,225);

		RuleFor(x => x.Description)
			.NotEmpty()
			.Length(3, 1000);


		RuleFor(x => x.SpecialTag)
			.Length(50);

		RuleFor(x => x.Category)
			.NotEmpty()
			.Length(50);

		RuleFor(x => x.Price)
			.NotEmpty()
			.GreaterThanOrEqualTo(5);

		RuleFor(x => x.Image)
			.SetValidator(new FileNameValidator())
			.SetValidator(new FileSizeValidator())
			.SetValidator(new AllowedSignaturesValidator());


	}
}
