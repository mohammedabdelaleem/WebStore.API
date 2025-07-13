using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.API.Entities;

public class CartItem	
{
	public  int  Id { get; set; }

	[ForeignKey(nameof(MenuItem))]
	public int MenuItemId { get; set; }

	public MenuItem MenuItem { get; set; } = new();

	public int Quantity { get; set; }

	[ForeignKey(nameof(ShoppingCart))]
	public int ShoppingCartId { get; set; }

}
