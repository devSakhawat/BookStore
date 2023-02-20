using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;

namespace BookStore.DataAccess.Repository
{
   public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
   {
      private readonly ApplicationDbContext context;

      public ApplicationUserRepository(ApplicationDbContext _context) : base(_context)
      {
         context = _context;
      }
   }
}
