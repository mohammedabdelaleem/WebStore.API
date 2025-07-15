using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebStore.API.Persistance.EntittiesConfigurations;

public class ShoppingCartConfigurations : IEntityTypeConfiguration<ShoppingCart>
{
	public void Configure(EntityTypeBuilder<ShoppingCart> builder)
	{

		builder.HasKey(x => x.Id);

		builder.Property(x=>x.UserId)
			.HasMaxLength(450)
			.IsRequired();


		// Configure one-to-one relationship with ApplicationUser (no nav props)
		builder.HasOne<ApplicationUser>()
			.WithOne()
			.HasForeignKey<ShoppingCart>(x => x.UserId)
			.IsRequired();

		//enforce uniqueness
		builder.HasIndex(c => c.UserId)
			   .IsUnique();

	}
}
