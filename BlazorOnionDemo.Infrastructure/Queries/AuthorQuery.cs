using BlazorOnionDemo.Application.Contracts.Queries;

namespace BlazorOnionDemo.Infrastructure.Queries;

public class AuthorQuery : IAuthorQuery
{
    Task<List<AuthorDto>> IAuthorQuery.GetAll()
    {
        throw new NotImplementedException();
    }

    Task<AuthorDto> IAuthorQuery.GetById(int id)
    {
        throw new NotImplementedException();
    }
}