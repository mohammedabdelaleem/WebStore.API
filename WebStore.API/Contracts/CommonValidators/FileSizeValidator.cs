namespace WebStore.API.Contracts.CommonValidators;

public class FileSizeValidator : AbstractValidator<IFormFile>
{
	public FileSizeValidator()
	{
		RuleFor(x => x)
		.Must((request, context) =>
		{
			return (request.Length <= FileSettings.MaxFileSizeInByte);
		})
		.WithMessage($"Max File Size Is {FileSettings.MaxFileSizeInMB} MB.")
		.When(x => x is not null);

	}
}
