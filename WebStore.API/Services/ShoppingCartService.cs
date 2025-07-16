

namespace WebStore.API.Services;

public class ShoppingCartService(
	AppDbContext context,
	ILogger<ShoppingCartService> logger) : IShoppingCartService
{
	private readonly AppDbContext _context = context;
	private readonly ILogger<ShoppingCartService> _logger = logger;


	public async Task<Result<ShoppingCart>> GetShoppingCart(string userId, CancellationToken cancellationToken = default)
	{
		// becauese of redux problem
		ShoppingCart shoppingCart ;

		if (!await _context.Users.AnyAsync(u => u.Id == userId, cancellationToken))
		{
			shoppingCart = new();
		}
		else
		{

			shoppingCart = await _context.ShoppingCarts
				   .Include(s => s.CartItems)
				   .ThenInclude(c => c.MenuItem)
				   .FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken);
		}

		if(shoppingCart == null )
			return Result.Failure<ShoppingCart>(ShoppingCartErrors.NotFound);


		if (shoppingCart.CartItems != null && shoppingCart.CartItems.Count > 0)
		shoppingCart.CartTotal = shoppingCart.CartItems.Sum(c => c.Quantity * c.MenuItem.Price);


		return Result.Success(shoppingCart);
	}

	public async Task<Result> AddOrUpdateItemInCart(CreateShoppingCartRequest request, CancellationToken cancellationToken = default)
	{
		try
		{
			if (!await _context.Users.AnyAsync(u=>u.Id == request.UserId, cancellationToken))
				return Result.Failure(UserErrors.NotFound);


			var shoppingCartDb = await _context.ShoppingCarts.Include(s => s.CartItems)
					.FirstOrDefaultAsync(s => s.UserId == request.UserId, cancellationToken);

			if (await _context.MenuItems.FirstOrDefaultAsync(m => m.Id == request.MenuItemId, cancellationToken) is not { } menuItem)
				return Result.Failure(MenuItemErrors.NotFound);


			if (shoppingCartDb == null && request.UpdatedQuantityBy <= 0)
			{
				return Result.Failure(new Error("Invalid Quantity", "Quantity Can't Be Less Than Or Equal Zero", StatusCodes.Status400BadRequest));
			}

			if (shoppingCartDb == null && request.UpdatedQuantityBy > 0)
			{
				// create a new shopping cart and add new menu item	
				await CreateNewShoppingCart(request, cancellationToken);

			}
			else
			{

				// shopping cart already exists for this user
				// check if there is a cart item for this menu item id 
				// if no create new one 
				// if yes update quantity

				await UpdateShoppingCart(request, shoppingCartDb, cancellationToken);
			}

			return Result.Success();
		}
		catch (Exception ex)
		{
			// logging 
			_logger.LogError("Error While Adding Or Updating Shoppig Cart: {msg}", ex.Message);
			return Result.Failure(ShoppingCartErrors.SavingError);
		}
	}






	private async Task CreateNewShoppingCart(CreateShoppingCartRequest request, CancellationToken cancellationToken = default)
	{
		var newShoppingCart = new ShoppingCart { UserId = request.UserId };

		await _context.AddAsync(newShoppingCart, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

		var newCartItem = new CartItem
		{
			MenuItemId = request.MenuItemId,
			Quantity = request.UpdatedQuantityBy,
			ShoppingCartId = newShoppingCart.Id,
			MenuItem = null               // don't forget null menu item : to prevent create a new one
		};

		await _context.AddAsync(newCartItem, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

	}
	private async Task UpdateShoppingCart(CreateShoppingCartRequest request, ShoppingCart shoppingCartDb, CancellationToken cancellationToken)
	{
		var cartItemInCart = shoppingCartDb.CartItems.FirstOrDefault(c => c.MenuItemId == request.MenuItemId);
		if (cartItemInCart == null)
		{

			// item doesn't exist in the current cart
			var newCartItem = new CartItem
			{
				MenuItemId = request.MenuItemId,
				Quantity = request.UpdatedQuantityBy,
				ShoppingCartId = shoppingCartDb.Id,
				MenuItem = null               // don't forget null menu item : to prevent create a new one
			};

			await _context.AddAsync(newCartItem, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
		}
		else
		{
			// item already exists in the cart and we need to update quantity 
			var newQuantity = cartItemInCart.Quantity + request.UpdatedQuantityBy;

			if (request.UpdatedQuantityBy == 0 || newQuantity <= 0)
			{
				// remove cart item from the cart , if it is the only item then also remove the cart  
				_context.Remove(cartItemInCart);
				if (shoppingCartDb.CartItems.Count() == 1)  // shopping cart saving at memory 
					_context.Remove(shoppingCartDb);

				await _context.SaveChangesAsync(cancellationToken); // now uesr has no shopping cart 
			}
			else
			{
				cartItemInCart.Quantity = newQuantity;
				await _context.SaveChangesAsync(cancellationToken);
			}
		}
	}

}
