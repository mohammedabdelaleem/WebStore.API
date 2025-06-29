using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;
using WebStore.API.Entities;

namespace WebStore.API.Persistance;

public class AppDbContext(
	DbContextOptions<AppDbContext> options) :
	IdentityDbContext<ApplicationUser,ApplicationRole,string>(options)
{

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
