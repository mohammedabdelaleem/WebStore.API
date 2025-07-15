
namespace WebStore.API.Contracts.Order;

public record CreateOrderRequset
	(
		 string PickupName,
		 string PickupEmail,
		 string PickupPhoneNumber,
		 string UserId,
		 double OrderTotal,
		 string StripePaymentIntentId,
		 string Status,
		 int TotalItems,
		 IEnumerable<CreateOrderDetailsRequset> OrderDetailsCreateDTO
	);