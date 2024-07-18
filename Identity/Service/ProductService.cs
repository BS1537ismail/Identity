using Identity.Areas.Identity.Data;
using Identity.Models;
using Identity.Repositories;

namespace Identity.Service
{
    public class ProductService : Repository<Product>, IProductService
    {
        private readonly ApplicationDbContext dbContext;

        public ProductService(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        //public async Task<IEnumerable<Product>> GetAllProductsAsync()
        //{
        //    return await dbContext.GetAllAsync();
        //}

        //public async Task<Product> GetProductByIdAsync(int id)
        //{
        //    return await dbContext.GetByIdAsync(id);
        //}

        //public async Task AddProductAsync(Product product)
        //{
        //    await dbContext.AddAsync(product);
        //}

        //public async Task UpdateProductAsync(Product product)
        //{
        //    await dbContext.UpdateAsync(product);
        //}

        //public async Task DeleteProductAsync(int id)
        //{
        //    await dbContext.DeleteAsync(id);
        //}
    }
}
