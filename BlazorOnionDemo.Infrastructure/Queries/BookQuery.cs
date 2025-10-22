using BlazorOnionDemo.Application.Contracts.Queries;

namespace BlazorOnionDemo.Infrastructure.Queries;

public class BookQuery : IBookQuery
{
    async Task<List<BookDto>> IBookQuery.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    async Task<BookDto> IBookQuery.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}