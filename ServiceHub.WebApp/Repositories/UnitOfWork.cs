using ServiceHub.WebApp.Interfaces;

namespace ServiceHub.WebApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategories Categories { get; set; }

        public UnitOfWork(ICategories Categories)
        {
            this.Categories = Categories;
        }
    }
}