using WebStore.API.Persistance;

namespace WebStore.API;

public static class DependencyInjection
{
	public static IServiceCollection AddDependancies(this IServiceCollection services, IConfiguration configuration)
	{
		var constr = configuration.GetConnectionString("constr") ??
		throw new InvalidOperationException("There is no Connection String For The 'DefaultConStr' Key ");


		services.AddControllers();
		services.AddOpenApi();

		services.AddDataBaseConfig(configuration);


		return services;
	}

	public static IServiceCollection AddDataBaseConfig(this IServiceCollection services, IConfiguration configuration)
	{
		var constr = configuration.GetConnectionString("constr") ??
throw new InvalidOperationException("There is no Connection String For The 'DefaultConStr' Key ");


		services.AddDbContext<AppDbContext>(options =>
		options.UseSqlServer(constr));

		return services;
	}
}
