using Identity.Areas.Identity.Data;
using Identity.Service;

namespace Identity.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        public ICategoryService CategoryService { get; private set; }
        public IProductService ProductService { get; private set; }
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            CategoryService = new CategoryService(dbContext);
            ProductService = new ProductService(dbContext);
        }
    }
}
