
namespace WebStore.API.Authentication;


public class JWTOptions
{

		[Required]
		public string Key { get; init; } = string.Empty; // init : setable only during initialization - Read only After Initialization - immutable

		[Required]
		public string Issuer { get; init; } = string.Empty;

		[Required]
		public string Audience { get; init; } = string.Empty;

		[Required, Range(1, int.MaxValue, ErrorMessage = "Invalid Expiry Minutes !!!!")]
		public int ExpiryMinutes { get; init; }

}

