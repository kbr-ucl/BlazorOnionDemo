using BlazorOnionDemo.Application.Contracts.Queries;
using BlazorOnionDemo.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BlazorOnionDemo.Infrastructure.Queries;

public class AuthorQuery : IAuthorQuery
{
    private readonly OrmContext _db;

    public AuthorQuery(OrmContext db)
    {
        _db = db;
    }

    async Task<List<AuthorDto>> IAuthorQuery.GetAllAsync()
    {
        return await _db.Authors
            .AsNoTracking()
            .Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                Books = a.Books
                    .Select(b => new AuthorBookDto
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Description = b.Description
                    })
                    .ToList()
            })
            .ToListAsync();
    }


    async Task<AuthorDto> IAuthorQuery.GetByIdAsync(int id)
    {
        return await _db.Authors
            .AsNoTracking()
            .Where(a => a.Id == id)
            .Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                Books = a.Books
                    .Select(b => new AuthorBookDto
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Description = b.Description
                    })
                    .ToList()
            })
            .SingleAsync();
    }
}