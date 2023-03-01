namespace BookStore.Models.ViewModel
{
   public class ShoppingCardVM
   {
      public IEnumerable<ShoppingCart> CartList { get; set; }

      public OrderHeader OrderHeader { get; set; }
   }
}
