using BlazorOnionDemo.Domain.Entity;

namespace BlazorOnionDemo.Application.Repositories;

public interface IBookRepository
{
    Task<Book> Load(int id);
    Task Save();
}