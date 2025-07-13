
namespace WebStore.API.Services;

public class ShoppingCartService(
	AppDbContext context,
	UserManager<ApplicationUser> userManager) : IShoppingCartService
{
	private readonly AppDbContext _context = context;
	private readonly UserManager<ApplicationUser> _userManager = userManager;

	public async Task<Result> AddOrUpdateItemInCart(string userId, int menuItemId, int updatedQuantityBy, CancellationToken cancellationToken = default)
	{
		try
		{
			if (await _userManager.FindByIdAsync(userId) is not { })
				return Result.Failure(UserErrors.UserNotFound);


			var shoppingCartDb = await _context.ShoppingCarts.Include(s => s.CartItems).FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken);

			if (await _context.MenuItems.FirstOrDefaultAsync(m => m.Id == menuItemId, cancellationToken) is not { } menuItem)
				return Result.Failure(MenuItemErrors.NotFound);


			if (shoppingCartDb == null && updatedQuantityBy <= 0)
			{
				return Result.Failure(new Error("Invalid Quantity", "Quantity Can't Be Less Than Or Equal Zero", StatusCodes.Status400BadRequest));
			}

			// create a new shopping cart and add new menu item	
			if (shoppingCartDb == null && updatedQuantityBy > 0)
			{

				var newShoppingCart = new ShoppingCart { UserId = userId };

				await _context.AddAsync(newShoppingCart, cancellationToken);
				await _context.SaveChangesAsync(cancellationToken);

				var newCartItem = new CartItem
				{
					MenuItemId = menuItemId,
					Quantity = updatedQuantityBy,
					ShoppingCartId = newShoppingCart.Id,
					MenuItem = null               // don't forget null menu item : to prevent create a new one
				};

				await _context.AddAsync(newCartItem, cancellationToken);
				await _context.SaveChangesAsync(cancellationToken);

			}
			else
			{
				// shopping cart already exists for this user
				var cartItemInCart = shoppingCartDb.CartItems.FirstOrDefault(c => c.MenuItemId == menuItemId);
				if (cartItemInCart == null)
				{

					// item doesn't exist in the current cart
					var newCartItem = new CartItem
					{
						MenuItemId = menuItemId,
						Quantity = updatedQuantityBy,
						ShoppingCartId = shoppingCartDb.Id,
						MenuItem = null               // don't forget null menu item : to prevent create a new one
					};

					await _context.AddAsync(newCartItem, cancellationToken);
					await _context.SaveChangesAsync(cancellationToken);
				}
				else
				{
					// item already exists in the cart and we need to update quantity 
					var newQuantity = cartItemInCart.Quantity + updatedQuantityBy;

					if (updatedQuantityBy == 0 || newQuantity <= 0)
					{
						// remove cart item from the cart , if it is the only item then also remove the cart  
						_context.Remove(cartItemInCart);

						if (shoppingCartDb.CartItems.Count() == 1)  // shopping cart saving at memory 
							_context.Remove(shoppingCartDb);


						await _context.SaveChangesAsync(cancellationToken);
					}
					else
					{
						cartItemInCart.Quantity = newQuantity;
						await _context.SaveChangesAsync(cancellationToken);
					}
				}
			}

			return Result.Success();
		}
		catch (Exception ex)
		{
			// logging 

			return Result.Failure(ShoppingCartErrors.SavingError);
		}
	}

}
