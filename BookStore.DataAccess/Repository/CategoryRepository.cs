using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;

namespace BookStore.DataAccess.Repository
{
   public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
	{
      private readonly ApplicationDbContext context;

      public OrderHeaderRepository(ApplicationDbContext _context) : base(_context)
      {
         context = _context;
      }

      public void Update(OrderHeader entity)
      {
         context.OrderHeaders.Update(entity);
      }
   }
}
