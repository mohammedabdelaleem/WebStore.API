namespace WebStore.API.Contracts.Auth;

public record ResetPasswordRequest(
	string Email,
	 string Code,
	 string NewPassword
	);