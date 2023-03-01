using BookStore.Models;

namespace BookStore.DataAccess.Repository.IRepository
{
   public interface IShoppingCartRepository : IRepository<ShoppingCart>
   {
      int IncrimentCount(ShoppingCart model, int count);
      int DecrimentCount(ShoppingCart model, int count);
   }
}
