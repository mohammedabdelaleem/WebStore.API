namespace WebStore.API.Contracts.CommonValidators;

public class FileNameValidator : AbstractValidator<IFormFile>
{
	public FileNameValidator()
	{
		RuleFor(x => x.FileName)
		.NotEmpty()
		.Matches(RegexPatterns.AllowedFileNameConvention)
		.WithMessage($"Invalid File Name.");
	}
}
