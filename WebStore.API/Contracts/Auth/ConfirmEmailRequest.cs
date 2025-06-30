namespace WebStore.API.Contracts.Auth;
public record ConfirmEmailRequest(
	string UserId,
	string Code
	);
