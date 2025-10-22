using BlazorOnionDemo.Application.Repositories;
using BlazorOnionDemo.Domain.Entity;

namespace BlazorOnionDemo.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    async Task IBookRepository.AddAsync(Book author)
    {
        throw new NotImplementedException();
    }

    async Task<Book> IBookRepository.LoadAsync(int id)
    {
        throw new NotImplementedException();
    }

    async Task<int> IBookRepository.SaveAsync()
    {
        throw new NotImplementedException();
    }
}