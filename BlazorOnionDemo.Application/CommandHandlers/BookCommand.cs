using BlazorOnionDemo.Application.Contracts.Commands;

namespace BlazorOnionDemo.Application.CommandHandlers;

public class BookCommand : IBookCommand
{
    Task IBookCommand.Create(BookCreateCommandDto dto)
    {
        throw new NotImplementedException();
    }
}