using WebStore.API.Contracts.Order;

namespace WebStore.API.Services;

public class OrderService(AppDbContext context) : IOrderService
{
	private readonly AppDbContext _context = context;

	public async Task<Result<List<Order>>> GetAll(CancellationToken cancellationToken=default)
	{
		var orders = await _context.Orders.Include(o=>o.OrderDetails)
						.ThenInclude(od=>od.MenuItem)
						.OrderByDescending(o=>o.Id)
						.ToListAsync(cancellationToken);

		if(!orders.Any())
			return Result.Failure<List<Order>>(OrderErrors.Empty);

		return Result.Success(orders);
	}

	public async Task<Result<List<Order>>> GetAllForUser(string userId,CancellationToken cancellationToken = default)
	{
		if(!await _context.Users.AnyAsync(u=>u.Id == userId, cancellationToken))
			return Result.Failure<List<Order>>(UserErrors.NotFound);

		var orders = await _context.Orders.Include(o => o.OrderDetails)
						.ThenInclude(od => od.MenuItem)
						.Where(o => o.UserId == userId)
						.OrderByDescending(o => o.Id)
						.ToListAsync(cancellationToken);

		if (!orders.Any())
			return Result.Failure<List<Order>>(OrderErrors.Empty);

		return Result.Success(orders);
	}

	public async Task<Result<Order>> Get(int orderId ,CancellationToken cancellationToken = default)
	{
		var order = await _context.Orders.Include(o => o.OrderDetails)
						.ThenInclude(od => od.MenuItem)
						.Where(o => o.Id == orderId)
						.SingleOrDefaultAsync(cancellationToken);

		if (order == null)
			return Result.Failure<Order>(OrderErrors.NotFound);

		return Result.Success(order);
	}

	public async Task<Result<Order>> Add(CreateOrderRequset requset, CancellationToken cancellationToken = default)
	{
		Order order = requset.Adapt<Order>();
		
		await _context.AddAsync(order, cancellationToken);
		await _context.SaveChangesAsync( cancellationToken);

		foreach (var orderDetailDTO in requset.OrderDetailsCreateDTO)
		{

			OrderDetails orderDetails = new()
			{
				OrderId = order.Id,
				MenuItemId = orderDetailDTO.MenuItemId, // validate Menu Item At Request
				ItemName = orderDetailDTO.ItemName,
				Quantity = orderDetailDTO.Quantity,
				Price = orderDetailDTO.Price,
			};

			await _context.AddAsync(orderDetails, cancellationToken);
		}
		await _context.SaveChangesAsync( cancellationToken);

		return Result.Success(order);

	}

	public async Task<Result> Update(int id ,UpdateOrderRequset requset, CancellationToken cancellationToken = default)
	{
		if(id != requset.Id)
				return Result.Failure(OrderErrors.InvalidInfo);

		var orderFromDB = await _context.Orders.SingleOrDefaultAsync(o => o.Id == id, cancellationToken);
		
		if(orderFromDB == null)
			return Result.Failure(OrderErrors.NotFound);

		requset.Adapt(orderFromDB);

		await _context.SaveChangesAsync( cancellationToken);

		return Result.Success();
		
	}
}
