namespace WebStore.API.Abstractions.Consts;

public static class RegexPatterns
{
	public const string Password = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$";
	public const string Number = @"^\d+$";


	// Allows alphanumerics, underscore, dash, dot, and space
	// Disallows: / \ : * ? " < > | and control characters
	public const string AllowedFileNameConvention = @"^(?!^(CON|PRN|AUX|NUL|COM\d|LPT\d)(\..*)?$)[^<>:\""/\\|?*\x00-\x1F]+$";

}