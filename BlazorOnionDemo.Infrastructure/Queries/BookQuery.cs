using BlazorOnionDemo.Application.Contracts.Queries;

namespace BlazorOnionDemo.Infrastructure.Queries;

public class BookQuery : IBookQuery
{
    Task<List<BookDto>> IBookQuery.GetAll()
    {
        throw new NotImplementedException();
    }

    Task<BookDto> IBookQuery.GetById(int id)
    {
        throw new NotImplementedException();
    }
}