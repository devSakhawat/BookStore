using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;

namespace BookStore.DataAccess.Repository
{
   public class CategoryRepository : Repository<Category>, ICategoryRepository
   {
      private readonly ApplicationDbContext context;

      public CategoryRepository(ApplicationDbContext _context) : base(_context)
      {
         context = _context;
      }

      public void Update(Category entity)
      {
         context.Categories.Update(entity);
      }

      public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
      { 
         var orderHeader = context.OrderHeaders.FirstOrDefault(oh => oh.Id == id);
         if (orderHeader != null)
         {
            orderHeader.OrderStatus = orderStatus;
            if (paymentStatus != null)
            {
               orderHeader.PaymentStatus = paymentStatus;
            }
         }
      }
   }
}
