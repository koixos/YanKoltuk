using System.Linq.Expressions;

namespace YanKoltukBackend.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? include = null);

        Task AddAsync(T entity);    
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
