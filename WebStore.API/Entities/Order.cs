using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.API.Entities;

public class Order
{
	public int Id { get; set; }
	public string PickupName { get; set; }
	public string PickupEmail { get; set; }
	public string PickupPhoneNumber { get; set; }


	[ForeignKey(nameof(ApplicationUser))]
	public string UserId { get; set; } // only registered user
	public ApplicationUser User { get; set; }

	public double OrderTotal { get; set; } // calculated attribute
	public DateTime OrderDate { get; set; }
	public string StripePaymentIntentId { get; set; }
	public string Status { get; set; }
	public int TotalItems { get; set; }

	public IEnumerable<OrderDetails> OrderDetails { get; set; }
}
