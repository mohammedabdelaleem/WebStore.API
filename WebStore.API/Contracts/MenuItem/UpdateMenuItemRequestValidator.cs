using WebStore.API.Contracts.CommonValidators;
using WebStore.API.Settings;

namespace WebStore.API.Contracts.MenuItem;

public class UpdateMenuItemRequestValidator : AbstractValidator<UpdateMenuItemRequest>
{
	public UpdateMenuItemRequestValidator()
	{
		RuleFor(x=>x.Name)
			.NotEmpty()
			.Length(3,225);

		RuleFor(x => x.Description)
			.NotEmpty()
			.Length(3, 1000);


		RuleFor(x => x.SpecialTag)
			.MinimumLength(5);

		RuleFor(x => x.Category)
			.NotEmpty()
			.MinimumLength(3);

		RuleFor(x => x.Price)
			.NotEmpty()
			.GreaterThanOrEqualTo(5);


		RuleFor(x => x.Image)
			.SetValidator(new FileNameValidator())
			.SetValidator(new FileSizeValidator())
			.SetValidator(new AllowedSignaturesValidator());
	}
}
