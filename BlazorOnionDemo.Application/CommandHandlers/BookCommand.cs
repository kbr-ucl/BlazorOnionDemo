using BlazorOnionDemo.Application.Contracts.Commands;

namespace BlazorOnionDemo.Application.CommandHandlers;

public class BookCommand : IBookCommand
{
    async Task IBookCommand.CreateAsync(BookCreateCommandDto dto)
    {
        throw new NotImplementedException();
    }
}