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
   }
}
