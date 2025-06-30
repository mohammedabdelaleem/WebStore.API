using FluentValidation;

namespace WebStore.API.Contracts.Auth;

public class LoginRequestValidator : AbstractValidator<LoginRequest>	
{
	public LoginRequestValidator()
	{
		RuleFor(x => x.Email)
			.NotEmpty()
			.EmailAddress();


		RuleFor(x => x.Password)
			.NotEmpty(); 
		// matched not needed here you login  
			
	}
}
