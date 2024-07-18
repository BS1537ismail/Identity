using Identity.Models;
using Identity.Repositories;

namespace Identity.Service
{
    public interface IProductService : IRepository<Product>
    {
        //Task<IEnumerable<Product>> GetAllProductsAsync();
        //Task<Product> GetProductByIdAsync(int id);
        //Task AddProductAsync(Product product);
        //Task UpdateProductAsync(Product product);
        //Task DeleteProductAsync(int id);
    }
}
