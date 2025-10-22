using BlazorOnionDemo.Application.Contracts.Commands;
using BlazorOnionDemo.Application.Repositories;
using BlazorOnionDemo.Domain.Entity;

namespace BlazorOnionDemo.Application.CommandHandlers;

public class AuthorCommand : IAuthorCommand
{
    private readonly IAuthorRepository _repository;

    public AuthorCommand(IAuthorRepository repository)
    {
        _repository = repository;
    }
    async Task IAuthorCommand.AddBookToAuthorAsync(AddBookToAuthorCommandDto dto)
    {
        var author = await _repository.LoadAsync(dto.AuthorId);
        if (author == null)
        {
            throw new Exception("Author not found");
        }

        var book = new Book
        {
            Title = dto.Book.Title,
            Description = dto.Book.Description
        };
        author.Books.Add(book);
        await _repository.SaveAsync();

    }

    async Task IAuthorCommand.CreateAsync(AuthorCreateCommandDto dto)
    {
        var author = new Author { Name = dto.Name };
        await _repository.AddAsync(author);
    }
}