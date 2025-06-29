using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;

namespace WebStore.API.Persistance.EntittiesConfigurations;

public class UsersRolesConfigurations : IEntityTypeConfiguration<IdentityUserRole<string>>
{

	public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
	{
		builder.HasData(
					new IdentityUserRole<string>
					{
						UserId = DefaultUsers.Admin.Id,
						RoleId = DefaultRoles.Admin.Id,
					}
					);
	}
}
