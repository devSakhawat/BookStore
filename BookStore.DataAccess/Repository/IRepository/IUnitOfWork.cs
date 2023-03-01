namespace BookStore.DataAccess.Repository.IRepository
{
   public interface IUnitOfWork
   {
      void Save();
      ICategoryRepository Category { get; }
      ICoverTypeRepository CoverType { get; }
      IProductRepository Product { get; }
      ICompanyRepository Company { get; }
      IApplicationUserRepository ApplicationUser { get; }
      IShoppingCartRepository ShoppingCart { get; }
      IOrderDetailRepository OrderDetail { get; }
      IOrderHeaderRepository OrderHeader { get; }
   }
}
