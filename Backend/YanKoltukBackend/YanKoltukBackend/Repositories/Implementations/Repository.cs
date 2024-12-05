using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YanKoltukBackend.Data;
using YanKoltukBackend.Repositories.Interfaces;

namespace YanKoltukBackend.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly YanKoltukDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(YanKoltukDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            IQueryable<T> q = _dbSet.Where(predicate);
            if (include != null)
                q = include(q);
            return await q.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
