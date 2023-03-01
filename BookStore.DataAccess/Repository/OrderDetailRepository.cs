using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;

namespace BookStore.DataAccess.Repository
{
   public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
	{
      private readonly ApplicationDbContext context;

      public OrderDetailRepository(ApplicationDbContext _context) : base(_context)
      {
         context = _context;
      }

      public void Update(OrderDetail entity)
      {
         context.OrderDetails.Update(entity);
      }
   }
}
