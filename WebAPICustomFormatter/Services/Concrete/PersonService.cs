using System.Linq.Expressions;
using WebAPICustomFormatter.Entities;
using WebAPICustomFormatter.Repository.Abstract;
using WebAPICustomFormatter.Services.Abstract;

namespace WebAPICustomFormatter.Services.Concrete;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _repository;

    public PersonService(IPersonRepository repository)
    {
        _repository = repository;
    }

    public async Task AddAsync(Person entity)
    {
        await _repository.AddAsync(entity);
    }

    public async Task DeleteAsync(Person entity)
    {
        await _repository.DeleteAsync(entity);
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Person> GetAsync(Expression<Func<Person, bool>> predicate)
    {
        return await _repository.GetAsync(predicate);
    }

    public async Task UpdateAsync(Person entity)
    {
        await _repository.UpdateAsync(entity);
    }
}
