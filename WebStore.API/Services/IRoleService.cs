using WebStore.API.Contracts.Roles;

namespace WebStore.API.Services;

public interface IRoleService
{
	Task<IEnumerable<RoleResponse>> GetAllAsync(bool includeDisabled = false, CancellationToken cancellationToken = default);
	Task<Result<RoleDetailsResponse>> GetAsync(string roleId, CancellationToken cancellationToken = default);
	Task<Result<RoleDetailsResponse>> AddAsync(RoleRequest request, CancellationToken cancellationToken = default);
	Task<Result> UpdateAsync(string id, RoleRequest request, CancellationToken cancellationToken = default);
	Task<Result> ToggleStatusAsync(string id, CancellationToken cancellationToken = default);

}
