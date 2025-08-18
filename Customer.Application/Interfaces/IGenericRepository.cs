using System.Linq.Expressions;

namespace Customer.Application.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);

    Task<IEnumerable<T>> GetAllAsync();

    Task AddAsync(T entity);

    void Update(T entity);

    void Delete(T entity);

    Task<T> GetByAsync(Expression<Func<T, bool>> predicate);

    Task SaveAsync();
}
