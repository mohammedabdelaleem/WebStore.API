namespace WebStore.API.Contracts.CommonValidators;

public class AllowedSignaturesValidator : AbstractValidator<IFormFile>
{
	public AllowedSignaturesValidator()
	{
		RuleFor(x => x)
		.Must((request, context) =>
		{
			var fileSignature = FileSettings.ExtractFileSignature(request);
			return (FileSettings.AllowedImageSignatures.Contains(fileSignature));
		})
		.WithMessage($"Invalid File Content Type.")
		.When(x => x is not null);

	}
}
