using WebStore.API.Contracts.MenuItem;

namespace WebStore.API.Services;

public class MenuItemService(AppDbContext context) : IMenuItemService
{
	private readonly AppDbContext _context = context;

	public async Task<Result<List<MenuItemResponse>>> GetAll(CancellationToken cancellationToken = default)
	{
	var menuItems =	await _context.MenuItems
			.ProjectToType<MenuItemResponse>().
			ToListAsync(cancellationToken);

		return Result.Success(menuItems);
	}
	
}
