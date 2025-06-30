using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using FluentValidation;
using Hangfire;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Reflection;
using System.Text;
using WebStore.API.Authentication;
using WebStore.API.OpenApiTransformers;
using WebStore.API.Persistance;
using WebStore.API.Services;
using WebStore.API.Settings;

namespace WebStore.API;

public static class DependencyInjection
{
	public static IServiceCollection AddDependancies(this IServiceCollection services, IConfiguration configuration)
	{
		var constr = configuration.GetConnectionString("constr") ??
		throw new InvalidOperationException("There is no Connection String For The 'DefaultConStr' Key ");


		services.AddControllers();
		

		services.AddAuthConfig(configuration);

		services.AddTransient<IEmailSender, EmailService>();

		services
			.AddMappsterrConfig()
			.AddFluentValidationConfig()
			.AddDataBaseConfig(configuration);

		services.AddExceptionHandler<GlobalExceptionHandler>();
		services.AddProblemDetails();

		services.AddBackgroundJobsConfig(configuration);


		services.AddHttpContextAccessor();
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<IRoleService, RoleService>();


		services.AddOptions<MailSettings>()
			.BindConfiguration(nameof(MailSettings))
			.ValidateDataAnnotations()
			.ValidateOnStart();

		services.AddApiVersioning(options =>
		{
			options.DefaultApiVersion = new ApiVersion(1);
			options.AssumeDefaultVersionWhenUnspecified = true;
			options.ReportApiVersions = true;

			options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
		})
			.AddApiExplorer(options =>
			{
				options.GroupNameFormat = "'v'V";
				options.SubstituteApiVersionInUrl = true;
			});

		services
			.AddEndpointsApiExplorer()
			.AddOpenApiServices();

		return services;
	}


	private static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddSingleton<IJWTProvider, JWTProvider>();
		services.AddScoped<IAuthService, AuthService>();

		services.AddIdentity<ApplicationUser, ApplicationRole>()
			.AddEntityFrameworkStores<AppDbContext>()
			.AddDefaultTokenProviders();


		//services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();
		//services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();


		// Configurations 
		//	services.Configure<JWTOptions>(configuration.GetSection(JWTOptions.SectionName));
		services.AddOptions<JWTOptions>()
				.BindConfiguration(nameof(JWTOptions))
				.ValidateDataAnnotations()
				.ValidateOnStart();

		var jwtSettings = configuration.GetSection(nameof(JWTOptions)).Get<JWTOptions>();

		// when you need to use [Authorize] with Controller or Endpoint 
		// you don't need to tell them you use JwtBearer
		// you just add the configurations here

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
			.AddJwtBearer(options =>
			{
				options.SaveToken = true; // any time during request if you need to reach the token , you can do it easily 
				options.TokenValidationParameters = new TokenValidationParameters
				{
					// more validations more rubost key less hacking 
					ValidateIssuerSigningKey = true,
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings!.Key)),
					ValidIssuer = jwtSettings.Issuer,
					ValidAudience = jwtSettings.Audience,

				};
			});


		services.Configure<IdentityOptions>(options =>
		{
			options.Password.RequiredLength = 8;
			options.SignIn.RequireConfirmedEmail = true;
			options.User.RequireUniqueEmail = true;

			options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
			options.Lockout.MaxFailedAccessAttempts = 3;
			options.Lockout.AllowedForNewUsers = true;


		});


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

	private static IServiceCollection AddMappsterrConfig(this IServiceCollection services)
	{
		var mappingConfigurations = TypeAdapterConfig.GlobalSettings;
		mappingConfigurations.Scan(Assembly.GetExecutingAssembly());

		services.AddSingleton<IMapper>(new Mapper(mappingConfigurations));

		return services;
	}

	private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
	{
		services
			.AddFluentValidationAutoValidation()
			.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


		return services;
	}

	private static IServiceCollection AddBackgroundJobsConfig(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHangfire(config => config
	   .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
		.UseSimpleAssemblyNameTypeSerializer()
		.UseRecommendedSerializerSettings()
		.UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection")));

		services.AddHangfireServer();

		return services;
	}

	private static IServiceCollection AddOpenApiServices(this IServiceCollection services)
	{
		var serviceProvider = services.BuildServiceProvider();
		var apiVersionDescriptionProvider = serviceProvider.GetRequiredService<IApiVersionDescriptionProvider>();

		foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
		{
			services.AddOpenApi(description.GroupName, options =>
			{
				options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
				options.AddDocumentTransformer(new ApiVersioningTransformer(description));
			});
		}

		return services;
	}
}
