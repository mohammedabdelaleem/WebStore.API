using WebStore.API.Contracts.Users;

namespace WebStore.API.Mapping;

public class MappingConfigurations : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		
		config.NewConfig<(ApplicationUser user, IList<string> userRoles), UserResponse>()
			.Map(dest => dest, src => src.user)
			.Map(dest => dest.Roles, src => src.userRoles);


		config.NewConfig<CreateUserRequest, ApplicationUser>()
			.Map(dest => dest.EmailConfirmed, src => true); 


		config.NewConfig<UpdateUserRequest, ApplicationUser>()
			.Map(dest => dest.NormalizedUserName, src => src.Email.ToUpper()); // update need this  

	}
}
