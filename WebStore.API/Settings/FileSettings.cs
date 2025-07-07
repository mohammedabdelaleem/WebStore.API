namespace WebStore.API.Settings;

public static class FileSettings
{
	public const int MaxFileSizeInMB = 1;
	public const int MaxFileSizeInByte =  MaxFileSizeInMB * 1024*1024;

	public static readonly string[] AllowedImageSignatures =
{

	"FF-D8",         // JPEG,JPG
    "89-50",      // PNG
    //"47-49",      // GIF
    //"42-4D",            // BMP
    //"49-49",      // TIFF (little endian)
    //"4D-4D",      // TIFF (big endian)
    //"52-49"       // WEBP (requires deeper validation)
};

	public static string ExtractFileSignature(IFormFile file, bool videoStream = false)
	{
		// get file signature 
		// each content type has unique singnature ==> you can get it from the first 2 bytes
		// even if you change the original file exension  ex: from .png to .jpg the signature will return the [.png signature]

		BinaryReader binary = new(file.OpenReadStream());
		var bytes = binary.ReadBytes(videoStream ? 4 : 2);
		var fileSequenceHex = BitConverter.ToString(bytes);

		return fileSequenceHex;
	}
}
