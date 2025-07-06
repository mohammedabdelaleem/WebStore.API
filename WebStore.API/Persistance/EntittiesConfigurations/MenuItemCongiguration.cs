using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebStore.API.Persistance.EntittiesConfigurations;

public class MenuItemCongiguration : IEntityTypeConfiguration<MenuItem>
{
	public void Configure(EntityTypeBuilder<MenuItem> builder)
	{
		builder.Property(x => x.Name)
			.IsRequired()
			.HasMaxLength(200);

		builder.Property(x => x.Description)
			.IsRequired()
			.HasMaxLength(300);


		builder.Property(x => x.Category)
			.IsRequired()
			.HasMaxLength(50);

		builder.Property(x => x.ImageUrl)
			.IsRequired();


		builder.Property(x => x.Price)
			.IsRequired()
		.HasPrecision(10, 2);

		builder.Property(x => x.SpecialTag)
			.IsRequired()
			.HasMaxLength(50);



		builder.HasData(new MenuItem
		{
			Id = 1,
			Name = "Spring Roll",
			Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
			ImageUrl = $"images/1.jpg",
			Price = 7.99,
			Category = "Appetizer",
			SpecialTag = ""
		}, new MenuItem
		{
			Id = 2,
			Name = "Idli",
			Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
			ImageUrl = $"images/2.jpg",
			Price = 8.99,
			Category = "Appetizer",
			SpecialTag = ""
		}, new MenuItem
		{
			Id = 3,
			Name = "Panu Puri",
			Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
			ImageUrl = $"images/3.jpg",
			Price = 8.99,
			Category = "Appetizer",
			SpecialTag = "Best Seller"
		}, new MenuItem
		{
			Id = 4,
			Name = "Hakka Noodles",
			Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
			ImageUrl = $"images/4.jpg",
			Price = 10.99,
			Category = "Entrée",
			SpecialTag = ""
		}, new MenuItem
		{
			Id = 5,
			Name = "Malai Kofta",
			Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
			ImageUrl = $"images/5.jpg",
			Price = 12.99,
			Category = "Entrée",
			SpecialTag = "Top Rated"
		}, new MenuItem
		{
			Id = 6,
			Name = "Paneer Pizza",
			Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
			ImageUrl = $"images/6.jpg",
			Price = 11.99,
			Category = "Entrée",
			SpecialTag = ""
		}, new MenuItem
		{
			Id = 7,
			Name = "Paneer Tikka",
			Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
			ImageUrl = $"images/7.jpg",
			Price = 13.99,
			Category = "Entrée",
			SpecialTag = "Chef's Special"
		}, new MenuItem
		{
			Id = 8,
			Name = "Carrot Love",
			Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
			ImageUrl = $"images/8.jpg",
			Price = 5.99,
			Category = "Dessert",
			SpecialTag = ""
		}, new MenuItem
		{
			Id = 9,
			Name = "Rasmalai",
			Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
			ImageUrl = $"images/9.jpg",
			Price = 8.99,
			Category = "Dessert",
			SpecialTag = "Chef's Special"
		}, new MenuItem
		{
			Id = 10,
			Name = "Sweet Rolls",
			Description = "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
			ImageUrl = $"images/10.jpg",
			Price = 9.99,
			Category = "Dessert",
			SpecialTag = "Top Rated"
		});
	}
}
