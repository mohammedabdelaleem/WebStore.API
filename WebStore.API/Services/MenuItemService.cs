using WebStore.API.Contracts.MenuItem;

namespace WebStore.API.Services;

public class MenuItemService(
	AppDbContext context,
	IWebHostEnvironment webHostEnvironment
	) : IMenuItemService
{
	private readonly AppDbContext _context = context;
	private readonly string _imagesPath = $"{webHostEnvironment.WebRootPath}/images";
	

	public async Task<Result<List<MenuItemResponse>>> GetAll(CancellationToken cancellationToken = default)
	{
	var menuItems =	await _context.MenuItems
				.ProjectToType<MenuItemResponse>()
				.ToListAsync(cancellationToken);

		return Result.Success(menuItems);
	}

	public async Task<Result<MenuItemResponse>> Get(int id,  CancellationToken cancellationToken = default)
	{
		if (await _context.MenuItems.SingleOrDefaultAsync(x => x.Id == id, cancellationToken) is not { } menuItem)
			return Result.Failure<MenuItemResponse>(MenuItemErrors.NotFound);

		return Result.Success(menuItem.Adapt<MenuItemResponse>());
	}

	public async Task<Result<MenuItemResponse>> Add(CreateMenuItemRequest request, CancellationToken cancellationToken = default)
	{

		try
		{
			var menuItem = request.Adapt<MenuItem>();

			var uniqueFileName = $"{Guid.CreateVersion7()}{request.Image.FileName}";
			menuItem.ImageUrl = Path.Combine("images", uniqueFileName);

			var path = Path.Combine(_imagesPath, uniqueFileName);

			using var stream = File.Create(path);
			await request.Image.CopyToAsync(stream, cancellationToken);


			await _context.AddAsync(menuItem, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);

			return Result.Success(menuItem.Adapt<MenuItemResponse>());

		}
		catch
		{
			return Result.Failure<MenuItemResponse>(MenuItemErrors.SavingError);
		}
	}


	public async Task<Result<MenuItemResponse>> Update(CreateMenuItemRequest request, CancellationToken cancellationToken = default)
	{

		try
		{
			var menuItem = request.Adapt<MenuItem>();
			menuItem.ImageUrl = Path.Combine("images", request.Image.FileName);

			var path = Path.Combine(_imagesPath, request.Image.FileName);

			using var stream = File.Create(path);
			await request.Image.CopyToAsync(stream, cancellationToken);


			await _context.AddAsync(menuItem, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);

			return Result.Success(menuItem.Adapt<MenuItemResponse>());

		}
		catch
		{
			return Result.Failure<MenuItemResponse>(MenuItemErrors.SavingError);
		}
	}

}
