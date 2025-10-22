using BlazorOnionDemo.Application.Repositories;
using BlazorOnionDemo.Domain.Entity;
using BlazorOnionDemo.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BlazorOnionDemo.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly OrmContext _db;

    public AuthorRepository(OrmContext db)
    {
        _db = db;
    }
    async Task IAuthorRepository.AddAsync(Author author)
    {
        _db.Authors.Add(author);
        await _db.SaveChangesAsync();
    }

    async Task<Author> IAuthorRepository.LoadAsync(int id)
    {
        return await _db.Authors.Include(a => a.Books).FirstAsync(a => a.Id == id);
    }

    async Task<int> IAuthorRepository.SaveAsync()
    {
        return await _db.SaveChangesAsync();
    }
}