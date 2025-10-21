using BlazorOnionDemo.Domain.Entity;

namespace BlazorOnionDemo.Application.Repositories;

public interface IAuthorRepository
{
    Task<Author> Load(int id);
    Task Save();
}