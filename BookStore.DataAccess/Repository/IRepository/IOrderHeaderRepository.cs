using BookStore.Models;

namespace BookStore.DataAccess.Repository.IRepository
{
   public interface IOrderHeaderRepository : IRepository<OrderHeader>
   {
      void Update(OrderHeader entity);
      void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);
   }
}
