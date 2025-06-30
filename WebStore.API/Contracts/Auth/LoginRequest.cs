namespace WebStore.API.Contracts.Auth;

public record LoginRequest(
		string Email,
		string Password
	);