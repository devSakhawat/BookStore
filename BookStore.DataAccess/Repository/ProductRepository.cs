using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;

namespace BookStore.DataAccess.Repository
{
   public class ProductRepository : Repository<Product>, IProductRepository
   {
      private readonly ApplicationDbContext context;

      public ProductRepository(ApplicationDbContext _context) : base(_context)
      {
         context = _context;
      }

      public void Update(Product entity)
      {
         var product = context.Products.FirstOrDefault(p => p.Id == entity.Id);
         if (product != null)
         {
            product.Title= entity.Title;
            product.Description= entity.Description;
            product.ISBN= entity.ISBN;
            product.Author= entity.Author;
            product.ListPrice= entity.ListPrice;
            product.Price= entity.Price;
            product.Price50= entity.Price50;
            product.Price100= entity.Price100;
            product.CategoryId= entity.CategoryId;
            product.CoverTypeId = entity.CoverTypeId;
            if (product.ImageUrl != null)
            {
               product.ImageUrl = entity.ImageUrl;
            }
         }
      }
   }
}
