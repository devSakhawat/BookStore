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
   public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
   {
      private readonly ApplicationDbContext context;

      public CoverTypeRepository(ApplicationDbContext _context) : base(_context)
      {
         context = _context;
      }

      public void Update(CoverType entity)
      {
         context.CoverTypes.Update(entity);
      }
   }
}
