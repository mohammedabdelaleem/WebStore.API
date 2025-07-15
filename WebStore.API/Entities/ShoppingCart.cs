﻿using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.API.Entities;

public class ShoppingCart
{
	public int Id { get; set; }

	public string UserId { get; set; }


	[NotMapped]
	public string StripePaymentIntentId { get; set; }

	[NotMapped]
	public string ClientSecret { get; set; }

	[NotMapped]
	public double CartTotal { get; set; }

	public ICollection<CartItem> CartItems { get; set; }
}
