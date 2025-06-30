using FluentValidation;

namespace WebStore.API.Contracts.Auth;

public class ForgetPasswordRequsetValidator : AbstractValidator<ForgetPasswordRequset>
{
	public ForgetPasswordRequsetValidator()
	{
		RuleFor(x => x.Email)
			.NotEmpty()
			.EmailAddress();
	}
}