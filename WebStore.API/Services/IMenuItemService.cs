using WebStore.API.Contracts.MenuItem;

namespace WebStore.API.Services;

public interface IMenuItemService
{
	Task<Result<List<MenuItemResponse>>> GetAll(CancellationToken cancellationToken = default);
}
