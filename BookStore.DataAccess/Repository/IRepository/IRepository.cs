using System.Linq.Expressions;

namespace BookStore.DataAccess.Repository.IRepository
{
   public interface IRepository<T>
   {
      T GetFirstOrDefault(Expression<Func<T, bool>> filter);
      IEnumerable<T> GetAll();
      void Add(T entity);
      void Remove(T entity);
      void RemoveRange(IEnumerable<T> entity);
   }
}
