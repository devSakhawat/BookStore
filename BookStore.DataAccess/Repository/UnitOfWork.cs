using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;

namespace BookStore.DataAccess.Repository
{
   public class UnitOfWork : IUnitOfWork
   {
      private readonly ApplicationDbContext context;

      public UnitOfWork(ApplicationDbContext _context)
      {
         context = _context;
         Category = new CategoryRepository(context);
         CoverType = new CoverTypeRepository(context);
         Product = new ProductRepository(context);
         Company = new CompanyRepository(context);
      }

      public void Save()
      {
         context.SaveChanges();
      }

      public ICategoryRepository Category { get; private set; }

      public ICoverTypeRepository CoverType { get; private set; }

      public IProductRepository Product { get; private set; }
      public ICompanyRepository Company { get; private set; }
   }
}