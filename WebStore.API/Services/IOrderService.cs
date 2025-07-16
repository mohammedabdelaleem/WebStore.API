using WebStore.API.Contracts.Order;

namespace WebStore.API.Services;

public interface IOrderService
{
	Task<Result<List<Order>>> GetAll(CancellationToken cancellationToken = default);
	Task<Result<List<Order>>> GetAllForUser(string userId, CancellationToken cancellationToken = default);
	Task<Result<Order>> Get(int orderId, CancellationToken cancellationToken = default);

	Task<Result<Order>> Add(CreateOrderRequset requset, CancellationToken cancellationToken = default);
	Task<Result> Update(int id, UpdateOrderRequset requset, CancellationToken cancellationToken = default);
}
