namespace WebStore.API.Authentication;

/*
 [1] send user with its roles and permissions  ====> to get Token
 [2] send token ===> validate it and return userId
*/

public interface IJWTProvider
{
	(string token, int expiresIn) GenerateToken(ApplicationUser user, IEnumerable<string> roles, IEnumerable<string> permissions);


	// Check incomming jwt token ==> if ok return user id from its claims
	// ? because may be the incomming jwt is not valid

	//return user id
	string? ValidateTokenAndGetUserId(string token);
}