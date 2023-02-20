using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;

namespace BookStore.DataAccess.Repository
{
   public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
   {
      private readonly ApplicationDbContext context;

      public ShoppingCartRepository(ApplicationDbContext _context) : base(_context)
      {
         context = _context;
      }

      public int DecrimentCount(ShoppingCart model, int count)
      {
         model.Count -= count;
         return model.Count;
      }

      public int IncrimentCount(ShoppingCart model, int count)
      {
         model.Count += count;
         return model.Count;
      }
   }
}