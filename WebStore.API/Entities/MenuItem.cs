namespace WebStore.API.Entities;

public class MenuItem
{
	public int Id { get; set; } 
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string SpecialTag { get; set; } = string.Empty;
	public string Category { get; set; } = string.Empty;
	public double Price { get; set; } 		
	public string Image { get; set; } = string.Empty;
}
