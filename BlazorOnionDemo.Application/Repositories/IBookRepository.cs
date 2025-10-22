using BlazorOnionDemo.Domain.Entity;

namespace BlazorOnionDemo.Application.Repositories;

public interface IBookRepository
{
    Task<Book> LoadAsync(int id);
    Task<int> SaveAsync();
    Task AddAsync(Book author);
}