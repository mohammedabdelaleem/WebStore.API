using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.API.Entities;

public class OrderDetails
{
	public int Id { get; set; }
	public int OrderId { get; set; }


	[ForeignKey(nameof(MenuItem))]
	public int MenuItemId { get; set; }
	public MenuItem MenuItem { get; set; }
	public int Quantity { get; set; }

	// from menu item we could retrive the name and price for this menu item 
	// but sometimes this name or price gets updated
	// at this case we dont want to toggle the price that order was updated with	
	public string ItemName { get; set; }  // give me the name and price at this period of time
	public double Price	 { get; set; }		
}
