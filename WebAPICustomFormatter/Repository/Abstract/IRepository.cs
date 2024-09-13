using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebAPICustomFormatter.Repository.Abstract;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetAsync(Expression<Func<T, bool>> expression);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
