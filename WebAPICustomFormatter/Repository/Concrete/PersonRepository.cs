using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAPICustomFormatter.Data;
using WebAPICustomFormatter.Entities;
using WebAPICustomFormatter.Repository.Abstract;

namespace WebAPICustomFormatter.Repository.Concrete;

public class PersonRepository : IPersonRepository
{
    private readonly PersonDbContext _context;

    public PersonRepository(PersonDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Person entity)
    {
        await _context.People.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Person entity)
    {
        await Task.Run(() =>
        {
            _context.People.Remove(entity);
        });
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await Task.Run(() =>
        {
            return _context.People;
        });
    }

    public async Task<Person> GetAsync(Expression<Func<Person, bool>> expression)
    {
        var item = await _context.People.FirstOrDefaultAsync(expression);
        return item;
    }

    public async Task UpdateAsync(Person entity)
    {
        await Task.Run(() =>
        {
            _context.People.Update(entity);
        });
        await _context.SaveChangesAsync();
    }
}
