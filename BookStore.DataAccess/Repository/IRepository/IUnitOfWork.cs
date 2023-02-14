namespace BookStore.DataAccess.Repository.IRepository
{
   public interface IUnitOfWork
   {
      void Save();
      ICategoryRepository Category { get; }

   }
}
