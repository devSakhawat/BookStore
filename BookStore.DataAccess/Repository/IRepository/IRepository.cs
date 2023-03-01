using System.Linq.Expressions;

namespace BookStore.DataAccess.Repository.IRepository
{
   public interface IRepository<T>
   {
      //T - Class
      T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);
      IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
      void Add(T entity);
      void Remove(T entity);
      void RemoveRange(IEnumerable<T> entity);
   }
}
