using Identity.Service;

namespace Identity.UnitOfWorks
{
    public interface IUnitOfWork
    {
        ICategoryService CategoryService { get; }
        IProductService ProductService { get; }
    }
}
