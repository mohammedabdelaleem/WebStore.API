
using WebStore.API.Contracts.Order;

namespace WebStore.API.Mapping;

public class MappingConfigurations(IWebHostEnvironment webHostEnvironment) : IRegister
{
	private readonly string _rootPath = webHostEnvironment.WebRootPath.Replace("\\","/");
	public void Register(TypeAdapterConfig config)
	{
		
		config.NewConfig<(ApplicationUser user, IList<string> userRoles), UserResponse>()
			.Map(dest => dest, src => src.user)
			.Map(dest => dest.Roles, src => src.userRoles);


		config.NewConfig<CreateUserRequest, ApplicationUser>()
			.Map(dest => dest.EmailConfirmed, src => true); 


		config.NewConfig<UpdateUserRequest, ApplicationUser>()
			.Map(dest => dest.NormalizedUserName, src => src.Email.ToUpper()); // update need this  


		config.NewConfig<MenuItem, MenuItemResponse>()
			.Map(dest=>dest.ImageUrl, src => Path.Combine(_rootPath, src.ImageUrl));

		config.NewConfig<CreateMenuItemRequest, MenuItem>()
		.Map(dest => dest.ImageUrl, src => $"images/{src.Image.FileName}");

		config.NewConfig<CreateOrderRequset, Order>()
		.Map(dest => dest.OrderDate, src => DateTime.UtcNow)
		.Map(dest => dest.Status, src => string.IsNullOrEmpty(src.Status) ? OrderStatus.Pending : src.Status);
	}
}
