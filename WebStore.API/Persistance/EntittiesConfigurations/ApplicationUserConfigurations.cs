using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebStore.API.Abstractions.Consts;

namespace WebStore.API.Persistance.EntittiesConfigurations;

public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
{
	public void Configure(EntityTypeBuilder<ApplicationUser> builder)
	{

		builder.Property(x => x.FirstName)
					.HasMaxLength(225);

		builder.Property(x => x.LastName)
					.HasMaxLength(225);




		builder
			.OwnsMany(x => x.RefreshTokens)
			.ToTable("RefreshTokens") // instead of RefreshToken
			.WithOwner()
			.HasForeignKey("UserId"); // instead of ApplicaionUserId // after migraiton



		builder.HasData(new ApplicationUser
		{
			Id = DefaultUsers.Admin.Id,
			FirstName = DefaultUsers.Admin.FirstName,
			LastName = DefaultUsers.Admin.LastName,
			Email = DefaultUsers.Admin.Email,
			NormalizedEmail = DefaultUsers.Admin.Email.ToUpper(),
			UserName = DefaultUsers.Admin.UserName,
			NormalizedUserName = DefaultUsers.Admin.UserName.ToUpper(),
			ConcurrencyStamp = DefaultUsers.Admin.ConcurrencyStamp,
			EmailConfirmed = true,
			SecurityStamp = DefaultUsers.Admin.SecurityStamp,
			PasswordHash = DefaultUsers.Admin.PasswordHash,
		});

	}
}
