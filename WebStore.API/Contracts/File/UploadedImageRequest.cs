namespace WebStore.API.Contracts.File;

public record UploadedImageRequest
	(
		IFormFile Image
	);