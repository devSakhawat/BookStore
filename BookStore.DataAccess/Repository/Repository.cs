using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BookStore.DataAccess.Repository
{
   public class Repository<T> : IRepository<T> where T : class
   {
      private readonly ApplicationDbContext context;
      internal DbSet<T> dbSet;
      public Repository(ApplicationDbContext _context)
      {
         context = _context;
         dbSet = context.Set<T>();
      }

      public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
      {
         IQueryable<T> query = dbSet;
         query = query.Where(filter);
         return query.FirstOrDefault();
      }

      public IEnumerable<T> GetAll()
      {
         IQueryable<T> query = dbSet;
         query = query.AsQueryable().AsNoTracking();
         return query.ToList();
         //return dbSet.AsQueryable().AsNoTracking().ToList();
      }

      public void Add(T entity)
      {
         dbSet.Add(entity);
      }

      public void Remove(T entity)
      {
         dbSet.Remove(entity);
      }

      public void RemoveRange(IEnumerable<T> entity)
      {
         dbSet.RemoveRange(entity);
      }
   }
}