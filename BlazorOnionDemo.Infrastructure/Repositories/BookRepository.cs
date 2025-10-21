using BlazorOnionDemo.Application.Repositories;
using BlazorOnionDemo.Domain.Entity;

namespace BlazorOnionDemo.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    Task<Book> IBookRepository.Load(int id)
    {
        throw new NotImplementedException();
    }

    Task IBookRepository.Save()
    {
        throw new NotImplementedException();
    }
}