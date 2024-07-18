using Identity.Models;
using Identity.Repositories;

namespace Identity.Service
{
    public interface ICategoryService : IRepository<Category>
    {
        //Task<string?> GetByIdAsync(Func<object, bool> value);
    }
}
