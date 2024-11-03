using System.Linq.Expressions;

namespace YanKoltukBackend.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // CRUD Ops
        Task<T> GetByIdAsync(int id);   // retrieve a single entity by ID
        Task<IEnumerable<T>> GetAllAsync(); // retrieve all entities
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);    // retrieve entities by a condition

        Task AddAsync(T entity);    // add a new entity
        Task Update(T entity); // update an existing entity
        Task Delete(T entity); // delete an entity
    }
}
