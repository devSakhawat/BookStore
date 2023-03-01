using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;

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
         ApplicationUser = new ApplicationUserRepository(context);
         ShoppingCart = new ShoppingCartRepository(context);
         OrderDetail = new OrderDetailRepository(context);
         OrderHeader= new OrderHeaderRepository(context);
      }

      public void Save()
      {
         context.SaveChanges();
      }

      public ICategoryRepository Category { get; private set; }

      public ICoverTypeRepository CoverType { get; private set; }

      public IProductRepository Product { get; private set; }
      public ICompanyRepository Company { get; private set; }
      public IApplicationUserRepository ApplicationUser { get; private set; }
      public IShoppingCartRepository ShoppingCart { get; private set; }
      public IOrderDetailRepository OrderDetail { get; private set; }
      public IOrderHeaderRepository OrderHeader { get; private set; }
   }
}