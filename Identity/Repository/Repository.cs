using Microsoft.EntityFrameworkCore;
using Identity.Areas.Identity.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq.Expressions;

namespace Identity.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _context.products.Include(x => x.Category).Include(x => x.CategoryId);
        }

        public async Task<IEnumerable<T>> GetAllAsync(string? inclideProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (!string.IsNullOrEmpty(inclideProperties))
            {
                foreach (var includeProp in inclideProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp.Trim());
                }
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate, string? inclideProperties = null)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(predicate);

            if (!string.IsNullOrEmpty(inclideProperties))
            {
                foreach (var includeProp in inclideProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp.Trim());
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
