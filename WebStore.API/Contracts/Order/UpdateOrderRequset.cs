namespace WebStore.API.Contracts.Order;

 public record UpdateOrderRequset // i only need them to be not empty : is i need validator or it will automatic 
(
	 int Id,
	 string PickupName ,
	 string PickupEmail ,
	 string PickupPhoneNumber ,
	 double OrderTotal, 
	 //why we remove user id at update
	 DateTime OrderDate, 
	 string StripePaymentIntentId ,
	 string Status 
	 );