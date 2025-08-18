using Customer.Application.Interfaces;
using Customer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Customer.Infrastructure.Reositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly CustomerDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(CustomerDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<T> GetByAsync(Expression<Func<T, bool>> predicate) => await _dbSet!.Where(predicate!)!.FirstOrDefaultAsync()!;

    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task SaveAsync() => await _context.SaveChangesAsync();

    public void Update(T entity) => _dbSet.Update(entity);
}