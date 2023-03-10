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
      
      public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
      {
         IQueryable<T> query = dbSet;
         query = query.Where(filter);
         if (includeProperties != null)
         {
            foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
               query = query.Include(includeProp);
            }
         }
         return query.FirstOrDefault();
      }

      // IncludeProp - "Category,CoverType"
      public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null,  string? includeProperties = null)
      {
         IQueryable<T> query = dbSet;
         if (filter != null)
         {
            query = query.Where(filter);
         }
         query = query.AsQueryable().AsNoTracking();
         if (includeProperties != null)
         {
            foreach (var includeProp in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
            {
               query = query.Include(includeProp);
            }
         }
         return query.ToList();
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