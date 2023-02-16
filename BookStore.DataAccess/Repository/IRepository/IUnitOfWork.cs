namespace BookStore.DataAccess.Repository.IRepository
{
   public interface IUnitOfWork
   {
      void Save();
      ICategoryRepository Category { get; }
      ICoverTypeRepository CoverType { get; }
      IProductRepository Product { get; }
   }
}
