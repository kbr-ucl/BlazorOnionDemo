using BlazorOnionDemo.Application.Contracts.Commands;

namespace BlazorOnionDemo.Application.CommandHandlers;

public class AuthorCommand : IAuthorCommand
{
    Task IAuthorCommand.AddBookToAuthor(AddBookToAuthorCommandDto dto)
    {
        throw new NotImplementedException();
    }

    Task IAuthorCommand.Create(AuthorCreateCommandDto dto)
    {
        throw new NotImplementedException();
    }
}