using BlazorOnionDemo.Domain.Entity;

namespace BlazorOnionDemo.Application.Repositories;

public interface IAuthorRepository
{
    Task<Author> LoadAsync(int id);
    Task<int> SaveAsync();
    Task AddAsync(Author author);
}