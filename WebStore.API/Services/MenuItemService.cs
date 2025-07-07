using Mapster;
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
		var menuItems = await _context.MenuItems
					.ProjectToType<MenuItemResponse>()
					.ToListAsync(cancellationToken);

		return Result.Success(menuItems);
	}

	public async Task<Result<MenuItemResponse>> Get(int id, CancellationToken cancellationToken = default)
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

			var path = Path.Combine(_imagesPath, uniqueFileName);


			Directory.CreateDirectory(_imagesPath); // Ensure folder exists
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


	public async Task<Result> Update(int id, UpdateMenuItemRequest request, CancellationToken cancellationToken = default)
	{
		if (id != request.Id)
			return Result.Failure(MenuItemErrors.InvalidInfo);

		if (await _context.MenuItems.FindAsync(id, cancellationToken) is not { } menuItemDb)
			return Result.Failure(MenuItemErrors.NotFound);


		request.Adapt(menuItemDb);


		// Delete old image if exists
		var oldImagePath = Path.Combine(_imagesPath, Path.GetFileName(menuItemDb.ImageUrl));
		if (File.Exists(oldImagePath))
			File.Delete(oldImagePath);

		// Save new image
		var uniqueFileName = $"{Guid.CreateVersion7()}{Path.GetExtension(request.Image.FileName)}";
		var path = Path.Combine(_imagesPath, uniqueFileName);

		Directory.CreateDirectory(_imagesPath);
		using var stream = File.Create(path);
		await request.Image.CopyToAsync(stream, cancellationToken);

		menuItemDb.ImageUrl = $"images/{uniqueFileName}";

		await _context.SaveChangesAsync(cancellationToken);
		return Result.Success();
	}




	public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
	{
		if (await _context.MenuItems.FindAsync(id, cancellationToken) is not { } menuItem)
			return Result.Failure(MenuItemErrors.NotFound);


		// remove image
		var oldPath = Path.Combine(_imagesPath, Path.GetFileName(menuItem.ImageUrl));
		if (File.Exists(oldPath))
			File.Delete(oldPath);

		_context.MenuItems.Remove(menuItem);
		await _context.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}
