using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace WebStore.API.Persistance;

public class AppDbContext(
	DbContextOptions<AppDbContext> options) :
	IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
{


	public DbSet<MenuItem> MenuItems { get; set; }
	public DbSet<ShoppingCart> ShoppingCarts { get; set; }
	public DbSet<CartItem> CartItems { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


		// change on delete behaviour
		var cascadeFKs = builder.Model
			.GetEntityTypes().SelectMany(e => e.GetForeignKeys())
			.Where(fk => fk.DeleteBehavior == DeleteBehavior.Cascade && !fk.IsOwnership);

		foreach (var relationship in cascadeFKs)
			relationship.DeleteBehavior = DeleteBehavior.Restrict;

		base.OnModelCreating(builder);
	}
}
