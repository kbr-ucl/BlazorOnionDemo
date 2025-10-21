using BlazorOnionDemo.Application.Repositories;
using BlazorOnionDemo.Domain.Entity;

namespace BlazorOnionDemo.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    Task<Author> IAuthorRepository.Load(int id)
    {
        throw new NotImplementedException();
    }

    Task IAuthorRepository.Save()
    {
        throw new NotImplementedException();
    }
}