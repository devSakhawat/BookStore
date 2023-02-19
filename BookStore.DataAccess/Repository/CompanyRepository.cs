using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
   public class CompanyRepository : Repository<Company>, ICompanyRepository
   {
      private readonly ApplicationDbContext context;

      public CompanyRepository(ApplicationDbContext _context) : base(_context)
      {
         context = _context;
      }

      public void Update(Company entity)
      {
         context.Companies.Update(entity);
      }
   }
}
