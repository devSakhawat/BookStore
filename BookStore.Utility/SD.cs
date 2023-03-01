namespace BookStore.Utility
{
	public static class SD
	{
		//Roles of Application
		public const string Role_User_Indi = "Individual";
		public const string Role_User_Comp = "Company";
		public const string Role_Admin = "Admin";
		public const string Role_Employee = "Employee";


		// OrderStatus
		public const string StatusPending = "Pending";
		public const string StatusApproved = "Approved";
		public const string StatusInProcess = "Process";
		public const string StatusShipped = "Shipped";
		public const string StatusCancelled = "Cancelled";
		public const string StatusRefunded = "Refunded";

		// PaymentStatus
		public const string PyamentStatusPending = "Pending";
		public const string PyamentStatusApproved = "Approved";
		public const string PyamentStatusDelayedPayment = "ApprovedForDelayedPayment";
		public const string PyamentStatusRejected = "Rejected";

		public const string SessionCart = "SessionShoppingCart";
	}
}
