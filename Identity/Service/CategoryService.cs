using Identity.Areas.Identity.Data;
using Identity.Models;
using Identity.Repositories;

namespace Identity.Service
{
    public class CategoryService : Repository<Category>, ICategoryService
    {
        public CategoryService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
