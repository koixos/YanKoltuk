using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YanKoltukBackend.Data;
using YanKoltukBackend.Repositories.Interfaces;

namespace YanKoltukBackend.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly YanKoltukDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(YanKoltukDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task Update(T entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }
    }
}
