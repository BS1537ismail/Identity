using System.Linq.Expressions;

namespace Identity.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string? inclideProperties = null);
        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate, string? inclideProperties = null);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
