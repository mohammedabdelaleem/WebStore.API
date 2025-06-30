namespace WebStore.API.Contracts.Roles;

public record RoleRequest
(
		string Name,
	List<string> Permissions
	);
