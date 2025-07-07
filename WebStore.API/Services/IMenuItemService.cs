using WebStore.API.Contracts.MenuItem;

namespace WebStore.API.Services;

public interface IMenuItemService
{
	Task<Result<List<MenuItemResponse>>> GetAll(CancellationToken cancellationToken = default);
	Task<Result<MenuItemResponse>> Get(int id, CancellationToken cancellationToken = default);
	Task<Result<MenuItemResponse>> Add(CreateMenuItemRequest request , CancellationToken cancellationToken = default);

}
