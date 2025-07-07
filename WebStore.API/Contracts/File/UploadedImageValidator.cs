using WebStore.API.Contracts.CommonValidators;

namespace WebStore.API.Contracts.File;

public class UploadedImageValidator : AbstractValidator<UploadedImageRequest>
{
	public UploadedImageValidator()
	{
		RuleFor(x => x.Image)
			.SetValidator(new FileNameValidator())
			.SetValidator(new FileSizeValidator())
			.SetValidator(new AllowedSignaturesValidator());

	}
}
