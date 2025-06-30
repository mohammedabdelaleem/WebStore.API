using Microsoft.AspNetCore.Identity;

namespace WebStore.API.Entities;

public class ApplicationUser : IdentityUser
{
	public ApplicationUser()
	{
		Id = Guid.CreateVersion7().ToString();
		ConcurrencyStamp = Guid.CreateVersion7().ToString();
	}

	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public bool IsDisabled { get; set; }

	// owned entity , we don't need to add its table at AppDbContextAdd commentMore actions
	// it will generate automatically
	public List<RefreshToken> RefreshTokens { get; set; } = [];
}
