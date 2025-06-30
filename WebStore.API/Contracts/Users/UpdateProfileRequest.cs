namespace WebStore.API.Contracts.Users;

public record UpdateProfileRequest(
	string FirstName,
	string LastName
	);
